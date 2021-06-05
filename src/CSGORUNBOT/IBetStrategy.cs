using System.Collections.Generic;

namespace CSGORUNBOT
{
    public interface IBetStrategy
    {
        public NeedToBetResponse NeedToBet();
        public List<NeedToBetResponse> GetPossibleDirections(GameBet currentBet);
    }
}
