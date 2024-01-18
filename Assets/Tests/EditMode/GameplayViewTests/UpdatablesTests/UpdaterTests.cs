using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.Updatables;

namespace Tests.EditMode.GameplayViewTests.UpdatablesTests
{
    public class UpdaterTests
    {
        private const string InnerListFieldName = "_updatables";
        
        [Test]
        public void AddUpdatable_ShouldAddGivenUpdatable_ToInnerList()
        {
            var updatable = new Mock<IUpdatable>().Object;
            var updater = new Updater();
            
            updater.AddUpdatable(updatable);
            var innerList = updater.GetPrivateField<List<IUpdatable>>(InnerListFieldName);
            
            Assert.Contains(updatable, innerList);
        }
        
        [Test]
        public void RemoveUpdatable_ShouldRemoveGivenUpdatable_FromInnerList()
        {
            var updatable = new Mock<IUpdatable>().Object;
            var updater = new Updater();
            
            updater.AddUpdatable(updatable);
            updater.RemoveUpdatable(updatable);
            var innerList = updater.GetPrivateField<List<IUpdatable>>(InnerListFieldName);
            
            Assert.IsEmpty(innerList);
        }
        
        [Test]
        public void Update_ShouldCallUpdate_OnAllUpdatables()
        {
            var updatablesCount = new Random().Next(1, 100);
            var updatableMocksList = CreateUpdatableMocksList(updatablesCount);
            var updater = new Updater();
            foreach (var updatableMock in updatableMocksList)
            {
                updater.AddUpdatable(updatableMock.Object);
            }
            
            updater.Update();
            
            foreach (var updatableMock in updatableMocksList)
            {
                updatableMock.Verify(u => u.Update(), Times.Once);
            }
        }
        
        private static List<Mock<IUpdatable>> CreateUpdatableMocksList(int count)
        {
            var updatableMocksList = new List<Mock<IUpdatable>>();
            for (var i = 0; i < count; i++)
            {
                var updatableMock = new Mock<IUpdatable>();
                updatableMocksList.Add(updatableMock);
            }

            return updatableMocksList;
        }
    }
}