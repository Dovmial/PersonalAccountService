using DataLib.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PAService.DTOs
{
    public record PersonalAccountDTO(
        [Required(ErrorMessage = "Обязательное поле")]
    string Address,
        [Required(ErrorMessage = "Обязательное поле")]
    double Area);

    public static class PersonalAccountDTOExt
    {
        public static PersonalAccount ToEntity(this PersonalAccountDTO account)
            => new PersonalAccount()
            {
                Address = account.Address,
                Area = account.Area,
            };
    }
}
