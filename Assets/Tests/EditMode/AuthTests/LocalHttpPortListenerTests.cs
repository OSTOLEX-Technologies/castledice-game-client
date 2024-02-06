using Moq;
using NUnit.Framework;
using Src.Auth.REST.PortListener;

namespace Tests.EditMode.AuthTests
{
    public class LocalHttpPortListenerTests
    {
        [Test]
        public void StartListening_ShouldFireCallback()
        {
            string expectedCode = "someComplexCode";
            string resultCode = "";
            
            var httpPortListenerHandler = new Mock<IHttpPortListenerHandler>();
            httpPortListenerHandler.Setup(a => a.Start()).Raises(
                x => x.OnListenerFired += null, expectedCode);
            
            var localHttpPortListener = new LocalHttpPortListener(httpPortListenerHandler.Object);
            localHttpPortListener.StartListening(receivedCode => resultCode = receivedCode);
            
            Assert.AreEqual(expectedCode, resultCode);
        }
    }
}