using static Tests.Utils.ObjectCreationUtility;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using Moq;

namespace Tests.Utils
{
    public static class ContentMocksCreationUtility
    {
        public static Content GetPlayerOwnedContent()
        {
            return GetPlayerOwnedContent(GetPlayer());
        }
        
        public static Content GetPlayerOwnedContent(Player player)
        {
            var mockContent = new Mock<Content>();
            var mockPlayerOwned = mockContent.As<IPlayerOwned>();
            mockPlayerOwned.Setup(x => x.GetOwner()).Returns(player);
            return mockContent.Object;
        }

        public static Content GetNotRemovableContent()
        {
            var mockContent = new Mock<Content>();
            var mockRemovable = mockContent.As<IRemovable>();
            mockRemovable.Setup(x => x.CanBeRemoved()).Returns(false);
            return mockContent.Object;
        }
        
        public static Content GetRemovableContent(int removeCost)
        {
            var mockContent = new Mock<Content>();
            var mockRemovable = mockContent.As<IRemovable>();
            mockRemovable.Setup(x => x.CanBeRemoved()).Returns(true);
            mockRemovable.Setup(x => x.GetRemoveCost()).Returns(removeCost);
            return mockContent.Object;
        }

        public static Content GetUndefinedContent()
        {
            var contentMock = new Mock<Content>();
            return contentMock.Object;
        }

        public static Content GetReplaceableContent(int replaceCost)
        {
            var contentMock = new Mock<Content>();
            var replaceableMock = contentMock.As<IReplaceable>();
            replaceableMock.Setup(x => x.GetReplaceCost()).Returns(replaceCost);
            return contentMock.Object;
        }

        public static Content GetCapturableContent(int captureHitCost, Player forPlayer)
        {
            var contentMock = new Mock<Content>();
            var capturableMock = contentMock.As<ICapturable>();
            capturableMock.Setup(x => x.GetCaptureHitCost(forPlayer)).Returns(captureHitCost);
            return contentMock.Object;
        }

        public static IPlaceable GetPlaceablePlayerOwned(Player owner)
        {
            var placeableMock = new Mock<IPlaceable>();
            var playerOwnedMock = placeableMock.As<IPlayerOwned>();
            playerOwnedMock.Setup(x => x.GetOwner()).Returns(owner);
            return placeableMock.Object;
        }

        public static IPlaceable GetPlaceableReplaceable(int replaceCost)
        {
            var placeableMock = new Mock<IPlaceable>();
            var replaceableMock = placeableMock.As<IReplaceable>();
            replaceableMock.Setup(x => x.GetReplaceCost()).Returns(replaceCost);
            return placeableMock.Object;
        }
        
        public static IPlaceable GetPlaceableRemovable(int removeCost)
        {
            var placeableMock = new Mock<IPlaceable>();
            var removableMock = placeableMock.As<IRemovable>();
            removableMock.Setup(x => x.CanBeRemoved()).Returns(true);
            removableMock.Setup(x => x.GetRemoveCost()).Returns(removeCost);
            return placeableMock.Object;
        }
        
        public static IPlaceable GetPlaceableCapturable(int captureHitCost, Player forPlayer)
        {
            var placeableMock = new Mock<IPlaceable>();
            var capturableMock = placeableMock.As<ICapturable>();
            capturableMock.Setup(x => x.GetCaptureHitCost(forPlayer)).Returns(captureHitCost);
            return placeableMock.Object;
        }

        public static IPlaceable GetPlaceableNotRemovable()
        {
            var placeableMock = new Mock<IPlaceable>();
            var removableMock = placeableMock.As<IRemovable>();
            removableMock.Setup(x => x.CanBeRemoved()).Returns(false);
            return placeableMock.Object;
        }
    }
}