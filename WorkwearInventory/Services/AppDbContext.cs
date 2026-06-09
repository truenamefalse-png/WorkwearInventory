using System.Data.Entity;
using WorkwearInventory.Models;

namespace WorkwearInventory.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<IssueReceipt> IssueReceipts { get; set; }    // было Sales
        public DbSet<IssueItem> IssueItems { get; set; }         // было SaleItems

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<IssueReceipt>()
                .HasMany(r => r.Items)
                .WithRequired(i => i.IssueReceipt)
                .HasForeignKey(i => i.IssueReceiptId);
        }
    }
}