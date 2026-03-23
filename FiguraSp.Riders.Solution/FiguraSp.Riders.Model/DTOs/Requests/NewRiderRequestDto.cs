using System.ComponentModel.DataAnnotations;

namespace FiguraSp.Riders.Model.DTOs.Requests
{
    public class NewRiderRequestDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string Surname { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string Nationality { get; set; } = null!;
        [Required]
        public DateOnly DoB { get; set; }
        [MaxLength(50)]
        public string PictureUrl { get; set; } = null!;
    }
}
