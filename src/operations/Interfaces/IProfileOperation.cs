using entities.Interfaces;

namespace operations.Interfaces
{
    public interface IProfileOperation
    {
        IProfile Create(string username, string passwordHash);
    }
}
