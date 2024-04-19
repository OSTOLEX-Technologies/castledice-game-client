namespace Src.General.Caching
{
    public interface IObjectCacher
    {
        void CacheObject<T>(T obj);
    }
}