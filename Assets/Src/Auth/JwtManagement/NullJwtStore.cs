namespace Src.Auth.JwtManagement
{
    public class NullJwtStore : AbstractJwtStore
    {
        public static AbstractJwtStore Instance { get; } = new NullJwtStore();

        public NullJwtStore() {}
    }
}