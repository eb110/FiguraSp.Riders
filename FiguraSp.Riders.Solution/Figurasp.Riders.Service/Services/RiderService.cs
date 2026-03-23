using FiguraSp.Riders.Entity;
using FiguraSp.Riders.Model.Data;
using Microsoft.EntityFrameworkCore;

namespace Figurasp.Riders.Service.Services
{
    public class RiderService(RidersDbContext context) : IRiderService
    {
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
    }
}
