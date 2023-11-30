using System;

namespace Src.Caching
{
    public static class Singleton<T>
    {
        private static bool _registered = false;
        private static T _instance;

        public static bool Registered => _registered;

        public static T Instance
        {
            get
            {
                if (_registered)
                {
                    return _instance;
                }
                throw new InvalidOperationException("Singleton not registered for type " + typeof(T).Name);
            }
            private set => _instance = value;
        }
        
        public static void Register(T instance)
        {
            if (_registered)
            {
                throw new InvalidOperationException("Singleton already registered for type " + typeof(T).Name);
            }

            Instance = instance;
            _registered = true;
        }
        
        public static void Unregister()
        {
            if (!_registered)
            {
                throw new InvalidOperationException("Singleton not registered for type " + typeof(T).Name);
            }

            Instance = default;
            _registered = false;
        }
    }
}