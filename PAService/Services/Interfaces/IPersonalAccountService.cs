using DataLib.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAService.Services.Interfaces
{
    public interface IPersonalAccountService
    {
        Task<ICollection<PersonalAccount>> GetAsync();
        Task CreateAsync(PersonalAccount account);
    }
}
