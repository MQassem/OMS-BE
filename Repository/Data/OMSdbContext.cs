using Microsoft.EntityFrameworkCore;
using Repository.Model;

namespace Repository.Data
{
    public class OMSdbContext : DbContext
    {
        public OMSdbContext(DbContextOptions<OMSdbContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> orderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id)
                    .UseIdentityColumn();

                entity.Property(e => e.EntryDate)
                    .HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Price)
                    .HasPrecision(18, 2);
            });
            modelBuilder.Entity<Order>().HasData(
                new Order()
                {
                    Id = 1,
                    ClientId = 1,
                    Price = 100,
                    EntryUserId = 1,
                });
            modelBuilder.Entity<OrderDetails>().HasData(
                new OrderDetails()
                {
                    Id = 1,
                    ItemId = 1,
                    ItemPrice = 100,
                    OrderId = 1,
                });
        //    modelBuilder.Entity<OrderDetails>()
        //.HasOne(d => d.Order)
        //.WithMany(o => o.OrderDetails)
        //.HasForeignKey(d => d.OrderId)
        //.OnDelete(DeleteBehavior.Cascade);
        }
    }
}
