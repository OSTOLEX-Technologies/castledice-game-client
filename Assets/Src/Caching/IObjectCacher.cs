namespace Src.Caching
{
    public interface IObjectCacher
    {
        void CacheObject<T>(T obj);
    }
}