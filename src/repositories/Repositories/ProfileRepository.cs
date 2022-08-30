using entities;
using entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using repositories.Interfaces;
using System;
using System.Linq;

namespace repositories.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private IServiceProvider Container { get; set; }

        private Bank Bank { get; set; }

        public ProfileRepository(Bank database, IServiceProvider serviceProvider)
        {
            Bank = database;
            Container = serviceProvider;
        }

        public IProfile GetObject(string name, string passwordHash)
        {
            var profile = Bank.Profile.Where(item => item.User.Name.Equals(name) && item.PasswordHash.Equals(passwordHash)).FirstOrDefault();
            return profile;
        }

        public IProfile Create(IUser user, string passwordHash)
        {
            var entity = Container.GetRequiredService<IProfile>();
            entity.Id = Guid.NewGuid();
            entity.User = user;
            entity.PasswordHash = passwordHash;

            var profile = Bank.Profile.Add(entity as ProfileEntity);
            Bank.SaveChanges();
            return profile.Entity;
        }
    }
}
