using System.ComponentModel.DataAnnotations;

namespace FiguraSp.Riders.Model.DTOs.Requests
{
    public class UpdateRiderRequestDto : NewRiderRequestDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
