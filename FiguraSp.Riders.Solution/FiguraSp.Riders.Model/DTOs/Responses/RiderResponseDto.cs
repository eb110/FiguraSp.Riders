using FiguraSp.SharedLibrary.Responses;

namespace FiguraSp.Riders.Model.DTOs.Responses
{
    public record RiderResponseDto : DefaultResponse
    {
        public Guid? Id { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Nationality { get; set; }

        public DateOnly? DoB { get; set; }

        public string? PictureUrl { get; set; }
    }
}
