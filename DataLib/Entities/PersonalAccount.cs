
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLib.Entities
{
    public sealed class PersonalAccount
    {
        public int Id { get; set; }

        [RegularExpression(@"\d{10}$", ErrorMessage = "должно быть 10 цифр")]
        [Display(Name = "Номер ЛС")]
        public string Number { get; set; }

        [Display(Name = "Дата активации")]
        public DateTime DateActivate { get; set; }

        [Display(Name = "Дата окончания")]
        public DateTime? DateFinish { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Площадь должна быть положительным числом")]
        [Display(Name = "Площадь, м²")]
        public double Area { get; set; }
        public ICollection<Resident> Residents { get; set; } = new List<Resident>();

        [NotMapped]
        [Display(Name = "Количство проживающих")]
        public int ResidentsCount => Residents.Count;
    }
}
