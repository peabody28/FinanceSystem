using entities.Interfaces;
using helpers;
using operations.Interfaces;
using repositories.Extensions;
using repositories.Interfaces;
using System;

namespace operations.Operations
{
    public class ProfileOperation : IProfileOperation
    {
        #region [ Dependency ]

        private IUserOperation UserOperation { get; set; }

        private IProfileRepository ProfileRepository { get; set; }

        private IServiceProvider Container { get; set; }

        #endregion

        public ProfileOperation(IServiceProvider container, IUserOperation userOperation, IProfileRepository profileRepository)
        {
            Container = container;
            UserOperation = userOperation;
            ProfileRepository = profileRepository;
        }

        public IProfile Create(string username, string password)
        {
            return Container.InTransaction(() =>
            {
                var user = UserOperation.Create(username);

                if (user == null)
                    return null;

                var passwordHash = MD5Helper.Encode(password);
                
                var profile = ProfileRepository.Create(user, passwordHash);

                return profile;
            });
        }
    }
}
