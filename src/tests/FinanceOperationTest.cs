using entities;
using entities.Interfaces;
using logger.Interfaces;
using logger.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using operations.Interfaces;
using operations.Operations;
using repositories;
using repositories.Extensions;
using repositories.Interfaces;
using repositories.Repositories;
using System;

namespace tests
{
    public class FinanceOperationTest
    {
        private IUserOperation _userOperation;

        private IFinanceOperationOperation _financeOperationOperation;

        private IServiceProvider Container { get; set; }

        [SetUp]
        public void Setup()
        {
            #region [ Dependencies ]

            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
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
            services.AddSingleton<ILoggingOperation, LoggingOperation>();

            Container = services.BuildServiceProvider();

            #endregion

            _financeOperationOperation = Container.GetService<IFinanceOperationOperation>();
            _userOperation = Container.GetService<IUserOperation>();
        }

        [Test]
        public void CreateFinanceOperation()
        {
            Container.InTransaction(() =>
            {
                var user = _userOperation.Create("testUser");

                Assert.IsNotNull(user, "user creating fail");

                var entity = _financeOperationOperation.Create(user, 50);

                Assert.IsNotNull(entity, "finance operation crating fail");

                Assert.IsFalse(entity.IsApproved, "finance operation must be not approved");
            });
        }
    }
}