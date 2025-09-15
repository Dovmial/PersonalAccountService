using DataLib.Entities;
using System.ComponentModel.DataAnnotations;

namespace PAService.Models.PersonalAccountVMs
{
    public record PersonalAccountCreateVM(
        [Required(ErrorMessage = "Обязательное поле")]
        string Address,

        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0, double.MaxValue, ErrorMessage = "Должно быть положительным числом")]
        double Area);

    public static class PersonalAccountVMExt
    {
        public static PersonalAccount ToEntity(this PersonalAccountCreateVM account)
            => new PersonalAccount()
            {
                Address = account.Address,
                Area = account.Area,
            };
    }
}
