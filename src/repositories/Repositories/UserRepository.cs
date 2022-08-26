using entities;
using entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IProfileRepository ProfileRepository { get; set; }

        private IServiceProvider Container { get; set; }

        private Bank Bank { get; set; }

        public UserRepository(Bank database, IServiceProvider serviceProvider, IProfileRepository profileRepository)
        {
            Bank = database;
            Container = serviceProvider;
            ProfileRepository = profileRepository;
        }

        public IUser GetObject(string name)
        {
            var user = Bank.User.Where(user => user.Name.Equals(name)).FirstOrDefault();
            return user;
        }

        public IEnumerable<IUser> Read()
        {
            return Bank.User.Cast<IUser>();
        }

        public IUser Create(string name)
        {
            var entity = Container.GetRequiredService<IUser>();
            entity.Id = Guid.NewGuid();
            entity.Name = name;

            var user = Bank.User.Add(entity as UserEntity);
            Bank.SaveChanges();

            return user.Entity;
        }
    }
}
