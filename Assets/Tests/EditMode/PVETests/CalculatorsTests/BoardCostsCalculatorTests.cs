using System;
using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.PVE.Calculators;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests.CalculatorsTests
{
    public class BoardCostsCalculatorTests
    {
        [Test]
        public void GetCosts_ShouldGiveMinimalPlaceCost_ToEmptyCells()
        {
            var board = GetFullNByNBoard(2);
            var minimalPlaceCost = new Random().Next(1, 100);
            var expectedCostsMatrix = new int[,]
            {
                {minimalPlaceCost, minimalPlaceCost},
                {minimalPlaceCost, minimalPlaceCost}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = minimalPlaceCost
            }.Build();
            
            var actualCostsMatrix = calculator.GetCosts(GetPlayer());
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }

        [Test]
        public void GetCosts_ShouldGiveIntMaxValueAsCost_ToCellsWithPlayerContent()
        {
            var player = GetPlayer();
            var board = GetFullNByNBoard(2);
            var firstContent = GetPlayerOwnedContent(player);
            var secondContent = GetPlayerOwnedContent(player);
            board[0, 0].AddContent(firstContent);
            board[1, 0].AddContent(secondContent);
            var minimalPlaceCost = 1;
            var expectedCostsMatrix = new int[,]
            {
                {int.MaxValue, 1},
                {int.MaxValue, 1}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = minimalPlaceCost,
            }.Build();
            
            var actualCostsMatrix = calculator.GetCosts(player);
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }

        [Test]
        public void GetCosts_ShouldGiveIntMaxValueAsCost_ToCellsWithNotRemovableContent()
        {
            var board = GetFullNByNBoard(2);
            var notRemovableContent = GetNotRemovableContent();
            board[0, 0].AddContent(notRemovableContent);
            var minimalPlaceCost = 1;
            var expectedCostsMatrix = new int[,]
            {
                {int.MaxValue, 1},
                {1, 1}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = minimalPlaceCost,
            }.Build();
            
            var actualCostsMatrix = calculator.GetCosts(GetPlayer());
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }
        
        [Test]
        public void GetCosts_ShouldGiveDefaultCost_ToCellsWithUndefinedContent()
        {
            var board = GetFullNByNBoard(2);
            var undefinedContent = GetUndefinedContent();
            board[0, 0].AddContent(undefinedContent);
            var minimalPlaceCost = 1;
            var defaultCost = new Random().Next(15, 100);
            var expectedCostsMatrix = new int[,]
            {
                {defaultCost, 1},
                {1, 1}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = minimalPlaceCost,
                DefaultCost = defaultCost
            }.Build();
            
            var actualCostsMatrix = calculator.GetCosts(GetPlayer());
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }

        [Test]
        public void GetCosts_ShouldGiveReplaceCost_ForReplaceableContent()
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer();
            var replaceCost = new Random().Next(2, 100);
            var replaceableContent = GetReplaceableContent(replaceCost);
            board[0, 0].AddContent(replaceableContent);
            var minimalPlaceCost = 1;
            var expectedCostsMatrix = new int[,]
            {
                {replaceCost, 1},
                {1, 1}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = minimalPlaceCost,
            }.Build();
            
            var actualCostsMatrix = calculator.GetCosts(player);
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }

        [Test]
        public void GetCosts_ShouldGiveRemoveCost_ToCellsWithRemovableContent()
        {
            var board = GetFullNByNBoard(2);
            var removeCost = new Random().Next(2, 100);
            var removableContent = GetRemovableContent(removeCost);
            board[1, 1].AddContent(removableContent);
            var minimalPlaceCost = 1;
            var expectedCostsMatrix = new int[,]
            {
                {1, 1},
                {1, removeCost}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = minimalPlaceCost,
            }.Build();
            
            var actualCostsMatrix = calculator.GetCosts(GetPlayer());
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }

        [Test]
        public void GetCosts_ShouldGiveCaptureHitCost_ToCellsWithCapturableContent()
        {
            var player = GetPlayer();
            var board = GetFullNByNBoard(2);
            var captureHitCost = new Random().Next(2, 100);
            var capturableContent = GetCapturableContent(captureHitCost, player);
            board[0, 1].AddContent(capturableContent);
            var minimalPlaceCost = 1;
            var expectedCostsMatrix = new int[,]
            {
                {1, captureHitCost},
                {1, 1}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = minimalPlaceCost,
            }.Build();
            
            var actualCostsMatrix = calculator.GetCosts(player);

            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }

        [Test]
        //Free cells in this context are cells that have no content on them.
        public void GetCostsAfterMove_ShouldGiveMinimalPlaceCost_ForCellsThatWillBecomeFreeAfterMove()
        {
            var board = GetFullNByNBoard(3);
            var player = GetPlayer();
            var minPlaceCost = new Random().Next(1, 10);
            var replaceableContent1 = GetReplaceableContent(11);
            var replaceableContent2 = GetReplaceableContent(11);
            var replaceableContent3 = GetReplaceableContent(11);
            board[0, 0].AddContent(replaceableContent1);
            board[0, 1].AddContent(replaceableContent2);
            board[0, 2].AddContent(replaceableContent3);
            var move = new ReplaceMove(player, new Vector2Int(0, 0), 
                GetPlaceablePlayerOwned(player));
            var freedPositionsCalculatorMock = new Mock<IFreedPositionsCalculator>();
            freedPositionsCalculatorMock.Setup(x => x.GetFreedPositions(move)).Returns(new List<Vector2Int>
            {
                new Vector2Int(0, 1),
            });
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = minPlaceCost,
                FreedPositionsCalculator = freedPositionsCalculatorMock.Object
            }.Build();
            var expectedCostsMatrix = new int[,]
            {
                {int.MaxValue, minPlaceCost, 11},
                {minPlaceCost, minPlaceCost, minPlaceCost},
                {minPlaceCost, minPlaceCost, minPlaceCost}
            };
            
            var actualCostsMatrix = calculator.GetCostsAfterMove(player, move);
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }

        [Test]
        public void GetCostsAfterMove_ShouldGiveIntMaxValue_IfMovePlacesPlayerOwnedContent()
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer();
            var contentToPlace = GetPlaceablePlayerOwned(player);
            var move = new PlaceMove(player, new Vector2Int(0, 0), contentToPlace);
            var expectedCostsMatrix = new int[,]
            {
                {int.MaxValue, 1},
                {1, 1}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = 1,
            }.Build();
            
            var actualCostsMatrix = calculator.GetCostsAfterMove(player, move);
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }

        [Test]
        public void GetCostsAfterMove_ShouldGiveIntMaxValue_IfMoveReplacesWithPlayerOwnedContent()
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer();
            var replaceableContent = GetReplaceableContent(11);
            board[0, 0].AddContent(replaceableContent);
            var contentToPlace = GetPlaceablePlayerOwned(player);
            var move = new ReplaceMove(player, new Vector2Int(0, 0), contentToPlace);
            var expectedCostsMatrix = new int[,]
            {
                {int.MaxValue, 1},
                {1, 1}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = 1,
            }.Build();
            
            var actualCostsMatrix = calculator.GetCostsAfterMove(player, move);
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }

        [Test]
        public void GetCostsAfterMove_ShouldGiveReplaceCost_IfMovePlacesReplaceableContentNotOwnedByPlayer()
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer();
            var replaceCost = new Random().Next(2, 100);
            var contentToPlace = GetPlaceableReplaceable(replaceCost);
            var move = new PlaceMove(player, new Vector2Int(0, 0), contentToPlace);
            var expectedCostsMatrix = new int[,]
            {
                {replaceCost, 1},
                {1, 1}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = 1,
            }.Build();
            
            var actualCostsMatrix = calculator.GetCostsAfterMove(player, move);
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }
        
        [Test]
        public void GetCostsAfterMove_ShouldGiveReplaceCost_IfMoveReplacesWithReplaceableContentNotOwnedByPlayer()
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer();
            var replaceCost = new Random().Next(2, 100);
            var replaceableContent = GetReplaceableContent(replaceCost);
            board[0, 0].AddContent(replaceableContent);
            var contentToPlace = GetPlaceableReplaceable(replaceCost);
            var move = new ReplaceMove(player, new Vector2Int(0, 0), contentToPlace);
            var expectedCostsMatrix = new int[,]
            {
                {replaceCost, 1},
                {1, 1}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = 1,
            }.Build();
            
            var actualCostsMatrix = calculator.GetCostsAfterMove(player, move);
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }
        
        [Test]
        public void GetCostsAfterMove_ShouldGiveRemoveCost_IfMovePlacesRemovableContent()
        {
            var board = GetFullNByNBoard(2);
            var removeCost = new Random().Next(2, 100);
            var removableContent = GetPlaceableRemovable(removeCost);
            var move = new PlaceMove(GetPlayer(), new Vector2Int(0, 0), removableContent);
            var expectedCostsMatrix = new int[,]
            {
                {removeCost, 1},
                {1, 1}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = 1,
            }.Build();
            
            var actualCostsMatrix = calculator.GetCostsAfterMove(GetPlayer(), move);
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }
        
        //Same test but for replace move
        [Test]
        public void GetCostsAfterMove_ShouldGiveRemoveCost_IfMoveReplacesWithRemovableContent()
        {
            var board = GetFullNByNBoard(2);
            var removeCost = new Random().Next(2, 100);
            var contentToPlace = GetPlaceableRemovable(removeCost);
            var move = new ReplaceMove(GetPlayer(), new Vector2Int(0, 0), contentToPlace);
            var expectedCostsMatrix = new int[,]
            {
                {removeCost, 1},
                {1, 1}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = 1,
            }.Build();
            
            var actualCostsMatrix = calculator.GetCostsAfterMove(GetPlayer(), move);
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }
        
        [Test]
        //In the current version of game such situation are not the case, but who knows what we will 
        //add in the future.
        public void GetCostsAfterMove_ShouldGiveCaptureHitCost_IfMovePlacesCapturableContent()
        {
            var player = GetPlayer();
            var board = GetFullNByNBoard(2);
            var captureHitCost = new Random().Next(2, 100);
            var capturableContent = GetPlaceableCapturable(captureHitCost, player);
            var move = new PlaceMove(player, new Vector2Int(0, 0), capturableContent);
            var expectedCostsMatrix = new int[,]
            {
                {captureHitCost, 1},
                {1, 1}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = 1,
            }.Build();
            
            var actualCostsMatrix = calculator.GetCostsAfterMove(player, move);
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }
        
        [Test]
        public void GetCostsAfterMove_ShouldGiveCaptureHitCost_IfMoveReplacesWithCapturableContent()
        {
            var player = GetPlayer();
            var board = GetFullNByNBoard(2);
            var captureHitCost = new Random().Next(2, 100);
            var contentToPlace = GetPlaceableCapturable(captureHitCost, player);
            var move = new ReplaceMove(player, new Vector2Int(0, 0), contentToPlace);
            var expectedCostsMatrix = new int[,]
            {
                {captureHitCost, 1},
                {1, 1}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = 1,
            }.Build();
            
            var actualCostsMatrix = calculator.GetCostsAfterMove(player, move);
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }

        [Test]
        public void GetCostsAfterMove_ShouldGiveIntMaxValue_IfMovePlacesNotRemovableContent()
        {
            var board = GetFullNByNBoard(2);
            var notRemovableContent = GetPlaceableNotRemovable();
            var move = new PlaceMove(GetPlayer(), new Vector2Int(0, 0), notRemovableContent);
            var expectedCostsMatrix = new int[,]
            {
                {int.MaxValue, 1},
                {1, 1}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = 1,
            }.Build();
            
            var actualCostsMatrix = calculator.GetCostsAfterMove(GetPlayer(), move);
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }
        
        [Test]
        public void GetCostsAfterMove_ShouldGiveIntMaxValue_IfMoveReplacesWithNotRemovableContent()
        {
            var board = GetFullNByNBoard(2);
            var notRemovableContent = GetPlaceableNotRemovable();
            var move = new ReplaceMove(GetPlayer(), new Vector2Int(0, 0), notRemovableContent);
            var expectedCostsMatrix = new int[,]
            {
                {int.MaxValue, 1},
                {1, 1}
            };
            var calculator = new BoardCostsCalculatorBuilder
            {
                Board = board,
                MinimalPlaceCost = 1,
            }.Build();
            
            var actualCostsMatrix = calculator.GetCostsAfterMove(GetPlayer(), move);
            
            Assert.AreEqual(expectedCostsMatrix, actualCostsMatrix);
        }
        
        private class BoardCostsCalculatorBuilder
        {
            public Board Board { get; set; } = GetFullNByNBoard(1);

            public IFreedPositionsCalculator FreedPositionsCalculator { get; set; }

            public int DefaultCost { get; set; }
            public int MinimalPlaceCost { get; set; }

            public BoardCostsCalculatorBuilder()
            {
                var freedPositionsCalculatorMock = new Mock<IFreedPositionsCalculator>();
                freedPositionsCalculatorMock.Setup(x => x.GetFreedPositions(It.IsAny<AbstractMove>()))
                    .Returns(new List<Vector2Int>());
                FreedPositionsCalculator = freedPositionsCalculatorMock.Object;
            }

            public BoardCostsCalculator Build()
            {
                return new BoardCostsCalculator(Board, FreedPositionsCalculator, DefaultCost, MinimalPlaceCost);
            }
        }
        
        private static Content GetPlayerOwnedContent(Player player)
        {
            var mockContent = new Mock<Content>();
            var mockPlayerOwned = mockContent.As<IPlayerOwned>();
            mockPlayerOwned.Setup(x => x.GetOwner()).Returns(player);
            return mockContent.Object;
        }

        private static Content GetNotRemovableContent()
        {
            var mockContent = new Mock<Content>();
            var mockRemovable = mockContent.As<IRemovable>();
            mockRemovable.Setup(x => x.CanBeRemoved()).Returns(false);
            return mockContent.Object;
        }
        
        private static Content GetRemovableContent(int removeCost)
        {
            var mockContent = new Mock<Content>();
            var mockRemovable = mockContent.As<IRemovable>();
            mockRemovable.Setup(x => x.CanBeRemoved()).Returns(true);
            mockRemovable.Setup(x => x.GetRemoveCost()).Returns(removeCost);
            return mockContent.Object;
        }

        private static Content GetUndefinedContent()
        {
            var contentMock = new Mock<Content>();
            return contentMock.Object;
        }

        private static Content GetReplaceableContent(int replaceCost)
        {
            var contentMock = new Mock<Content>();
            var replaceableMock = contentMock.As<IReplaceable>();
            replaceableMock.Setup(x => x.GetReplaceCost()).Returns(replaceCost);
            return contentMock.Object;
        }

        private static Content GetCapturableContent(int captureHitCost, Player forPlayer)
        {
            var contentMock = new Mock<Content>();
            var capturableMock = contentMock.As<ICapturable>();
            capturableMock.Setup(x => x.GetCaptureHitCost(forPlayer)).Returns(captureHitCost);
            return contentMock.Object;
        }

        private static IPlaceable GetPlaceablePlayerOwned(Player owner)
        {
            var placeableMock = new Mock<IPlaceable>();
            var playerOwnedMock = placeableMock.As<IPlayerOwned>();
            playerOwnedMock.Setup(x => x.GetOwner()).Returns(owner);
            return placeableMock.Object;
        }

        private static IPlaceable GetPlaceableReplaceable(int replaceCost)
        {
            var placeableMock = new Mock<IPlaceable>();
            var replaceableMock = placeableMock.As<IReplaceable>();
            replaceableMock.Setup(x => x.GetReplaceCost()).Returns(replaceCost);
            return placeableMock.Object;
        }
        
        private static IPlaceable GetPlaceableRemovable(int removeCost)
        {
            var placeableMock = new Mock<IPlaceable>();
            var removableMock = placeableMock.As<IRemovable>();
            removableMock.Setup(x => x.CanBeRemoved()).Returns(true);
            removableMock.Setup(x => x.GetRemoveCost()).Returns(removeCost);
            return placeableMock.Object;
        }
        
        private static IPlaceable GetPlaceableCapturable(int captureHitCost, Player forPlayer)
        {
            var placeableMock = new Mock<IPlaceable>();
            var capturableMock = placeableMock.As<ICapturable>();
            capturableMock.Setup(x => x.GetCaptureHitCost(forPlayer)).Returns(captureHitCost);
            return placeableMock.Object;
        }

        private static IPlaceable GetPlaceableNotRemovable()
        {
            var placeableMock = new Mock<IPlaceable>();
            var removableMock = placeableMock.As<IRemovable>();
            removableMock.Setup(x => x.CanBeRemoved()).Returns(false);
            return placeableMock.Object;
        }
    }
}