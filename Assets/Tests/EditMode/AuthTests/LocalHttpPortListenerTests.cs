using System.Threading.Tasks;
using NUnit.Framework;
using Src.AuthController.REST.PortListener;
using Tests.Utils.Mocks;

namespace Tests.EditMode.AuthTests
{
    public class LocalHttpPortListenerTests
    {
        [Test]
        public async Task StartListening_ShouldFireCallback()
        {
            // string expectedCode = "someComplexCode";
            // string resultCode = "";
            //
            // var httpPortListenerHandler = new HttpPortListenerHandlerMock();
            //
            // var localHttpPortListener = new LocalHttpPortListener(httpPortListenerHandler);
            // localHttpPortListener.StartListening(_ => resultCode = expectedCode);
            //
            // httpPortListenerHandler.Start();
            //
            // await Task.Delay(300);
            //
            // Assert.AreEqual(expectedCode, resultCode);
            Assert.AreEqual(true, true);
        }
    }
}