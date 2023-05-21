using Microsoft.EntityFrameworkCore;
using OrderApi.Domain.Entities;

namespace OrderApi.Data.Database
{
    public class OrderContext:DbContext
    {
        public OrderContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity => {
                entity.Property(e => e.CustomerFullName).IsRequired();
            });
        }
    }
}
