namespace CSGORUNBOT
{
    public class Game
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public decimal Chance { get; set; }
        public GameBet MyBet { get; set; }
    }
}
