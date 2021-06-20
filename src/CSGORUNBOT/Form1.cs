using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace CSGORUNBOT
{
    public partial class Form1 : Form
    {
        private const string WebsiteUrl = "https://csgorun.pro/";
        private const string BrowserApp = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        private const int DebugPort = 9200;

        private IGameManager _gameManager;

        public Form1()
        {
            InitializeComponent();       
        }

        //run bot
        private void button1_Click(object sender, EventArgs e)
        {         
            if (!int.TryParse(betAfterGamesTextBox.Text, out var betAfterNumberOfGames) ||
                !decimal.TryParse(betIfChanceTextBox.Text, out var betIfChance) ||
                !decimal.TryParse(defaultPricetextBox.Text, out var defaultPrice) ||
                !decimal.TryParse(defaultPlusMinusTextBox.Text, out var defaultPlusMinus) ||
                !decimal.TryParse(defaultStepTextBox.Text, out var defaultStep) ||
                !decimal.TryParse(betChanceTextBox.Text, out var betChance) ||
                !decimal.TryParse(multiplyPriceTextBox.Text, out var multiplyPriceIfFail) ||
                !decimal.TryParse(maxProfitTextBox.Text, out var maxProfit))
            {
                MessageBox.Show("check your settings on correctness");
                return;
            }

            var config = new GameConfig()
            {
                IntervaOfGames = 1000,
                BetAfterNumberOfGames = betAfterNumberOfGames, //for multiply strategy
                BetIfChance = betIfChance,                     //for multiply strategy
                DefaultPrice = defaultPrice,
                DefaultPlusMinus = defaultPlusMinus,
                DefaultStep = defaultStep,
                BetChance = betChance,
                MultiplyPriceIfFail = multiplyPriceIfFail,
                MaxProfit = maxProfit
            };

            //var config = new GameConfig()
            //{
            //    IntervaOfGames = 1000,
            //    BetAfterNumberOfGames = 1, //for multiply strategy
            //    BetIfChance = 2,           //for multiply strategy
            //    DefaultPrice = 0.30m,
            //    DefaultPlusMinus = 0.15m,
            //    DefaultStep = 0.01m,
            //    BetChance = 1.2m,
            //    MultiplyPriceIfFail = 5,
            //    MaxProfit = 10m
            //};

            //var config = new GameConfig()
            //{
            //    IntervaOfGames = 1000,
            //    BetAfterNumberOfGames = 1, //for multiply strategy
            //    BetIfChance = 2,           //for multiply strategy
            //    DefaultPrice = 0.60m,
            //    DefaultPlusMinus = 0.30m,
            //    DefaultStep = 0.01m,
            //    BetChance = 1.3m,
            //    MultiplyPriceIfFail = 4,
            //    MaxProfit = 20m
            //};

            var options = new ChromeOptions() { DebuggerAddress = $"localhost:{DebugPort}" };
            var driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl(WebsiteUrl);

            var bot = new BrowserBot(driver, config);
            var gameRepository = new LocalGameRepository();
            //var betStrategy = new MultipleBetStrategy(gameRepository, config);
            var betStrategy = new EveryBetStrategy(gameRepository, config);

            _gameManager = new GameManager(bot, betStrategy, gameRepository, config);
            _gameManager.Start();
        }

        //launch browser
        private void button2_Click(object sender, EventArgs e)
        {
            var proc = new Process();
            proc.StartInfo.FileName = BrowserApp;
            proc.StartInfo.Arguments = @$"--remote-debugging-port={DebugPort} --user-data-dir=D:\projects\CSGORUNBOT\TEMP";
            proc.Start();
        }

        //close app
        private void button3_Click(object sender, EventArgs e)
        {
            _gameManager?.Stop();
            //Environment.Exit(0);
        }

        //default settings 1
        private void button4_Click(object sender, EventArgs e)
        {
            //todo: move the settings to appsettins file
            betAfterGamesTextBox.Text = 1.ToString();
            betIfChanceTextBox.Text = 2.ToString();
            defaultPricetextBox.Text = 0.60m.ToString();
            defaultPlusMinusTextBox.Text = 0.30m.ToString();
            defaultStepTextBox.Text = 0.01m.ToString();
            betChanceTextBox.Text = 1.3m.ToString();
            multiplyPriceTextBox.Text = 4m.ToString();
            maxProfitTextBox.Text = 20m.ToString();
        }

        //default settings 2
        private void button5_Click(object sender, EventArgs e)
        {
            //todo: move the settings to appsettins file
            betAfterGamesTextBox.Text = 1.ToString();
            betIfChanceTextBox.Text = 2.ToString();
            defaultPricetextBox.Text = 0.25m.ToString();
            defaultPlusMinusTextBox.Text = 0.15m.ToString();
            defaultStepTextBox.Text = 0.01m.ToString();
            betChanceTextBox.Text = 2m.ToString();
            multiplyPriceTextBox.Text = 2m.ToString();
            maxProfitTextBox.Text = 10m.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }
    }
}
