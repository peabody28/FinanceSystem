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
using repositories.Interfaces;
using repositories.Repositories;

namespace tests
{
    public class UserTest
    {
        private IUserOperation _userOperation;

        private IProfileOperation _profileOperation;

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
            services.AddScoped<IProfileOperation, ProfileOperation>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();


            services.AddScoped<IUser, UserEntity>();
            services.AddScoped<IProfile, ProfileEntity>();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<ILoggingOperation, LoggingOperation>();

            var serviceProvider = services.BuildServiceProvider();

            #endregion

            _userOperation = serviceProvider.GetService<IUserOperation>();
            _profileOperation = serviceProvider.GetService<IProfileOperation>();
        }

        [Test]
        [TestCase("testUser")]
        [TestCase("")]
        public void CreateUser(string name)
        {
            var user = _userOperation.Create(name);
            if (string.IsNullOrEmpty(name))
                Assert.IsNull(user, "user with emtpy name is created");
            else
                Assert.IsNotNull(user, "user creating fail");
        }

        [Test]
        [TestCase("max", "123456")]
        [TestCase("", "123465")]
        [TestCase("max", "")]
        public void CreateProfile(string name, string passwd)
        {
            var profile = _profileOperation.Create(name, passwd);

            if (string.IsNullOrEmpty(name))
                Assert.IsNull(profile, "user with emtpy name is created");
            else
                Assert.IsNotNull(profile, "profile creating fail");
        }
    }
}