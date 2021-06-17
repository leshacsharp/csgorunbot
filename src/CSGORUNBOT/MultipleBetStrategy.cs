using System;
using System.Collections.Generic;
using System.Linq;

namespace CSGORUNBOT
{
    public class MultipleBetStrategy : IBetStrategy
    {
        private readonly IGameRepository _gameRepository;
        private readonly GameConfig _config;

        public MultipleBetStrategy(IGameRepository gameRepository, GameConfig config)
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
                Chance = _config.BetChance,
                Price = _config.DefaultPrice
            };
            possibleDirestions.Add(defaultDirection);

            if (currentBet != null)
            {
                var failDirection = new NeedToBetResponse()
                {
                    Bet = true,
                    Chance = _config.BetChance,
                    Price = Math.Round(currentBet.Price * _config.MultiplyPriceIfFail, 2, MidpointRounding.ToNegativeInfinity)
                };
                possibleDirestions.Add(failDirection);
            }

            return possibleDirestions;
        }

        public NeedToBetResponse NeedToBet()
        {
            var lastGames = _gameRepository.GetAll().TakeLast(_config.BetAfterNumberOfGames).ToList();
            var needToBet = lastGames.Count == _config.BetAfterNumberOfGames && lastGames.All(g => g.Chance < _config.BetIfChance);
            var response = new NeedToBetResponse() { Bet = needToBet };

            System.IO.File.AppendAllLines("D:/logs.txt", new[] { $"NeedToBet lastGames.count={lastGames.Count} needTobet={needToBet}", "----------" });

            if (needToBet)
            {
                response.Chance = _config.BetChance;
                var lastGameBet = lastGames.FirstOrDefault()?.MyBet;
                if (lastGameBet == null)
                {
                    response.Price = _config.DefaultPrice;
                }
                else if (!lastGameBet.IsSuccessed)
                {
                    response.Price = Math.Round(lastGameBet.Price * _config.MultiplyPriceIfFail, 2, MidpointRounding.ToNegativeInfinity);
                }
            }
            return response;
        }


    }
}
