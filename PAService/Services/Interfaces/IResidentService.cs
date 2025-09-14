using DataLib.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAService.Services.Interfaces
{
    public interface IResidentService
    {
        Task GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task CreateAsync(Resident resident);
        Task UpdateAsync(Resident resident);
        Task<ICollection<Resident>> GetAllAsync();
        Task<ICollection<Resident>> GetResidentsByPersonalAccountId(int personalAccountId);
    }
}
