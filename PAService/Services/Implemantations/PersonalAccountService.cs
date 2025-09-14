using DataLib;
using DataLib.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PAService.Services.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task CreateAsync(PersonalAccount account)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                _dbContext.PersonalAccounts.Add(account);
                account.DateActivate = DateTime.UtcNow;
                account.Number = "0000000000";
                await _dbContext.SaveChangesAsync();
                //сохраняем уникальный номер в нужном формате
                account.Number = account.Id.ToString("D10");
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("error creating accaunt: {ex}", ex);
                await transaction.RollbackAsync();
                throw;
            }
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<PersonalAccount>> GetAsync()
        {
            return await _dbContext.PersonalAccounts.ToListAsync();
        }

        public Task GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PersonalAccount resident)
        {
            throw new NotImplementedException();
        }
    }
}
