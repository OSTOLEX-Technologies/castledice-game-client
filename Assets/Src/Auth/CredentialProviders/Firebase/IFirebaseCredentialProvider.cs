using System.Threading.Tasks;
using Firebase.Auth;

namespace Src.Auth.CredentialProviders.Firebase
{
    public interface IFirebaseCredentialProvider
    {
        public Task<Credential> GetCredentialAsync(AuthType authProviderType);
        
        /// <summary>
        /// Interrupt init process (port listening, occupying threads etc.)
        /// in case when unexpected scenario happens and other threads/OS modules
        /// cannot handle initialization process unattainability on their own.
        /// </summary>
        public void InterruptProviderInit();
    }
}