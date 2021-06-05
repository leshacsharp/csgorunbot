using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CSGORUNBOT
{
    public class BrowserBot : IBrowserBot
    {
        private readonly RemoteWebDriver _webDriver;
        private readonly GameConfig _config;

        public BrowserBot(
            RemoteWebDriver webDriver,
            GameConfig config)
        {
            _webDriver = webDriver;
            _config = config;
        }
   
        public BetResponse Bet(decimal price, decimal chance)
        {
            Thread.Sleep(300);

            var response = new BetResponse();       
            var inventoryPrices = _webDriver.FindElementsByCssSelector(".cur-u-drops-list .drop-preview__price").Select(e => ParsePrice(e.Text)).ToList(); 
            var possiblePricesToBet = PriceRange(price, _config.DefaultPlusMinus, _config.DefaultStep);

            if (!inventoryPrices.Any(ip => possiblePricesToBet.Contains(ip)))
            {
                System.IO.File.AppendAllLines("D:/logs.txt", new[] { $"Bet There is not a approprited skin to bet. Buy it before betting.", "----------" });
                response.Reasons.Add($"There is not a approprited skin to bet. Buy it before betting.");
                return response;
            }

            var inventorySkins = _webDriver.FindElementsByCssSelector(".cur-u-drops-list button");
            var priceToBet = possiblePricesToBet.First(p => inventoryPrices.Contains(p));
            var skinToBet = inventorySkins.First(s => ParsePrice(s.FindElement(By.CssSelector(".drop-preview__price")).Text) == priceToBet);
            skinToBet.Click();

            var chanceInput = _webDriver.FindElementByCssSelector("#auto-upgrade-input");
            var chanceInputValue = chanceInput.GetAttribute("value");
            chanceInput.SendKeys(string.Concat(Enumerable.Repeat(Keys.Backspace, chanceInputValue.Length)));
            chanceInput.SendKeys(chance.ToString()); 

            var makeBetButton = _webDriver.FindElementByCssSelector("button.make-bet");
            makeBetButton.Click();

            Thread.Sleep(300);

            var betIsAccepted = double.Parse(_webDriver.FindElementByCssSelector(".game-info-bet__count > span").Text.Replace(".", ",")) > 0;
            response.Successed = betIsAccepted;
            System.IO.File.AppendAllLines("D:/logs.txt", new[] { $"Bet click bet button accepted={betIsAccepted}", "----------" });

            return response;    
        }

        public BuyResponse BuySkin(decimal price)
        {
            var dropButton = _webDriver.FindElementByCssSelector(".cur-u-drops-btns .btn--green");
            dropButton.Click();

            var skinPrice = price;
            var isSkinPicked = TryPickSkin(price);

            if (!isSkinPicked && _config.DefaultPlusMinus.HasValue && _config.DefaultStep.HasValue)
            {
                var plusMinus = _config.DefaultPlusMinus.Value;
                var step = _config.DefaultStep.Value;

                for (var i = price - step; i >= price - plusMinus; i -= step)
                {
                    if (isSkinPicked = TryPickSkin(i))
                    {
                        skinPrice = i;
                        break;
                    }
                }
                if (!isSkinPicked)
                {
                    for (var i = price + step; i <= price + plusMinus; i += step)
                    {
                        if (isSkinPicked = TryPickSkin(i))
                        {
                            skinPrice = i;
                            break;
                        }
                    }
                }
            }

            var response = new BuyResponse();
            if (isSkinPicked)
            {
                var buyButton = _webDriver.FindElementByCssSelector(".withdraw-form-top .hide-below-m");
                buyButton.Click();

                Thread.Sleep(100);

                response.Price = skinPrice;
                response.Successed = true;
            }

            dropButton = _webDriver.FindElementByCssSelector(".cur-u-drops-btns .btn--green");
            dropButton.Click();

            return response;       
        }

        public Game GetPreviousGame()
        {
            var previousGame = _webDriver.FindElementByCssSelector(".graph-labels .graph-label:first-child");
            var gameUrl = new Uri(previousGame.GetAttribute("href"));
            var gameId = gameUrl.Segments.LastOrDefault();
            var gameChance = decimal.Parse(previousGame.Text.Replace("x", string.Empty).Replace(".", ","));

            return new Game()
            {
                Id = gameId,
                Url = gameUrl.ToString(),
                Chance = gameChance
            };
        }

        public bool CanBet()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(400)) { PollingInterval = TimeSpan.FromMilliseconds(200) };
            var counter = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".graph-svg__counter"))).Text;

            var betsAreInProgress = counter.Contains("s");
            var haveTimeToBet = betsAreInProgress && double.Parse(counter.Replace("s", string.Empty).Replace(".", ",")) >= 2;
            var betCount = double.Parse(_webDriver.FindElementByCssSelector(".game-info-bet__count > span").Text.Replace(".", ","));
            var betAlreadyAccepted = betCount > 0;

            System.IO.File.AppendAllLines("D:/logs.txt", new[] { $"CanBet={betsAreInProgress && haveTimeToBet && !betAlreadyAccepted} betCount={betCount}" });

            return betsAreInProgress && haveTimeToBet && !betAlreadyAccepted;
        }

        public bool HasSkin(decimal price)
        {
            var possibleSkinPrices = PriceRange(price, _config.DefaultPlusMinus, _config.DefaultStep);
            var inventoryPrices = _webDriver.FindElementsByCssSelector(".cur-u-drops-list .drop-preview__price").Select(e => ParsePrice(e.Text)).ToList();

            System.IO.File.AppendAllLines("D:/logs.txt", new[] { $"HasSkin {string.Join(",", inventoryPrices)}"});

            return inventoryPrices.Any(p => possibleSkinPrices.Contains(p));
        }

        private bool TryPickSkin(decimal price)
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(300)) { PollingInterval = TimeSpan.FromMilliseconds(150) };
            var maxPriceInput = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#exchange-filter-maxPrice-field")));
            var minPriceInput = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#exchange-filter-minPrice-field")));

            maxPriceInput.Clear();
            minPriceInput.Clear();
            maxPriceInput.SendKeys(price.ToString());
            minPriceInput.SendKeys(price.ToString());

            try
            {
                Thread.Sleep(200);  
                var skin = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".withdraw-list__inner button:first-child")));
                skin.Click();
                return true;       
            }
            catch (WebDriverTimeoutException) { }
            return false;
        }   
        
        public void ClearInventory()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(400));
            var selectAllButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".checkbox-control__content")));
            selectAllButton.Click();

            BuySkin(_config.DefaultPrice);
        }

        private decimal ParsePrice(string price)
        {
            return decimal.Parse(price.Replace("$", string.Empty).Replace(".", ","));
        }

        private List<decimal> PriceRange(decimal price, decimal? plusMinus, decimal? step)
        {
            var priceRange = new List<decimal>();
            priceRange.Add(price);

            if (plusMinus.HasValue && step.HasValue)
            {
                for (var i = price - step.Value; i >= price - plusMinus; i -= step.Value)
                {
                    priceRange.Add(i);
                }
                for (var i = price + step.Value; i <= price + plusMinus; i += step.Value)
                {
                    priceRange.Add(i);
                }
            }

            return priceRange;
        }

        public List<Skin> GetInventory()
        {
            Thread.Sleep(200);
            var inventory = new List<Skin>();
            var skinsElements = _webDriver.FindElementsByCssSelector(".cur-u-drops-list button");

            foreach (var skinElement in skinsElements)
            {
                var title = skinElement.FindElement(By.CssSelector(".drop-preview__title")).Text;
                var subTitle = skinElement.FindElement(By.CssSelector(".drop-preview__subtitle")).Text;
                var info = skinElement.FindElement(By.CssSelector(".drop-preview__desc")).Text;
                var price = skinElement.FindElement(By.CssSelector(".drop-preview__price")).Text;

                var skin = new Skin()
                {
                    Name = $"{title} {subTitle}",
                    Information = info,
                    Price = ParsePrice(price)
                };
                inventory.Add(skin);
            }

            return inventory;
        }
    }
}
