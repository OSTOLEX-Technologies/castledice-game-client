using castledice_game_logic;
using static Tests.ObjectCreationUtility;
using castledice_game_logic.GameObjects;
using Moq;
using NUnit.Framework;
using Src.GameplayView.CellsContent.ContentCreation;
using Src.GameplayView.CellsContent.ContentViews;
using UnityEngine;

namespace Tests.PlayMode
{
    public class UnityContentViewProviderTests
    {
        public static Content[] Contents = { GetCastle(), GetKnight(), GetTree() };
        
        [Test]
        public void GetContentView_ShouldReturnView_WithAppropriateContent([ValueSource(nameof(Contents))]Content content)
        {
            var commonPrefabProviderMock = new Mock<ICommonContentViewPrefabProvider>();
            var playerPrefabProviderMock = new Mock<IPlayerContentViewPrefabProvider>();
            commonPrefabProviderMock.Setup(c => c.TreePrefab).Returns(GetTreeView());
            playerPrefabProviderMock.Setup(p => p.GetKnightPrefab(It.IsAny<Player>())).Returns(GetKnightView());
            playerPrefabProviderMock.Setup(p => p.GetCastlePrefab(It.IsAny<Player>())).Returns(GetCastleView());
            var go = new GameObject();
            var provider = go.AddComponent<UnityContentViewProvider>();
            provider.Init(playerPrefabProviderMock.Object, commonPrefabProviderMock.Object);
            var view = provider.GetContentView(content);

            Assert.AreSame(content, view.Content);
        }

        private TreeView GetTreeView()
        {
            var go = new GameObject();
            var view = go.AddComponent<TreeView>();
            return view;
        }
        
        private KnightView GetKnightView()
        {
            var go = new GameObject();
            var view = go.AddComponent<KnightView>();
            return view;
        }
        
        private CastleView GetCastleView()
        {
            var go = new GameObject();
            var view = go.AddComponent<CastleView>();
            return view;
        }
    }
}