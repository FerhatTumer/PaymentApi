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
                entity.Property(t => t.RowVersion)
                                 .IsRowVersion() // Concurrency token for optimistic concurrency control
                                 .IsRequired();  // Required field
                entity.HasMany(t => t.TransactionDetails)
                      .WithOne()  // No explicit navigation property in TransactionDetail
                      .HasForeignKey(td => td.TransactionId) // Foreign key relationship
                      .OnDelete(DeleteBehavior.Cascade); // Cascade delete if Transaction is deleted
            });


            modelBuilder.Entity<TransactionDetail>(entity =>
            {
                entity.ToTable("TransactionDetail");  // Tablo adını belirtmek için
                entity.HasKey(td => td.Id);           // Primary Key
                entity.Property(td => td.TransactionType).IsRequired().HasMaxLength(20);
                entity.Property(td => td.Status).IsRequired().HasMaxLength(20);
                entity.Property(td => td.Amount).IsRequired();
                entity.Property(td => td.RowVersion)
                .IsRowVersion() // Concurrency token for optimistic concurrency control
                .IsRequired();  // Required field
                entity.HasOne<Transaction>()
                      .WithMany(t => t.TransactionDetails) // Set up one-to-many relationship
                      .HasForeignKey(td => td.TransactionId) // Foreign key relationship
                      .OnDelete(DeleteBehavior.Cascade); // Cascade delete if Transaction is deleted
            });

            modelBuilder.Ignore<DomainEvent>();
        }
    }
}