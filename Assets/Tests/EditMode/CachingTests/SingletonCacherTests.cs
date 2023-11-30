using NUnit.Framework;
using Src.Caching;

namespace Tests.EditMode.CachingTests
{
    public class SingletonCacherTests
    {
        private class TestClass
        {
            
        }
        
        [Test]
        public void CacheObject_ShouldRegisterObjectToSingleton()
        {
            var obj = new TestClass();
            var cacher = new SingletonCacher();
            
            cacher.CacheObject(obj);
            
            Assert.True(Singleton<TestClass>.Registered);
        }

        [Test]
        public void CacheObject_ShouldSetNewInstanceOfObject_IfSomeObjectAlreadyRegistered()
        {
            var registeredObj = new TestClass();
            var newObj = new TestClass();
            Singleton<TestClass>.Register(registeredObj);
            var cacher = new SingletonCacher();
            
            cacher.CacheObject(newObj);
            
            Assert.AreSame(newObj, Singleton<TestClass>.Instance);
        }

        [TearDown]
        public void UnregisterSingleton()
        {
            if (Singleton<TestClass>.Registered)
            {
                Singleton<TestClass>.Unregister();
            }
        }
    }
}