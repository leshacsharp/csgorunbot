using System;
using System.Linq;
using System.Windows.Forms;

namespace CSGORUNBOT
{
    public class GameManager : IGameManager
    {
        private readonly IBrowserBot _bot;
        private readonly IBetStrategy _betStrategy;
        private readonly IGameRepository _gameRepository;
        private readonly GameConfig _config;
        private Timer _gameTimer;
        private Timer _prepareTimer;

        private GameBet _currentBet;
        private decimal _maxBalance;

        public GameManager(
            IBrowserBot bot,
            IBetStrategy betStrategy,
            IGameRepository gameRepository,
            GameConfig config)
        {
            _bot = bot;
            _betStrategy = betStrategy;
            _gameRepository = gameRepository;
            _config = config;
        }

        public void Start()
        {
            _gameTimer = new Timer();
            _gameTimer.Interval = _config.IntervaOfGames;
            _gameTimer.Tick += Play;
            _gameTimer.Start();

            _prepareTimer = new Timer();
            _prepareTimer.Interval = 500; // todo: move to the settings
            _prepareTimer.Tick += PrepareToGame;
            _prepareTimer.Start();

            _maxBalance = _bot.GetBalance() + (_config.MaxProfit ?? default);
        }

        private void PrepareToGame(object sender, EventArgs e)
        {
            var currentBalance = _bot.GetBalance();
            if (_config.MaxProfit.HasValue && currentBalance >= _maxBalance)
            {
                System.IO.File.AppendAllLines("D:/logs.txt", new[] { $"PrepareToGame stop bot curBalance={currentBalance}" });
                _gameTimer?.Stop();
                _prepareTimer?.Stop();
                return;
            }
 
            var possibleGameDirections = _betStrategy.GetPossibleDirections(_currentBet);
            var possibleGameDirectionsPrices = possibleGameDirections.Select(d => d.Price).ToList();
            System.IO.File.AppendAllLines("D:/logs.txt", new[] { $"PrepareToGame DirectionsPrices = {string.Join(";", possibleGameDirectionsPrices)}"});
            System.IO.File.AppendAllLines("D:/logs.txt", new[] { $"PrepareToGame curBalance={currentBalance}" });

            foreach (var direction in possibleGameDirections)
            {
                if (direction.Bet && !_bot.HasSkin(direction.Price))
                {
                    _bot.BuySkin(direction.Price);
                }
            }            
        }

        private void Play(object sender, EventArgs e)
        {
            System.IO.File.AppendAllLines("D:/logs.txt", new[] { $"-----------------------------------time={DateTime.Now}--------------------------------" });
 
            var previousGame = _bot.GetPreviousGame();
            var storedGame = _gameRepository.Get(previousGame.Id);

            if (storedGame == null)
            {
                if (_currentBet != null)
                {
                    _currentBet.IsSuccessed = previousGame.Chance >= _currentBet.Chance;
                    previousGame.MyBet = _currentBet;          
                    _currentBet = null;
                }
              
                _gameRepository.Add(previousGame);  
                System.IO.File.AppendAllLines("D:/logs.txt", new[] { $"_gameRepository.Add - chance:{previousGame.Chance}" });
            }

            var needToBet = _betStrategy.NeedToBet();
            if (needToBet.Bet && _bot.CanBet())
            {
                System.IO.File.AppendAllLines("D:/logs.txt", new[] { "need to bet, can bet " });
                if (_bot.HasSkin(needToBet.Price))
                {
                    System.IO.File.AppendAllLines("D:/logs.txt", new[] { "has skin " });
                    var betResponse = _bot.Bet(needToBet.Price, needToBet.Chance);
                    if (betResponse.Successed)
                    {
                        _currentBet = new GameBet() { Chance = needToBet.Chance, Price = needToBet.Price };
                        _bot.ClearInventory();
                    }
                }
                else
                {
                    var buyResponse = _bot.BuySkin(needToBet.Price);
                    if (buyResponse.Successed)
                    {
                        System.IO.File.AppendAllLines("D:/logs.txt", new[] { "buy skin "});
                        var betResponse = _bot.Bet(buyResponse.Price, needToBet.Chance);
                        if (betResponse.Successed)
                        {
                            _currentBet = new GameBet() { Chance = needToBet.Chance, Price = buyResponse.Price };
                            _bot.ClearInventory();
                        }
                    }
                }
            }
        }

        public void Stop()
        {
            _gameTimer?.Stop();
            _prepareTimer?.Stop();
        }
    }
}