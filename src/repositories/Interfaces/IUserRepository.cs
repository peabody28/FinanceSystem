using entities.Interfaces;
using System.Collections.Generic;

namespace repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<IUser> Read();

        IUser Create(string name);

        IUser GetObject(string name);
    }
}
