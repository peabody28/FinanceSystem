using entities.Interfaces;
using System;

namespace entities
{
    public class UserEntity : IUser
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
