using System;
using System.Collections.Generic;
using System.Linq;

namespace CSGORUNBOT
{
    public class LocalGameRepository : IGameRepository
    {
        private readonly List<Game> _games = new List<Game>();
        public void Add(Game game)
        {
            if (Get(game.Id) != null)
            {
                throw new ArgumentException($"Game with Id={game.Id} is already exists");
            }
            _games.Add(game);
        }

        public Game Get(string id)
        {
            return _games.SingleOrDefault(g => g.Id == id);
        }

        public IEnumerable<Game> GetAll()
        {
            return _games;
        }

        public void Update(Game game)
        {
            var storedGame = Get(game.Id);
            if (storedGame == null)
            {
                throw new ArgumentException($"Game with Id={game.Id} hasn't been found");
            }

            if (storedGame != null)
            {
                _games.Remove(storedGame);
                _games.Add(game);
            }
        }
    }
}
