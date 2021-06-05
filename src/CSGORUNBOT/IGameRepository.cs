using System.Collections.Generic;

namespace CSGORUNBOT
{
    public interface IGameRepository
    {
        void Add(Game game);
        void Update(Game game);
        Game Get(string id);
        IEnumerable<Game> GetAll();
    }
}
