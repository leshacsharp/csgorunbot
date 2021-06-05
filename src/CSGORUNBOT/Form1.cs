using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSGORUNBOT
{
    public partial class Form1 : Form
    {
        private const string WebsiteUrl = "https://csgorun.pro/";
        private const string BrowserApp = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        private const int DebugPort = 9200;

        public Form1()
        {
            InitializeComponent();       
        }

        //run bot
        private void button1_Click(object sender, EventArgs e)
        {
            var options = new ChromeOptions() { DebuggerAddress = $"localhost:{DebugPort}" };
            var driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl(WebsiteUrl);

            var config = new GameConfig()
            {
                IntervaOfGames = 1000,
                BetAfterNumberOfGames = 2,
                BetIfChance = 2,
                DefaultPrice = 0.25m,
                DefaultPlusMinus = 0.05m,
                DefaultStep = 0.01m,
                DefaultChance = 2
            };

            var bot = new BrowserBot(driver, config);
            var gameRepository = new LocalGameRepository();
            var betStrategy = new BetStrategy(gameRepository, config);
            var gameManager = new GameManager(bot, betStrategy, gameRepository, config);

            var tokenSource = new CancellationTokenSource();
            gameManager.Start(tokenSource.Token);
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
            Environment.Exit(0);
        }
    }
}
