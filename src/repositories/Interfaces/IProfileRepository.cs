using entities.Interfaces;

namespace repositories.Interfaces
{
    public interface IProfileRepository
    {
        IProfile GetObject(string name, string passwordHash);

        IProfile Create(IUser user, string passwordHash);
    }
}
