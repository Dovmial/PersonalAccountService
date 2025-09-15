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
    public sealed class ResidentService: IResidentService
    {
        private readonly ILogger<ResidentService> _logger;
        private readonly AppDbContext _dbContext;
        public ResidentService(
            ILogger<ResidentService> logger,
            AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task<ICollection<Resident>> GetAllAsync(bool withPersonalAccount, CancellationToken cancellationToken)
        {
            return await _dbContext.Residents
                .AsNoTracking()
                .WithPersonalAccounts(withPersonalAccount)
                .ToListAsync();
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var resident = await _dbContext.Residents.FirstAsync(x => x.Id == id);
            if (resident is null)
            {
                string error = $"Жилец [{id}] не найден";
                _logger.LogError(error);
                throw new NullReferenceException(error);
            }
            _dbContext.Residents.Remove(resident);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Жилец [{id}] удален");
        }

        public Task GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _dbContext.Residents
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Resident resident, CancellationToken cancellationToken)
        {
            await _dbContext.Residents.AddAsync(resident);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Жилец создан {id}", resident.Id);
        }

        public async Task UpdateAsync(Resident resident, CancellationToken cancellationToken)
        {
            _dbContext.Update(resident);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Данные жильца {id} обновлены", resident.Id);
        }

        public async Task AddToPersonalAccount(
            Resident resident,
            int accountId, 
            CancellationToken cancellationToken)
        {
            if (accountId != 0)
            {
                var account = await _dbContext.PersonalAccounts.FindAsync(accountId);
                if (account is not null)
                {
                    resident.PersonalAccounts.Add(account);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    _logger.LogInformation("к лицевому счету {number} добален жилец {residentId}",
                        account.Number, resident.Id);
                }
                _logger.LogWarning($"Лицевой счет [{accountId.ToString("D10")}] не найден");
            }
        }

        public async Task<ICollection<Resident>> GetResidentsByPersonalAccountId(
            int personalAccountId,
            CancellationToken cancellationToken)
        {
            if (personalAccountId == 0)
                return new List<Resident>();
            
            var account = await _dbContext.PersonalAccounts
                .AsNoTracking()
                .Include(x => x.Residents)
                .FirstAsync(x => x.Id == personalAccountId, cancellationToken);
            return account.Residents;
        }
    }
}
