using Moq;
using NUnit.Framework;
using Src.GameplayView;

namespace Tests.EditMode
{
    public class GameCreationViewTests
    {
        [Test]
        public void ChooseCreateGame_ShouldRaiseCreateGameChosenEvent()
        {
            var view = new Mock<GameCreationView>().Object;
            bool eventRaised = false;
            view.CreateGameChosen += (sender, args) => eventRaised = true;
            
            view.ChooseCreateGame();
            
            Assert.True(eventRaised);
        }
        
        [Test]
        public void ChooseCancelGame_ShouldRaiseCancelCreationChosenEvent()
        {
            var view = new Mock<GameCreationView>().Object;
            bool eventRaised = false;
            view.CancelCreationChosen += (sender, args) => eventRaised = true;
            
            view.ChooseCancelGame();
                
            Assert.True(eventRaised);
        }
    }
}