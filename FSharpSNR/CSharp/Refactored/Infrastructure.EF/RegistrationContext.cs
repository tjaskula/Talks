using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Infrastructure.EF
{
    public class RegistrationContext : DbContext
    {
        public RegistrationContext() : base("RegistrationContext")
        {
        }

        public DbSet<AccountEntity> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}