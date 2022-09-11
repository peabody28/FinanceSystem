using background.task.tasks;
using entities;
using entities.Interfaces;
using logger.Interfaces;
using logger.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using operations.Interfaces;
using operations.Operations;
using repositories;
using repositories.Interfaces;
using repositories.Repositories;
using System;

namespace background.task
{
    internal class Program
    {
        private static IServiceProvider Container { get; set; }

        static void Main(string[] args)
        {
            #region [ Dependencies Registration ]

            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .AddCommandLine(args)
               .Build();

            var services = new ServiceCollection();

            services.AddScoped<Bank, Bank>();

            services.AddScoped<IUserOperation, UserOperation>();
            services.AddScoped<IFinanceOperationOperation, FinanceOperationOperation>();

            services.AddScoped<IFinanceOperationRepository, FinanceOperationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IChainRepository, ChainRepository>();

            services.AddScoped<IUser, UserEntity>();
            services.AddScoped<IProfile, ProfileEntity>();
            services.AddScoped<IChain, ChainEntity>();
            services.AddScoped<IFinanceOperation, FinanceOperationEntity>();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddScoped<ILoggingOperation, LoggingOperation>();

            Container = services.BuildServiceProvider();

            #endregion

            var financeOperationOperation = Container.GetRequiredService<IFinanceOperationOperation>();
            var financeOperationRepository = Container.GetRequiredService<IFinanceOperationRepository>();

            var task = new ApprovingFinanceOperationTask(financeOperationRepository, financeOperationOperation, configuration).Create();
            task.Start();
            task.Wait();
        }
    }
}
