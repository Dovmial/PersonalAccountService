
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLib.Entities
{
    public sealed class Resident
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }

        [Display(Name = "Отчество")]
        public string LastName { get; set; }

        [NotMapped]
        [Display(Name = "ФИО")]
        public string FullName => $"{LastName} {FirstName} {SecondName}";
        public ICollection<PersonalAccount> PersonalAccounts { get; set; }
            = new List<PersonalAccount>();
    }
}
