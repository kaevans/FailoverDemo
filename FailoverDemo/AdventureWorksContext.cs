using System.Data.Entity;

namespace FailoverDemo
{
    public partial class AdventureWorksContext : DbContext
    {
        public AdventureWorksContext()
            : base("name=AdventureWorks")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.PasswordHash)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.PasswordSalt)
                .IsUnicode(false);
        }
    }
}
