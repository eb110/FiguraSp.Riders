using FiguraSp.Riders.Entity;
using FiguraSp.Riders.Model.Data;
using FiguraSp.Riders.Model.DTOs.Requests;
using FiguraSp.Riders.Model.DTOs.Responses;
using FiguraSp.Riders.Model.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Figurasp.Riders.Service.Services
{
    public class RiderService(RidersDbContext context) : IRiderService
    {
        public async Task<RiderResponseDto> AddRider(NewRiderRequestDto riderDto)
        {
            IQueryable<Rider> existQuery = context.Riders.Where(r => r.Name.Equals(riderDto.Name) && r.Surname.Equals(riderDto.Surname)).AsQueryable();
            var existRider = await context.GetFirstOrDefaultAsync(existQuery);
            if (existRider != null)
            {
                return new() { Errors = ["Rider already exist"] };
            }
            var rider = new Rider
            {
                Name = riderDto.Name,
                Surname = riderDto.Surname,
                Nationality = riderDto.Nationality,
                DoB = riderDto.DoB,
                PictureUrl = riderDto.PictureUrl
            };

            context.Riders.Add(rider);
            var check = await context.SaveChangesAsync();

            if(check != 1)
            {
                return new() { Errors = ["Failed to add rider"] };
            }

            return new() { Success = true };
        }

        public async Task<RiderResponseDto> GetRiderById(Guid id)
        {
            IQueryable<Rider> query = context.Riders.Where(r => r.Id.Equals(id)).AsQueryable();
            var rider = await context.GetFirstOrDefaultAsync(query);
            return rider.ToRiderResponseDto();
        }

        public async Task<RiderResponseDto> GetRiderByInitials(string name, string surname)
        {
            IQueryable<Rider> query = context.Riders.Where(r => r.Name.Equals(name) && r.Surname.Equals(surname)).AsQueryable();
            var rider = await context.GetFirstOrDefaultAsync(query);
            if(rider == null)
            {
                return new() { Errors = ["Rider not found"] };
            }
            return rider.ToRiderResponseDto();
        }

        public async Task<List<RiderResponseDto>> GetRiders()
        {
            IQueryable<Rider> query = context.Riders.AsQueryable().AsNoTracking();
            var riders = await context.GetEntitiesToListAsync(query);

            List<RiderResponseDto> result = [.. riders.Select(x => x.ToRiderResponseDto())];

            return result;
        }

        public async Task<List<RiderResponseDto>> GetSeasonRiders(DateOnly year)
        {
            DateOnly seasonDateStart = year.AddYears(-50);
            DateOnly seasonDateEnd = year.AddYears(-16);
            IQueryable<Rider> query = context.Riders.Where(x => x.DoB >= seasonDateStart && x.DoB <= seasonDateEnd).AsQueryable().AsNoTracking();

            var riders = await context.GetEntitiesToListAsync(query);
            List<RiderResponseDto> result = [..riders.Select(x => x.ToRiderResponseDto())];

            return result;
        }

        public async Task<RiderResponseDto> RemoveRider(Guid id)
        {
            IQueryable<Rider> existQuery = context.Riders.Where(r => r.Id.Equals(id)).AsQueryable();
            var existRider = await context.GetFirstOrDefaultAsync(existQuery);

            if (existRider == null)
            {
                return new() { Errors = ["Rider not found"] };
            }

            context.Riders.Remove(existRider);
            var check = await context.SaveChangesAsync();
            if(check != 1)
            {
                return new() { Errors = ["Failed to remove rider"] };
            }
            return new() { Success = true };
        }

        public async Task<RiderResponseDto> UpdateRider(UpdateRiderRequestDto riderDto)
        {
            IQueryable<Rider> existQuery = context.Riders.Where(r => r.Id.Equals(riderDto.Id)).AsQueryable();
            var existRider = await context.GetFirstOrDefaultAsync(existQuery);
            if (existRider == null)
            {
                return new() { Errors = ["Rider not found"] };
            }
            existRider.Surname = riderDto.Surname;
            existRider.Name = riderDto.Name;
            existRider.Nationality = riderDto.Nationality;
            existRider.DoB = riderDto.DoB;
            existRider.PictureUrl = riderDto.PictureUrl;
            context.Update(existRider);
            var check = await context.SaveChangesAsync();
            if (check != 1)
            {
                return new() { Errors = ["Failed to update rider"] };
            }
            return new() { Success = true };
        }
    }

    public interface IRiderService
    {
        public Task<List<RiderResponseDto>> GetRiders();
        public Task<RiderResponseDto> GetRiderById(Guid id);
        public Task<RiderResponseDto> GetRiderByInitials(string name, string surname);
        public Task<RiderResponseDto> AddRider(NewRiderRequestDto riderDto);
        public Task<RiderResponseDto> RemoveRider(Guid id);
        public Task<RiderResponseDto> UpdateRider(UpdateRiderRequestDto riderDto);
        public Task<List<RiderResponseDto>> GetSeasonRiders(DateOnly year);
    }
}
