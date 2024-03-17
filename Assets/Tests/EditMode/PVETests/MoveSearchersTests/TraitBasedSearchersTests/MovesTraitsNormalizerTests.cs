using System.Collections.Generic;
using castledice_game_logic.MovesLogic;
using NUnit.Framework;
using static Tests.Utils.ObjectCreationUtility;
using Src.PVE.MoveSearchers.TraitBasedSearchers;

namespace Tests.EditMode.PVETests.MoveSearchersTests.TraitBasedSearchersTests
{
    public class MovesTraitsNormalizerTests
    {
        private struct NormalizeMovesTraitsTestCase
        {
            public Dictionary<AbstractMove, MoveTraitsValues> MovesTraits;
            public Dictionary<AbstractMove, MoveTraitsValues> ExpectedNormalizedMovesTraits;
        }
        
        [Test]
        //Normalized means that the range of values for each trait is between 0 and 1
        public void NormalizeTraitsForMoves_ShouldReturnMovesWithNormalizedTraits([Range(1, 3)]int testCaseNumber)
        {
            var testCase = GetTestsCase(testCaseNumber);
            var movesTraitsNormalizer = new MovesTraitsNormalizer();
            var normalizedMovesTraits = movesTraitsNormalizer.NormalizeTraits(testCase.MovesTraits);

            foreach (var move in normalizedMovesTraits.Keys)
            {
                var expectedNormalizedTraits = testCase.ExpectedNormalizedMovesTraits[move];
                var actualNormalizedTraits = normalizedMovesTraits[move];
                Assert.AreEqual(expectedNormalizedTraits.Destructiveness, actualNormalizedTraits.Destructiveness, 0.0001f);
                Assert.AreEqual(expectedNormalizedTraits.Aggressiveness, actualNormalizedTraits.Aggressiveness, 0.0001f);
                Assert.AreEqual(expectedNormalizedTraits.Defensiveness, actualNormalizedTraits.Defensiveness, 0.0001f);
                Assert.AreEqual(expectedNormalizedTraits.Enhanciveness, actualNormalizedTraits.Enhanciveness, 0.0001f);
                Assert.AreEqual(expectedNormalizedTraits.Harmfulness, actualNormalizedTraits.Harmfulness, 0.0001f);
            }
        }
        
        private NormalizeMovesTraitsTestCase GetTestsCase(int testCaseNumber)
        {
            //First case is a simple check for usage of appropriate normalization formula.
            //The formula is following: normalizedValue = (value - minValue) / (maxValue - minValue)
            if (testCaseNumber == 1)
            {
                var moves = new List<AbstractMove>
                {
                    GetMove(),
                    GetMove()
                };
                var movesTraits = new Dictionary<AbstractMove, MoveTraitsValues>
                {
                    {
                        moves[0],
                        new MoveTraitsValues
                        {
                            Destructiveness = 1, Aggressiveness = 2, Defensiveness = 3, Enhanciveness = 4,
                            Harmfulness = 5
                        }
                    },
                    {
                        moves[1],
                        new MoveTraitsValues
                        {
                            Destructiveness = 2, Aggressiveness = 3, Defensiveness = 4, Enhanciveness = 5,
                            Harmfulness = 6
                        }
                    }
                };
                var expectedNormalizedMovesTraits = new Dictionary<AbstractMove, MoveTraitsValues>
                {
                    {
                        moves[0],
                        new MoveTraitsValues
                        {
                            Destructiveness = 0, Aggressiveness = 0, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        moves[1],
                        new MoveTraitsValues
                        {
                            Destructiveness = 1, Aggressiveness = 1, Defensiveness = 1, Enhanciveness = 1,
                            Harmfulness = 1
                        }
                    }
                };
                return new NormalizeMovesTraitsTestCase
                {
                    MovesTraits = movesTraits,
                    ExpectedNormalizedMovesTraits = expectedNormalizedMovesTraits
                };
            }
            //Second case checks the compliance of the following rule: If all values of the trait are same, then the normalized value should be 0.
            if (testCaseNumber == 2)
            {
                var moves = new List<AbstractMove>
                {
                    GetMove(),
                    GetMove()
                };
                var movesTraits = new Dictionary<AbstractMove, MoveTraitsValues>
                {
                    {
                        moves[0],
                        new MoveTraitsValues
                        {
                            Destructiveness = 1, Aggressiveness = 1, Defensiveness = 1, Enhanciveness = 1,
                            Harmfulness = 1
                        }
                    },
                    {
                        moves[1],
                        new MoveTraitsValues
                        {
                            Destructiveness = 1, Aggressiveness = 1, Defensiveness = 1, Enhanciveness = 1,
                            Harmfulness = 1
                        }
                    }
                };
                var expectedNormalizedMovesTraits = new Dictionary<AbstractMove, MoveTraitsValues>
                {
                    {
                        moves[0],
                        new MoveTraitsValues
                        {
                            Destructiveness = 0, Aggressiveness = 0, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    },
                    {
                        moves[1],
                        new MoveTraitsValues
                        {
                            Destructiveness = 0, Aggressiveness = 0, Defensiveness = 0, Enhanciveness = 0,
                            Harmfulness = 0
                        }
                    }
                };
                return new NormalizeMovesTraitsTestCase
                {
                    MovesTraits = movesTraits,
                    ExpectedNormalizedMovesTraits = expectedNormalizedMovesTraits
                };
            }
            //Third case is a more complex example then the first one, but the idea is the same.
            //Values for non-normalized traits are following (destructiveness, aggressiveness, defensiveness, enhanciveness, harmfulness):
            // (15, 0.5, 750, 0, 0)
            // (30, 1, 600, -1, 1)
            // (45, 0.5, 450, -2, 0)
            // (60, 2, 300, -3, 1)
            // (75, 2, 150, -4, 0)
            //Normalized values for traits should be following (destructiveness, aggressiveness, defensiveness, enhanciveness, harmfulness):
            // (0, 0, 1, 1, 0)
            // (0.25, 0.25, 0.75, 0.75, 1)
            // (0.5, 0, 0.5, 0.5, 0)
            // (0.75, 1, 0.25, 0.25, 1)
            // (1, 1, 0, 0, 0)
            else
            { 
                var moves = new List<AbstractMove>
                {
                    GetMove(),
                    GetMove(),
                    GetMove(),
                    GetMove(),
                    GetMove()
                }; 
                var movesTraits = new Dictionary<AbstractMove, MoveTraitsValues>
                {
                      {
                           moves[0],
                           new MoveTraitsValues
                           {
                             Destructiveness = 15, Aggressiveness = 0.5f, Defensiveness = 750, Enhanciveness = 0,
                             Harmfulness = 0
                           }
                      },
                      {
                           moves[1],
                           new MoveTraitsValues
                           {
                             Destructiveness = 30, Aggressiveness = 1, Defensiveness = 600, Enhanciveness = -1,
                             Harmfulness = 1
                           }
                      },
                      {
                           moves[2],
                           new MoveTraitsValues
                           {
                             Destructiveness = 45, Aggressiveness = 0.5f, Defensiveness = 450, Enhanciveness = -2,
                             Harmfulness = 0
                           }
                      },
                      {
                           moves[3],
                           new MoveTraitsValues
                           {
                             Destructiveness = 60, Aggressiveness = 2, Defensiveness = 300, Enhanciveness = -3,
                             Harmfulness = 1
                           }
                      },
                      {
                           moves[4],
                           new MoveTraitsValues
                           {
                             Destructiveness = 75, Aggressiveness = 2f, Defensiveness = 150, Enhanciveness = -4,
                             Harmfulness = 0
                           }
                      }
                };
                var normalizedMovesTraits = new Dictionary<AbstractMove, MoveTraitsValues>
                {
                      {
                           moves[0],
                           new MoveTraitsValues
                           {
                             Destructiveness = 0, Aggressiveness = 0, Defensiveness = 1, Enhanciveness = 1,
                             Harmfulness = 0
                           }
                      },
                      {
                           moves[1],
                           new MoveTraitsValues
                           {
                             Destructiveness = 0.25f, Aggressiveness = 0.33333333f, Defensiveness = 0.75f, Enhanciveness = 0.75f,
                             Harmfulness = 1
                           }
                      },
                      {
                           moves[2],
                           new MoveTraitsValues
                           {
                             Destructiveness = 0.5f, Aggressiveness = 0, Defensiveness = 0.5f, Enhanciveness = 0.5f,
                             Harmfulness = 0
                           }
                      },
                      {
                           moves[3],
                           new MoveTraitsValues
                           {
                             Destructiveness = 0.75f, Aggressiveness = 1, Defensiveness = 0.25f, Enhanciveness = 0.25f,
                             Harmfulness = 1
                           }
                      },
                      {
                           moves[4],
                           new MoveTraitsValues
                           {
                             Destructiveness = 1, Aggressiveness = 1, Defensiveness = 0, Enhanciveness = 0,
                             Harmfulness = 0
                           }
                      }
                };
                return new NormalizeMovesTraitsTestCase
                {
                      MovesTraits = movesTraits,
                      ExpectedNormalizedMovesTraits = normalizedMovesTraits
                };
            }
        }
    }
}