using System.Collections.Generic;

namespace CrudsWithNancyFx.Models.Persistence
{
    public interface IServerDataRepository
    {
        ServerData Get(int id);
        IEnumerable<ServerData> GetAll();
        ServerData Add(ServerData serverData);
        void Delete(int id);
        bool Update(ServerData serverData);

    }
}
