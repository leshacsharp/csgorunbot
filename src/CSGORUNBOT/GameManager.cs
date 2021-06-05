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
        private Timer _timer;
        private Timer _timer2;

        private GameBet _currentBet;

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

        public void Start(System.Threading.CancellationToken cancellationToken)
        {
            _timer = new Timer();
            _timer.Interval = _config.IntervaOfGames;
            _timer.Tag = cancellationToken;
            _timer.Tick += Play;
            _timer.Start();

            _timer2 = new Timer();
            _timer2.Interval = 500; // todo: move to the settings
            _timer2.Tag = cancellationToken;
            _timer2.Tick += PrepareToGame;
            _timer2.Start();
        }

        private void PrepareToGame(object sender, EventArgs e)
        {
            var cancellationToken = (System.Threading.CancellationToken)_timer.Tag;
            cancellationToken.ThrowIfCancellationRequested();

            var possibleGameDirections = _betStrategy.GetPossibleDirections(_currentBet);
            var possibleGameDirectionsPrices = possibleGameDirections.Select(d => d.Price).ToList();
            System.IO.File.AppendAllLines("D:/logs.txt", new[] { $"PrepareToGame DirectionsPrices = {string.Join(";", possibleGameDirectionsPrices)}"});

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
            System.IO.File.AppendAllLines("D:/logs.txt", new[] { "-------------------------------------------------------------------" });

            var cancellationToken = (System.Threading.CancellationToken)_timer.Tag;
            cancellationToken.ThrowIfCancellationRequested();
 
            var previousGame = _bot.GetPreviousGame();
            var storedGame = _gameRepository.Get(previousGame.Id);

            if (storedGame == null)
            {
                if (_currentBet != null)
                {
                    _currentBet.IsSuccessed = previousGame.Chance >= _currentBet.Chance;
                    previousGame.MyBet = _currentBet;          
                    _currentBet = null;
                    System.IO.File.AppendAllLines("D:/logs.txt", new[] { $"CurrentBet = null " });
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
                        System.IO.File.AppendAllLines("D:/logs.txt", new[] { $"CurrentBet = new bet " });
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
                            System.IO.File.AppendAllLines("D:/logs.txt", new[] { $"CurrentBet = new bet " });
                            _currentBet = new GameBet() { Chance = needToBet.Chance, Price = buyResponse.Price };
                            _bot.ClearInventory();
                        }
                    }
                }
            }
        }
    }
}