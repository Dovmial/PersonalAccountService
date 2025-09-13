using DataLib.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAService.Services.Interfaces
{
    public interface IPersonalAccountService
    {
        Task<ICollection<PersonalAccount>> Get();
        Task CreateAsync(PersonalAccount account);
        
    }
}
