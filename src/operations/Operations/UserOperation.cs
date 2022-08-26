using repositories.Interfaces;
using operations.Interfaces;
using System.Collections.Generic;
using entities.Interfaces;

namespace operations.Operations
{
    public class UserOperation : IUserOperation
    {
        public IUserRepository UserRepository { get; set; }

        public UserOperation(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public IEnumerable<IUser> Read() => UserRepository.Read();

        public IUser GetObject(string name)
        {
            return UserRepository.GetObject(name);
        }

        public IUser Create(string name) => UserRepository.Create(name);
    }
}
