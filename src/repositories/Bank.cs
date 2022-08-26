using entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace repositories
{
    public class Bank : DbContext
    {
        private IConfiguration Configuration { get; set; }

        public Bank(IConfiguration config)
        {
            Configuration = config;
        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<ProfileEntity> Profile { get; set; }
        public DbSet<FinanceOperationEntity> FinanceOperation { get; set; }
        public DbSet<ChainEntity> Chain { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Configuration.GetConnectionString("Bank");
            optionsBuilder.UseSqlServer(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FinanceOperationEntity>()
                .HasOne(c => c.User as UserEntity);

            modelBuilder.Entity<FinanceOperationEntity>()
                .HasOne(c => c.Chain as ChainEntity);

            modelBuilder.Entity<ProfileEntity>()
                .HasOne(c => c.User as UserEntity);
        }
    }
}
