using FiguraSp.Riders.Entity;
using FiguraSp.Riders.Model.Data;
using FiguraSp.Riders.Model.DTOs.Requests;
using FiguraSp.Riders.Model.DTOs.Responses;
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

            return new() { Success = true, ResponseRider = rider};
        }

        public async Task<Rider> GetRiderById(Guid id)
        {
            IQueryable<Rider> query = context.Riders.Where(r => r.Id.Equals(id)).AsQueryable();
            var rider = await context.GetFirstOrDefaultAsync(query);
            return rider;
        }

        public async Task<RiderResponseDto> GetRiderByInitials(string name, string surname)
        {
            IQueryable<Rider> query = context.Riders.Where(r => r.Name.Equals(name) && r.Surname.Equals(surname)).AsQueryable();
            var rider = await context.GetFirstOrDefaultAsync(query);
            if(rider == null)
            {
                return new() { Errors = ["Rider not found"] };
            }
            return new() { Success = true, ResponseRider = rider};
        }

        public async Task<List<Rider>> GetRiders()
        {
            IQueryable<Rider> query = context.Riders.AsQueryable().AsNoTracking();
            var riders = await context.GetEntitiesToListAsync(query);
            return riders;
        }
    }

    public interface IRiderService
    {
        public Task<List<Rider>> GetRiders();
        public Task<Rider> GetRiderById(Guid id);
        public Task<RiderResponseDto> GetRiderByInitials(string name, string surname);
        public Task<RiderResponseDto> AddRider(NewRiderRequestDto riderDto);
    }
}
