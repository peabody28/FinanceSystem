using entities.Interfaces;
using operations.Interfaces;
using repositories;
using repositories.Interfaces;
using System;

namespace operations.Operations
{
    public class ProfileOperation : IProfileOperation
    {
        #region [ Dependency ]

        private IUserOperation UserOperation { get; set; }

        private IProfileRepository ProfileRepository { get; set; }

        private Bank Bank { get; set; }

        #endregion

        public ProfileOperation(IUserOperation userOperation, IProfileRepository profileRepository, Bank bank)
        {
            UserOperation = userOperation;
            ProfileRepository = profileRepository;
            Bank = bank;
        }

        public IProfile Create(string username, string passwordHash)
        {
            var transaction = Bank.Database.BeginTransaction();
            try
            {
                var user = UserOperation.Create(username);

                var profile = ProfileRepository.Create(user, passwordHash);

                transaction.Commit();

                return profile;
            }
            catch(Exception)
            {
                transaction.Rollback();
                return null;
            }
        }
    }
}
