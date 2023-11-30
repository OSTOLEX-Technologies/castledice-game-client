namespace Src.AuthController
{
    public interface IFirebaseTokenProvidersFactory
    {
        public FirebaseTokenProvider GetTokenProvider(FirebaseAuthProviderType authProviderType);
    }
}