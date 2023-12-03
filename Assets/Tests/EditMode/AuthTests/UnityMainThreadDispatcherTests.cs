using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Src.AuthController;
using UnityEngine;

namespace Tests.EditMode.AuthTests
{
    public class UnityMainThreadDispatcherTests
    {
        private GameObject go;
        private UnityMainThreadDispatcher component;
        
        [SetUp]
        public void SetUp()
        {
            go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            component = go.AddComponent<UnityMainThreadDispatcher>();
        }
        
        [Test]
        public void Instance_ShouldKeepItself()
        {
            Assert.AreEqual(component, UnityMainThreadDispatcher.Instance());
        }

        [Test]
        public async Task Instance_ShouldNullItselfAfterDestroy()
        {
            GameObject.Destroy(go.gameObject);
            await Task.Delay(100);
            Assert.Throws<Exception>(() => UnityMainThreadDispatcher.Instance());
        }

        [Test]
        [TestCase(2, 10, 2, 6)]
        [TestCase(5, -11, 2, -3)]
        [TestCase(20, 0, 10, 2)]
        public async Task Queue_ShouldKeepOrder(int a, int b, int c, int res)
        {
            var action1 = new Action<int>((i) => { i++; });

            int i = 0;
            component.Enqueue(() => { i += a; Debug.Log("1"); });
            component.Enqueue(() => { i += b; Debug.Log("2");});
            component.Enqueue(() => { i /= c; Debug.Log("3");});
            await Task.Delay(100);
            Debug.Log("i: " + i);
            Assert.AreEqual(i, res);
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(go.gameObject);
        }
    }
}