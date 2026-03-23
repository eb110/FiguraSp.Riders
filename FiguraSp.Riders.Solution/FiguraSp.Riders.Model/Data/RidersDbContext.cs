using FiguraSp.Riders.Entity;
using Microsoft.EntityFrameworkCore;

namespace FiguraSp.Riders.Model.Data
{
    public partial class RidersDbContext(DbContextOptions<RidersDbContext> options) : DbContext(options)
    {
        public virtual DbSet<Rider> Riders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Persian_100_CS_AS_KS_WS_SC_UTF8");

            modelBuilder.Entity<Rider>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Rider__3214EC07EDBD0851");

                entity.ToTable("Rider");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.Name).HasMaxLength(30);
                entity.Property(e => e.Nationality).HasMaxLength(30);
                entity.Property(e => e.PictureUrl).HasMaxLength(50);
                entity.Property(e => e.Surname).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public virtual async Task<T> GetFirstOrDefaultAsync<T>(IQueryable<T> query)
        {
            var entity = await query.FirstOrDefaultAsync();

            return entity!;
        }

        public virtual async Task<List<T>> GetEntitiesToListAsync<T>(IQueryable<T> query)
        {
            var entities = await query.ToListAsync();

            return entities;
        }
    }
}
