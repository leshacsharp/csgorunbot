namespace CSGORUNBOT
{
    public class GameConfig
    {
        public int IntervaOfGames { get; set; }
        public int BetAfterNumberOfGames { get; set; }
        public decimal BetIfChance { get; set; }
        public decimal DefaultPrice { get; set; } = 0.25m;
        public decimal? DefaultPlusMinus { get; set; }
        public decimal? DefaultStep { get; set; }
        public decimal DefaultChance { get; set; }
    }
}
