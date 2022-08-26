using entities.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities
{
    public class ProfileEntity : IProfile
    {
        public Guid Id { get; set; }


        [ForeignKey("User")]
        public Guid UserFk { get; set; }
        public IUser User { get; set; }

        IUser IProfile.User
        {
            get => User;
            set
            {
                User = value as UserEntity;
            }
        }

        public string PasswordHash { get; set; }
    }
}
