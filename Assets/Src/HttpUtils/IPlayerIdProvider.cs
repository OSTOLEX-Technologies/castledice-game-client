using System.Threading.Tasks;

namespace Src.HttpUtils
{
    public interface IPlayerIdProvider
    {
        public Task<int> GetLocalPlayerId();
    }
}