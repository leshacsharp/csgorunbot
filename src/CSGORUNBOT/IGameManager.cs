using System.Threading;

namespace CSGORUNBOT
{
    public interface IGameManager
    {
        void Start(CancellationToken cancellationToken);
    }
}
