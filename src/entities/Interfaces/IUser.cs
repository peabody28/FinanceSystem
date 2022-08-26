using System;

namespace entities.Interfaces
{
    public interface IUser
    {
        Guid Id { get; set; }

        string Name { get; set; }
    }
}
