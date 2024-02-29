using System;
using System.Collections.Generic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.PVE.GameSituations;
using Src.PVE.MoveSearchers.TraitBasedSearchers;
using Src.PVE.Providers;
using static Tests.Utils.ObjectCreationUtility;
using Random = UnityEngine.Random;

namespace Tests.EditMode.PVETests.MoveSearchersTests.TraitBasedSearchersTests
{
    public class TraitsWeightedSumMoveSearcherTests
    {
        private struct GetBestMoveTestCase
        {
            public List<AbstractMove> InputMoves;
            public Dictionary<AbstractMove, MoveTraitsValues> MovesToTraits;
            public Dictionary<AbstractMove, MoveTraitsValues> NormalizedMovesToTraits;
            public MoveTraitsValues DefaultWeights;
            public Dictionary<IGameSituation, MoveTraitsValues> SituationTraitsWeights;
            public IRangeRandomNumberGenerator RandomNumberGenerator;
            public AbstractMove ExpectedBestMove;
        }

        [Test]
        //This test uses different test cases that are provided from GetTestCase method. See this method and its comments for more details.
        public void GetBestMove_ShouldReturnCorrectBestMove([Values(1, 2, 3, 4, 5, 6)]int testCaseNumber)
        {
            var testCaseData = GetTestCase(testCaseNumber);
            var inputMoves = testCaseData.InputMoves;
            var movesToTraits = testCaseData.MovesToTraits;
            var normalizedMovesToTraits = testCaseData.NormalizedMovesToTraits;
            var defaultWeights = testCaseData.DefaultWeights;
            var situationTraitsWeights = testCaseData.SituationTraitsWeights;
            var traitsEvaluatorMock = new Mock<IMovesListTraitsEvaluator>();
            traitsEvaluatorMock.Setup(traitsEvaluator => traitsEvaluator.EvaluateTraitsForMoves(inputMoves)).Returns(movesToTraits);
            var traitsNormalizerMock = new Mock<IMovesTraitsNormalizer>();
            traitsNormalizerMock.Setup(traitsNormalizer => traitsNormalizer.NormalizeTraits(movesToTraits)).Returns(normalizedMovesToTraits);
            var totalPossibleMovesProviderMock = new Mock<ITotalPossibleMovesProvider>();
            totalPossibleMovesProviderMock.Setup(totalPossibleMovesProvider => totalPossibleMovesProvider.GetTotalPossibleMoves(It.IsAny<int>())).Returns(inputMoves);
            var moveSearcher = new TraitsWeightedSumMoveSearcherBuilder
            {
                TraitsEvaluator = traitsEvaluatorMock.Object,
                TraitsNormalizer = traitsNormalizerMock.Object,
                TotalPossibleMovesProvider = totalPossibleMovesProviderMock.Object,
                DefaultWeights = defaultWeights,
                SituationTraitsWeights = situationTraitsWeights,
                RandomNumberGenerator = testCaseData.RandomNumberGenerator
            }.Build();
            
            var actualBestMove = moveSearcher.GetBestMove(0);
            
            Assert.AreSame(testCaseData.ExpectedBestMove, actualBestMove);  
        }

        //Tip: For better readability, collapse "if" blocks if your IDE allows it.
        private GetBestMoveTestCase GetTestCase(int testCaseNumber)
        {
            var inputMoves = new List<AbstractMove>();
            var movesToTraits = new Dictionary<AbstractMove, MoveTraitsValues>();
            var randomNumberGeneratorMock = new Mock<IRangeRandomNumberGenerator>();
            randomNumberGeneratorMock.Setup(randomNumberGenerator => randomNumberGenerator.GetRandom(It.IsAny<int>(), It.IsAny<int>())).Returns(0);

            //First case where the only trait that matters is destructiveness.
            //No game situations provided and there is only one possible best move.
            if (testCaseNumber == 1)
            {
                var defaultWeights = new MoveTraitsValues
                {
                    Destructiveness = 1,
                    Aggressiveness = 0,
                    Defensiveness = 0,
                    Enhanciveness = 0,
                    Harmfulness = 0
                };
                var expectedBestMove = GetMove();
                var normalizedMovesToTraits = new Dictionary<AbstractMove, MoveTraitsValues>
                {
                    {
                        GetMove(),
                        new MoveTraitsValues
                        {
                            Destructiveness = 0, Aggressiveness = 0, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        GetMove(),
                        new MoveTraitsValues
                        {
                            Destructiveness = 0.5f, Aggressiveness = 0, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        expectedBestMove,
                        new MoveTraitsValues
                        {
                            Destructiveness = 1, Aggressiveness = 0, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    }
                };
                var testCase = new GetBestMoveTestCase
                {
                    InputMoves = inputMoves,
                    MovesToTraits = movesToTraits,
                    NormalizedMovesToTraits = normalizedMovesToTraits,
                    DefaultWeights = defaultWeights,
                    SituationTraitsWeights = new Dictionary<IGameSituation, MoveTraitsValues>(),
                    ExpectedBestMove = expectedBestMove,
                    RandomNumberGenerator = randomNumberGeneratorMock.Object
                };
                return testCase;
            }
            //Second case where weights are 0.5 for destructiveness and 0.5 for aggressiveness.
            //There are no game situations provided and there is only one possible best move.
            if (testCaseNumber == 2)
            {
                var defaultWeights = new MoveTraitsValues
                {
                    Destructiveness = 0.5f,
                    Aggressiveness = 0.5f,
                    Defensiveness = 0,
                    Enhanciveness = 0,
                    Harmfulness = 0
                };
                var expectedBestMove = GetMove();
                var normalizedMovesToTraits = new Dictionary<AbstractMove, MoveTraitsValues>
                {
                    {
                        GetMove(),
                        new MoveTraitsValues
                        {
                            Destructiveness = 0, Aggressiveness = 0, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        GetMove(),
                        new MoveTraitsValues
                        {
                            Destructiveness = 0.5f, Aggressiveness = 0, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        GetMove(),
                        new MoveTraitsValues
                        {
                            Destructiveness = 0.25f, Aggressiveness = 1, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        expectedBestMove,
                        new MoveTraitsValues
                        {
                            Destructiveness = 1f, Aggressiveness = 0.5f, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    }
                };
                var testCase = new GetBestMoveTestCase
                {
                    InputMoves = inputMoves,
                    MovesToTraits = movesToTraits,
                    NormalizedMovesToTraits = normalizedMovesToTraits,
                    DefaultWeights = defaultWeights,
                    SituationTraitsWeights = new Dictionary<IGameSituation, MoveTraitsValues>(),
                    ExpectedBestMove = expectedBestMove,
                    RandomNumberGenerator = randomNumberGeneratorMock.Object
                };
                return testCase;
            }
            //In third test case default weights are 1 for destructiveness and 0 for the rest, while the situation weights are 1 for aggressiveness and 0 for the rest.
            //So the best move should be the one with the highest aggressiveness. Also there is only one possible best move.
            if (testCaseNumber == 3)
            {
                var defaultWeights = new MoveTraitsValues
                {
                    Destructiveness = 1,
                    Aggressiveness = 0,
                    Defensiveness = 0,
                    Enhanciveness = 0,
                    Harmfulness = 0
                };
                var situationMock = new Mock<IGameSituation>();
                situationMock.Setup(situation => situation.IsSituation()).Returns(true);
                var situation = situationMock.Object;
                var situationWeights = new MoveTraitsValues
                {
                    Destructiveness = 0,
                    Aggressiveness = 1,
                    Defensiveness = 0,
                    Enhanciveness = 0,
                    Harmfulness = 0
                };
                var expectedBestMove =  GetMove();
                var normalizedMovesToTraits = new Dictionary<AbstractMove, MoveTraitsValues>
                {
                    {
                        GetMove(),
                        new MoveTraitsValues
                        {
                            Destructiveness = 0, Aggressiveness = 0, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        GetMove(),
                        new MoveTraitsValues
                        {
                            Destructiveness = 1f, Aggressiveness = 0, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        expectedBestMove,
                        new MoveTraitsValues
                        {
                            Destructiveness = 0, Aggressiveness = 1, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    }
                };
                return new GetBestMoveTestCase
                {
                    InputMoves = inputMoves,
                    MovesToTraits = movesToTraits,
                    NormalizedMovesToTraits = normalizedMovesToTraits,
                    DefaultWeights = defaultWeights,
                    SituationTraitsWeights = new Dictionary<IGameSituation, MoveTraitsValues>
                    {
                        {situation, situationWeights}
                    },
                    ExpectedBestMove = expectedBestMove,
                    RandomNumberGenerator = randomNumberGeneratorMock.Object
                };
            }
            //In fourth test case there are two situations: weights for the first one are 1 for destructiveness and 0 for the rest, while the weights for the second one are 1 for aggressiveness and 0 for the rest.
            //Second situation will return true for IsSituation method, while the first one will return false.
            //The default weights are 0.5 for destructiveness and 0.5 for aggressiveness.
            //The best move should be the one with the highest aggressiveness.
            if(testCaseNumber == 4)
            {
                var defaultWeights = new MoveTraitsValues
                {
                    Destructiveness = 0.5f,
                    Aggressiveness = 0.5f,
                    Defensiveness = 0,
                    Enhanciveness = 0,
                    Harmfulness = 0
                };
                var firstSituationMock = new Mock<IGameSituation>();
                firstSituationMock.Setup(situation => situation.IsSituation()).Returns(false);
                var firstSituation = firstSituationMock.Object;
                var firstSituationWeights = new MoveTraitsValues
                {
                    Destructiveness = 1,
                    Aggressiveness = 0,
                    Defensiveness = 0,
                    Enhanciveness = 0,
                    Harmfulness = 0
                };
                var secondSituationMock = new Mock<IGameSituation>();
                secondSituationMock.Setup(situation => situation.IsSituation()).Returns(true);
                var secondSituation = secondSituationMock.Object;
                var secondSituationWeights = new MoveTraitsValues
                {
                    Destructiveness = 0,
                    Aggressiveness = 1,
                    Defensiveness = 0,
                    Enhanciveness = 0,
                    Harmfulness = 0
                };
                var expectedBestMove = GetMove();
                var normalizedMovesToTraits = new Dictionary<AbstractMove, MoveTraitsValues>
                {
                    {
                        GetMove(),
                        new MoveTraitsValues
                        {
                            Destructiveness = 0, Aggressiveness = 0, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        GetMove(),
                        new MoveTraitsValues
                        {
                            Destructiveness = 0.5f, Aggressiveness = 0, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        GetMove(),
                        new MoveTraitsValues
                        {
                            Destructiveness = 0.25f, Aggressiveness = 0.5f, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        expectedBestMove,
                        new MoveTraitsValues
                        {
                            Destructiveness = 1f, Aggressiveness = 1, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    }
                };
                var testCase = new GetBestMoveTestCase
                {
                    InputMoves = inputMoves,
                    MovesToTraits = movesToTraits,
                    NormalizedMovesToTraits = normalizedMovesToTraits,
                    DefaultWeights = defaultWeights,
                    SituationTraitsWeights = new Dictionary<IGameSituation, MoveTraitsValues>
                    {
                        {firstSituation, firstSituationWeights},
                        {secondSituation, secondSituationWeights}
                    },
                    ExpectedBestMove = expectedBestMove,
                    RandomNumberGenerator = randomNumberGeneratorMock.Object
                };
                return testCase;
            }
            //In fifth test case there are three situations with following weights:
            //First situation: 1 for destructiveness and 0 for the rest.
            //Second situation: 1 for aggressiveness and 0 for the rest.
            //Third situation: 1 for defensiveness and 0 for the rest.
            //The default weights are 1 for enhanciveness and 0 for the rest.
            //In this case the second and the third situations return true for IsSituation method, while the first one returns false.
            //Actual weights should be the ones from the second situation, as situations should be checked in the order they are provided.
            //The best move should be the one with the highest aggressiveness.
            if (testCaseNumber == 5)
            {
                var defaultWeights = new MoveTraitsValues
                {
                    Destructiveness = 0,
                    Aggressiveness = 0,
                    Defensiveness = 0,
                    Enhanciveness = 1,
                    Harmfulness = 0
                };
                var firstSituationMock = new Mock<IGameSituation>();
                firstSituationMock.Setup(situation => situation.IsSituation()).Returns(false);
                var firstSituation = firstSituationMock.Object;
                var firstSituationWeights = new MoveTraitsValues
                {
                    Destructiveness = 1,
                    Aggressiveness = 0,
                    Defensiveness = 0,
                    Enhanciveness = 0,
                    Harmfulness = 0
                };
                var secondSituationMock = new Mock<IGameSituation>();
                secondSituationMock.Setup(situation => situation.IsSituation()).Returns(true);
                var secondSituation = secondSituationMock.Object;
                var secondSituationWeights = new MoveTraitsValues
                {
                    Destructiveness = 0,
                    Aggressiveness = 1,
                    Defensiveness = 0,
                    Enhanciveness = 0,
                    Harmfulness = 0
                };
                var thirdSituationMock = new Mock<IGameSituation>();
                thirdSituationMock.Setup(situation => situation.IsSituation()).Returns(true);
                var thirdSituation = thirdSituationMock.Object;
                var thirdSituationWeights = new MoveTraitsValues
                {
                    Destructiveness = 0,
                    Aggressiveness = 0,
                    Defensiveness = 1,
                    Enhanciveness = 0,
                    Harmfulness = 0
                };
                var expectedBestMove = GetMove();
                var normalizedMovesToTraits = new Dictionary<AbstractMove, MoveTraitsValues>
                {
                    {
                        GetMove(),
                        new MoveTraitsValues
                        {
                            Destructiveness = 0, Aggressiveness = 0, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        GetMove(),
                        new MoveTraitsValues
                        {
                            Destructiveness = 0.5f, Aggressiveness = 0, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        GetMove(),
                        new MoveTraitsValues
                        {
                            Destructiveness = 0.25f, Aggressiveness = 0.2f, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        expectedBestMove,
                        new MoveTraitsValues
                        {
                            Destructiveness = 1f, Aggressiveness = 1f, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    }
                };
                var testCase = new GetBestMoveTestCase
                {
                    InputMoves = inputMoves,
                    MovesToTraits = movesToTraits,
                    NormalizedMovesToTraits = normalizedMovesToTraits,
                    DefaultWeights = defaultWeights,
                    SituationTraitsWeights = new Dictionary<IGameSituation, MoveTraitsValues>
                    {
                        {firstSituation, firstSituationWeights},
                        {secondSituation, secondSituationWeights},
                        {thirdSituation, thirdSituationWeights}
                    },
                    ExpectedBestMove = expectedBestMove,
                    RandomNumberGenerator = randomNumberGeneratorMock.Object
                };
                return testCase;
            }
            //In sixth test case there are no situations and default weights are 0.5 for destructiveness and 0.5 for aggressiveness.
            //However, there a three best moves with the same value, so the best move should be randomly chosen from these three.
            else
            {
                var defaultWeights = new MoveTraitsValues
                {
                    Destructiveness = 0.5f,
                    Aggressiveness = 0.5f,
                    Defensiveness = 0,
                    Enhanciveness = 0,
                    Harmfulness = 0
                };
                var possibleBestMoves = new List<AbstractMove>
                {
                    GetMove(),
                    GetMove(),
                    GetMove()
                };
                var bestMoveId = Random.Range(0, 3);
                var expectedBestMove = possibleBestMoves[bestMoveId];
                var normalizedMovesToTraits = new Dictionary<AbstractMove, MoveTraitsValues>
                {
                    {
                        GetMove(),
                        new MoveTraitsValues
                        {
                            Destructiveness = 0, Aggressiveness = 0, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        possibleBestMoves[0],
                        new MoveTraitsValues
                        {
                            Destructiveness = 0.5f, Aggressiveness = 0.5f, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        possibleBestMoves[1],
                        new MoveTraitsValues
                        {
                            Destructiveness = 0.5f, Aggressiveness = 0.5f, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        possibleBestMoves[2],
                        new MoveTraitsValues
                        {
                            Destructiveness = 0.5f, Aggressiveness = 0.5f, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    }
                };
                var randomMock = new Mock<IRangeRandomNumberGenerator>();
                randomMock.Setup(random => random.GetRandom(0, 3)).Returns(bestMoveId);
                var testCase = new GetBestMoveTestCase
                {
                    InputMoves = possibleBestMoves,
                    MovesToTraits = movesToTraits,
                    NormalizedMovesToTraits = normalizedMovesToTraits,
                    DefaultWeights = defaultWeights,
                    SituationTraitsWeights = new Dictionary<IGameSituation, MoveTraitsValues>(),
                    ExpectedBestMove = expectedBestMove,
                    RandomNumberGenerator = randomMock.Object
                };
                return testCase;
            }
        }

        private AbstractMove GetMove()
        {
            return new PlaceMove(GetPlayer(), new Vector2Int(1, 2), GetKnight());
        }
        
        private class TraitsWeightedSumMoveSearcherBuilder
        {
            public IMovesListTraitsEvaluator TraitsEvaluator = new Mock<IMovesListTraitsEvaluator>().Object;
            public IMovesTraitsNormalizer TraitsNormalizer = new Mock<IMovesTraitsNormalizer>().Object;
            public Dictionary<IGameSituation, MoveTraitsValues> SituationTraitsWeights = new Dictionary<IGameSituation, MoveTraitsValues>();
            public MoveTraitsValues DefaultWeights;
            public ITotalPossibleMovesProvider TotalPossibleMovesProvider = new Mock<ITotalPossibleMovesProvider>().Object;
            public IRangeRandomNumberGenerator RandomNumberGenerator = new Mock<IRangeRandomNumberGenerator>().Object;
            
            public TraitsWeightedSumMoveSearcher Build()
            {
                return new TraitsWeightedSumMoveSearcher(TraitsEvaluator, TraitsNormalizer, TotalPossibleMovesProvider, RandomNumberGenerator, SituationTraitsWeights, DefaultWeights);
            }
        }
    }
}