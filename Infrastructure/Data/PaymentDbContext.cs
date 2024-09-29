using Core.Entities;
using Core.Events;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        {
        }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<TransactionDetail> TransactionDetail { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.BankId).IsRequired();
                entity.Property(t => t.TotalAmount).IsRequired();
                entity.Property(t => t.NetAmount).IsRequired();
                entity.Property(t => t.Status).IsRequired().HasMaxLength(20);
                entity.Property(t => t.OrderReference).IsRequired().HasMaxLength(50);
                entity.Property(t => t.TransactionDate).IsRequired();
            });


            modelBuilder.Entity<TransactionDetail>(entity =>
            {
                entity.ToTable("TransactionDetail");  // Tablo adını belirtmek için
                entity.HasKey(td => td.Id);           // Primary Key
                entity.Property(td => td.TransactionType).IsRequired().HasMaxLength(20);
                entity.Property(td => td.Status).IsRequired().HasMaxLength(20);
                entity.Property(td => td.Amount).IsRequired();
                // Foreign Key: TransactionId ile Transaction tablosuna referans
                entity.HasOne(td => td.Transaction)
                      .WithMany(t => t.TransactionDetails)
                      .HasForeignKey(td => td.TransactionId)
                      .OnDelete(DeleteBehavior.Cascade); // Transaction silindiğinde, TransactionDetail de silinir
            });

            modelBuilder.Ignore<DomainEvent>();
        }
    }
}