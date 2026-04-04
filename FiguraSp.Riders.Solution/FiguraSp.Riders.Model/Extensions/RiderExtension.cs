using FiguraSp.Riders.Entity;
using FiguraSp.Riders.Model.DTOs.Responses;

namespace FiguraSp.Riders.Model.Extensions
{
    public static class RiderExtension
    {
        public static RiderResponseDto ToRiderResponseDto(this Rider rider)
        {
            RiderResponseDto result = new()
            {
                Id = rider.Id,
                Name = rider.Name,
                Surname = rider.Surname,
                Nationality = rider.Nationality,
                DoB = rider.DoB,
                PictureUrl = rider.PictureUrl,
                Success = true
            };

            return result;
        }
    }
}
