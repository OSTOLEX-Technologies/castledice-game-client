namespace Src.General.Caching
{
    public class SingletonCacher : IObjectCacher
    {
        public void CacheObject<T>(T obj)
        {
            if (Singleton<T>.Registered)
            {
                Singleton<T>.Unregister();
            }
            Singleton<T>.Register(obj);
        }
    }
}