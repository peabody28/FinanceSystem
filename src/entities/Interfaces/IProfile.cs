using System;

namespace entities.Interfaces
{
    public interface IProfile
    {
        Guid Id { get; set; }

        Guid UserFk { get; set; }
        IUser User { get; set; }

        string PasswordHash { get; set; }
    }
}
