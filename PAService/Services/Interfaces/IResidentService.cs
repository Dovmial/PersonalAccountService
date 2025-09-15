using DataLib.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PAService.Services.Interfaces
{
    public interface IResidentService
    {
        Task GetByIdAsync(int id, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task CreateAsync(Resident resident, CancellationToken cancellationToken);
        Task UpdateAsync(Resident resident, CancellationToken cancellationToken);
        Task<ICollection<Resident>> GetAllAsync(bool withPersonalAccount, CancellationToken cancellationToken);
        Task<ICollection<Resident>> GetResidentsByPersonalAccountId(
            int personalAccountId,
            CancellationToken cancellationToken);
        Task AddToPersonalAccount(
            Resident resident,
            int accountId,
            CancellationToken cancellationToken);
    }
}
