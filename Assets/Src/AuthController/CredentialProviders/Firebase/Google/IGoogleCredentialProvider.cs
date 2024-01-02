using System.Threading.Tasks;
using Src.AuthController.REST.REST_Response_DTOs;
using Src.AuthController.REST.REST_Response_DTOs.Firebase.Google;

namespace Src.AuthController.CredentialProviders.Firebase.Google
{
    public interface IGoogleCredentialProvider
    {
        public Task<GoogleIdTokenResponse> GetCredentialAsync();
    }
}