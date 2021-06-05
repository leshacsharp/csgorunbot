using System.Collections.Generic;
using System.Linq;

namespace CSGORUNBOT
{
    public class BetStrategyV2 : IBetStrategy
    {
        private readonly IGameRepository _gameRepository;
        private readonly GameConfig _config;

        public BetStrategyV2(IGameRepository gameRepository, GameConfig config)
        {
            _gameRepository = gameRepository;
            _config = config;
        }

        public List<NeedToBetResponse> GetPossibleDirections(GameBet currentBet)
        {
            var possibleDirestions = new List<NeedToBetResponse>();

            var defaultDirection = new NeedToBetResponse()
            {
                Bet = true,
                Chance = _config.DefaultChance,
                Price = _config.DefaultPrice
            };
            possibleDirestions.Add(defaultDirection);
 
            if (currentBet != null)
            {
                var failDirection = new NeedToBetResponse()
                {
                    Bet = true,
                    Chance = _config.DefaultChance,
                    Price = currentBet.Price * _config.DefaultChance
                };
                possibleDirestions.Add(failDirection);
            }

            return possibleDirestions;
        }

        public NeedToBetResponse NeedToBet()
        {
            var lastGame = _gameRepository.GetAll().LastOrDefault();
            var response = new NeedToBetResponse() 
            { 
                Bet = true,
                Chance = _config.DefaultChance,
                Price = _config.DefaultPrice
            };

            var lastGameBet = lastGame.MyBet;
            if (lastGame?.MyBet?.IsSuccessed == false)
            {
                response.Price = lastGameBet.Price * response.Chance;
            }

            return response;
        }
    }
}
