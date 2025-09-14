using DataLib.Entities;
using System.ComponentModel.DataAnnotations;

namespace PAService.DTOs
{
    public record ResidentDTO(
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(50, ErrorMessage = "не более 50 символов")]
    string LastName,
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(50, ErrorMessage = "не более 50 символов")]
    string FirstName,
        string SecondName);

    public static class ResidentDtoExt
    {
        public static Resident ToEntity(this ResidentDTO dto)
            => new Resident()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                SecondName = dto.SecondName
            };
    }
}