using DataLib;
using DataLib.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PAService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ICollection<Resident>> GetAllAsync()
        {
            return await _dbContext.Residents
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task DeleteAsync(int id)
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

        public Task GetByIdAsync(int id)
        {
            return _dbContext.Residents
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Resident resident)
        {
            await _dbContext.Residents.AddAsync(resident);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Resident resident)
        {
            _dbContext.Update(resident);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddPersonalAccount(Resident resident, int accountId)
        {
            if (accountId != 0)
            {
                var account = await _dbContext.PersonalAccounts.FindAsync(accountId);
                if (account is not null)
                {
                    resident.PersonalAccounts.Add(account);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<ICollection<Resident>> GetResidentsByPersonalAccountId(int personalAccountId)
        {
            if (personalAccountId == 0)
                return new List<Resident>();
            
            var account = await _dbContext.PersonalAccounts
                .AsNoTracking()
                .Include(x => x.Residents)
                .FirstAsync(x => x.Id == personalAccountId);
            return account.Residents;
        }
    }
}
