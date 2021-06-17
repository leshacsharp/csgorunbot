using System.Collections.Generic;

namespace CSGORUNBOT
{
    //todo: make decimal? plusMinus, decimal? step as default settings for the bot

    public interface IBrowserBot
    {
        BuyResponse BuySkin(decimal price);
        BetResponse Bet(decimal price, decimal chance);
        bool HasSkin(decimal pricem);
        bool CanBet();
        Game GetPreviousGame();
        List<Skin> GetInventory();
        void ClearInventory();
        decimal GetBalance();
    }
}
