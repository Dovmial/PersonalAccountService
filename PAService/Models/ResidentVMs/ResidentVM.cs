using DataLib.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PAService.Models.ResidentVMs
{
    public sealed record ResidentVM(

        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(50, ErrorMessage = "не более 50 символов")]
        string LastName,

        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(50, ErrorMessage = "не более 50 символов")]
        string FirstName,

        string SecondName);

    public static class ResidentVmExt
    {
        public static Resident ToEntity(this ResidentVM vm)
            => new Resident()
            {
                FirstName = vm.FirstName.Trim(),
                LastName = vm.LastName.Trim(),
                SecondName = vm.SecondName.Trim()
            };

        public static ICollection<Resident> ToEntityCollection(this IEnumerable<ResidentVM> residentVMs)
         => residentVMs
                .Select(x => x.ToEntity())
                .ToList();
        
    }
}
