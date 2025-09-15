using DataLib;
using DataLib.Entities;
using DataLib.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PAService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PAService.Services.Implemantations
{
    public class PersonalAccountService : IPersonalAccountService
    {
        private readonly ILogger<PersonalAccountService> _logger;
        private readonly AppDbContext _dbContext;

        public PersonalAccountService(
            ILogger<PersonalAccountService> logger,
            AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task CreateAsync(PersonalAccount account, CancellationToken cancellationToken)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                _dbContext.PersonalAccounts.Add(account);
                account.DateActivate = DateTime.UtcNow;
                account.Number = "0000000000";
                await _dbContext.SaveChangesAsync(cancellationToken);
                //сохраняем уникальный номер в нужном формате
                account.Number = account.Id.ToString("D10");
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("error creating accaunt: {ex}", ex);
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var account = await _dbContext.PersonalAccounts
                 .FindAsync(id, cancellationToken);
            if (account is null)
                throw new ArgumentNullException($"{nameof(account)}: не найден");
            _dbContext.PersonalAccounts.Remove(account);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<ICollection<PersonalAccount>> GetAsync(
            bool withResidents,
            CancellationToken cancellationToken)
        {
            return await _dbContext.PersonalAccounts
                .AsNoTracking()
                .WithResidents(withResidents)
                .ToListAsync(cancellationToken);
        }

        public async Task<PersonalAccount> GetByIdAsync(int id, CancellationToken cancellationToken)
        { 
            return await _dbContext.PersonalAccounts
                .WithResidents(true)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<PersonalAccount> Details(
            string numberPersonalAccount,
            CancellationToken cancellationToken)
        {
            if(!int.TryParse(numberPersonalAccount, out int accountId))
                return await GetByIdAsync(accountId, cancellationToken);
            _logger.LogError("Некорретный номер лицего счета {number}", numberPersonalAccount);
            return null;
        }

        public async Task UpdateAsync(PersonalAccount account, CancellationToken cancellationToken)
        {
            _dbContext.PersonalAccounts.Update(account);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
