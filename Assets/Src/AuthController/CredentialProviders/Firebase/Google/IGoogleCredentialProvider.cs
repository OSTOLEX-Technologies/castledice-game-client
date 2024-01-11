using System.Threading.Tasks;
using Src.AuthController.JwtManagement;

namespace Src.AuthController.CredentialProviders.Firebase.Google
{
    public interface IGoogleCredentialProvider
    {
        public Task<GoogleJwtStore> GetCredentialAsync();
    }
}