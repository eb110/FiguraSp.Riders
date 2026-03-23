using FiguraSp.Riders.Entity;
using FiguraSp.SharedLibrary.Responses;

namespace FiguraSp.Riders.Model.DTOs.Responses
{
    public record class RiderResponseDto : DefaultResponse
    {
        public Rider? ResponseRider { get; init; }
    }
}
