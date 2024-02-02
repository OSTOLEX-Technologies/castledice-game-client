using System.Threading.Tasks;
using Src.Auth.JwtManagement;

namespace Src.Auth.CredentialProviders.Firebase.Google
{
    public interface IGoogleCredentialProvider
    {
        public Task<GoogleJwtStore> GetCredentialAsync();
    }
}