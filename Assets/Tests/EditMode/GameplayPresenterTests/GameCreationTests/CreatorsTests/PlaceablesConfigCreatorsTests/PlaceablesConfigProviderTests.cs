using castledice_game_data_logic.ConfigsData;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects.Configs;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.Creators.PlaceablesConfigCreators;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.CreatorsTests.PlaceablesConfigCreatorsTests
{
    public class PlaceablesConfigProviderTests
    {
        [TestCaseSource(nameof(GetPlaceablesConfigTestCases))]
        public void GetPlaceablesConfig_ShouldReturnAppropriatePlaceablesConfig(PlaceablesConfigData configData, PlaceablesConfig expectedConfig)
        {
            var provider = new PlaceablesConfigProvider();
            
            var actualConfig = provider.GetPlaceablesConfig(configData);
            
            Assert.IsTrue(ConfigsAreEqual(actualConfig, expectedConfig));
        }

        private bool ConfigsAreEqual(PlaceablesConfig first, PlaceablesConfig second)
        {
            var firstKnightConfig = first.KnightConfig;
            var secondKnightConfig = second.KnightConfig;
            return firstKnightConfig.Health == secondKnightConfig.Health &&
                   firstKnightConfig.PlacementCost == secondKnightConfig.PlacementCost;
        }

        public static object[] GetPlaceablesConfigTestCases =
        {
            new object[]
            {
                new PlaceablesConfigData(new KnightConfigData(1, 2)),
                new PlaceablesConfig(new KnightConfig(1, 2))
            },
            new object[]
            {
                new PlaceablesConfigData(new KnightConfigData(3, 4)),
                new PlaceablesConfig(new KnightConfig(3, 4))
            }
        };
    }
}