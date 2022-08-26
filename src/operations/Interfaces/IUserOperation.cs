using entities.Interfaces;
using System.Collections.Generic;

namespace operations.Interfaces
{
    public interface IUserOperation
    {
        IEnumerable<IUser> Read();

        IUser GetObject(string name);

        IUser Create(string name);
    }
}
