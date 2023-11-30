using System;
using NUnit.Framework;
using Src;
using Src.Caching;

namespace Tests.EditMode
{
    public class SingletonTests
    {
        public class TestClass
        {
        }
        
        [Test]
        public void Register_ShouldThrowInvalidOperationException_IfObjectOfGivenTypeIsAlreadyRegistered()
        {
            var obj = new TestClass();
            Singleton<TestClass>.Register(obj);
            
            Assert.Throws<InvalidOperationException>(() => Singleton<TestClass>.Register(obj));
            Singleton<TestClass>.Unregister();
        }
        
        [Test]
        public void Register_ShouldSetInstance_IfObjectOfGivenTypeIsNotRegistered()
        {
            var obj = new TestClass();
            Singleton<TestClass>.Register(obj);
            
            Assert.AreSame(obj, Singleton<TestClass>.Instance);
            Singleton<TestClass>.Unregister();
        }
        
        [Test]
        public void Unregister_ShouldThrowInvalidOperationException_IfObjectOfGivenTypeIsNotRegistered()
        {
            Assert.Throws<InvalidOperationException>(Singleton<TestClass>.Unregister);
        }
        
        [Test]
        public void InstanceProperty_ShouldThrowInvalidOperationException_IfObjectOfGivenTypeIsNotRegistered()
        {
            Assert.Throws<InvalidOperationException>(() => { _ = Singleton<TestClass>.Instance; });
        }

        [Test]
        public void RegisteredProperty_ShouldReturnFalse_IfObjectIsNotRegistered()
        {
            Assert.False(Singleton<TestClass>.Registered);
        }

        [Test]
        public void RegisteredProperty_ShouldReturnTrue_IfObjectIsRegistered()
        {
            Singleton<TestClass>.Register(new TestClass());
            
            Assert.True(Singleton<TestClass>.Registered);
            Singleton<TestClass>.Unregister();
        }
    }
}