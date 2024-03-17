using System.Threading.Tasks;
using castledice_game_logic;
using Src.Auth.REST;
using Src.Auth.TokenProviders;
using Src.Caching;

namespace Src.HttpUtils
{
    public class PlayerIdProvider : IPlayerIdProvider
    {
        private const string AuthServiceUrl = "http://auth.castledice.xyz:8000";
        
        public async Task<int> GetLocalPlayerId()
        {
            //TODO: extract magic string to config
            var httpIdRetriever = new HttpIdRetriever(
                AuthServiceUrl,
                new HttpClientRequestAdapter());
            var token = await Singleton<IAccessTokenProvider>.Instance.GetAccessTokenAsync();
            var id = await httpIdRetriever.RetrievePlayerIdAsync(token);
            return id;
        }
    }
}