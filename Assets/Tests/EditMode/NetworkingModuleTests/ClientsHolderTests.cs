using System;
using NUnit.Framework;
using Riptide;
using Src.NetworkingModule;

namespace Tests.EditMode.NetworkingModuleTests
{
    public class ClientsHolderTests
    {
        [Test]
        public void AddClient_ShouldThrowInvalidOperationException_IfClientWithGivenTypeAlreadyAdded()
        {
            var firstClient = new ClientWrapper(new Client());
            var secondClient = new ClientWrapper(new Client());
            ClientsHolder.AddClient(ClientType.GameServerClient, firstClient);
            
            Assert.Throws<InvalidOperationException>(() => ClientsHolder.AddClient(ClientType.GameServerClient, secondClient));
            ClientsHolder.RemoveClient(ClientType.GameServerClient);
        }
        
        [Test]
        public void HasClient_ShouldReturnFalse_IfNoClientWithGivenType()
        {
            Assert.IsFalse(ClientsHolder.HasClient(ClientType.GameServerClient));
        }
        
        [Test]
        public void HasClient_ShouldReturnTrue_IfClientWithGivenTypeWasAdded()
        {
            var client = new ClientWrapper(new Client());
            ClientsHolder.AddClient(ClientType.GameServerClient, client);
            
            Assert.IsTrue(ClientsHolder.HasClient(ClientType.GameServerClient));
            ClientsHolder.RemoveClient(ClientType.GameServerClient);
        }
        
        [Test]
        public void GetClient_ShouldThrowInvalidOperationException_IfNoClientWithGivenType()
        {
            Assert.Throws<InvalidOperationException>(() => ClientsHolder.GetClient(ClientType.GameServerClient));
        }
        
        [Test]
        public void GetClient_ShouldReturnClient_IfClientForThisTypeWasAdded()
        {
            var client = new ClientWrapper(new Client());
            ClientsHolder.AddClient(ClientType.GameServerClient, client);
            
            Assert.AreSame(client, ClientsHolder.GetClient(ClientType.GameServerClient));
            ClientsHolder.RemoveClient(ClientType.GameServerClient);
        }

        [Test]
        public void RemoveClient_ShouldThrowInvalidOperationException_IfClientWithGivenTypeIsAbsent()
        {
            Assert.Throws<InvalidOperationException>(() => ClientsHolder.RemoveClient(ClientType.GameServerClient));
        }

        [Test]
        public void RemoveClient_ShouldRemoveClient_IfClientWithGivenTypeWasAdded()
        {
            ClientsHolder.AddClient(ClientType.GameServerClient, new ClientWrapper(new Client()));
            
            ClientsHolder.RemoveClient(ClientType.GameServerClient);
            
            Assert.Throws<InvalidOperationException>(() => ClientsHolder.GetClient(ClientType.GameServerClient));
        }
    }
}