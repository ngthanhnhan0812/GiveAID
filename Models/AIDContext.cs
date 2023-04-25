using Microsoft.EntityFrameworkCore;

namespace GiveAID.Models
{
    public class AIDContext : DbContext
    {
        public AIDContext(DbContextOptions<AIDContext> options) 
            : base(options) { }

        public AIDContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source = ADMIN;Initial Catalog= GiveAID; Persist Security Info= True;User ID=sa;Password=08122003;TrustServerCertificate=true");
        }

        #region model
        public DbSet<Admin>? Admins { get; set; }
        public DbSet<Member>? Members { get; set; }
        public DbSet<Topic>? Topics { get; set; }
        public DbSet<Donation>? Donations { get; set; }
        public DbSet<Organization_program>? Organization_Programs { get; set; }
        public DbSet<Gallery>? Galleries { get; set; }
        public DbSet<Partnership>? Partnerships { get; set; }
        public DbSet<Payment>? Payment { get; set; }
        public DbSet<Contact>? Contacts { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Member>()
                .Property(s => s.created_at)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Topic>()
                .Property(s => s.created_at)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Donation>() 
                .Property(s => s.donation_date)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Donation>()
                .Property(s => s.created_at)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Organization_program>()
                .Property(s => s.created_at)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Gallery>()
                .Property(s => s.created_at)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Partnership>()
                .Property(s => s.created_at)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Payment>()
                .Property(s => s.created_at)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Contact>()
                .Property(s => s.created_at)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
