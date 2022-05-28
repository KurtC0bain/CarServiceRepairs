using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class CarServiceDbContext : DbContext
    {
        public CarServiceDbContext(DbContextOptions<CarServiceDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<DetailOrder> DetailOrders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DetailOrder>()
            .HasKey(a => new { a.DetailId, a.OrderId});
        }
    }
}
