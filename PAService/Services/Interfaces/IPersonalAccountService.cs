using DataLib.Entities;
using DataLib.Filters;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PAService.Services.Interfaces
{
    public interface IPersonalAccountService
    {
        Task<ICollection<PersonalAccount>> GetAsync(
            FilterPersonalAccount filter,
            string sort,
            string direct,
            int page,
            int pagesize,
            CancellationToken cancellationToken);
        Task<PersonalAccount> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<PersonalAccount> Details(
            string numberPersonalAccount,
            CancellationToken cancellationToken);
        Task CreateAsync(PersonalAccount account, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(PersonalAccount account, CancellationToken cancellationToken);
    }
}
