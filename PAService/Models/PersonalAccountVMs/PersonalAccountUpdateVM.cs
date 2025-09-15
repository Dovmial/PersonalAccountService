using DataLib.Entities;
using PAService.Helpers.Validators;
using PAService.Models.ResidentVMs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PAService.Models.PersonalAccountVMs
{
    public record PersonalAccountUpdateVM(
        [Required]
        int Id,

        [Required(ErrorMessage = "Обязательное поле")]
        string Address,

        [Required(ErrorMessage = "Обязательное поле")]
        double Area,

        [Required(ErrorMessage = "Обязательное поле")]
        DateTime DateActivate,

        [DateGreaterThan(nameof(DateActivate))]
        DateTime? DateFinish,

        ICollection<ResidentVM> Residents);

    public static class PersonalAccountUpdateExt
    {
        public static void ToUpdateEntity(
            this PersonalAccountUpdateVM accountUpdateVM,
            PersonalAccount account)
        {
            var residents = accountUpdateVM.Residents.ToEntityCollection();

            account.DateActivate = accountUpdateVM.DateActivate;
            account.DateFinish = accountUpdateVM.DateFinish;
            account.Address = accountUpdateVM.Address;
            account.Area = accountUpdateVM.Area;
            account.Residents = residents;
        }
    }
}
