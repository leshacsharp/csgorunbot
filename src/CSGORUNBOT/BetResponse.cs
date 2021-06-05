using System.Collections.Generic;

namespace CSGORUNBOT
{
    public class BetResponse
    {
        public BetResponse()
        {
            Reasons = new List<string>();
        }

        public bool Successed { get; set; }
        public List<string> Reasons { get; set; }
    }
}
