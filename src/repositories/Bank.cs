using entities;
using logger.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace repositories
{
    public class Bank : DbContext
    {
        private IConfiguration Configuration { get; set; }

        public ILoggingOperation LoggingOperation { get; set; }

        public Bank(IConfiguration config, ILoggingOperation loggingOperation)
        {
            Configuration = config;
            LoggingOperation = loggingOperation;
        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<ProfileEntity> Profile { get; set; }
        public DbSet<FinanceOperationEntity> FinanceOperation { get; set; }
        public DbSet<ChainEntity> Chain { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Configuration.GetConnectionString("Bank");
            optionsBuilder.UseSqlServer(connString);
            optionsBuilder.LogTo(LoggingOperation.Log, LogLevel.Debug);
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
