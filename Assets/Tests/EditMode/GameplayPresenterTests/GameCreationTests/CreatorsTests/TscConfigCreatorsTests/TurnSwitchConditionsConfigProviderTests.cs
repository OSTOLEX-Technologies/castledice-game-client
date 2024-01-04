using System;
using System.Collections.Generic;
using castledice_game_data_logic.TurnSwitchConditions;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.Creators.TscConfigCreators;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.CreatorsTests.TscConfigCreatorsTests
{
    public class TurnSwitchConditionsConfigProviderTests
    {
        [Test]
        [TestCaseSource(nameof(GetTscTypes))]
        public void GetTurnSwitchConditionsConfig_ShouldReturnConfig_WithListFromGivenData(TscType type)
        {
            var expectedList = new List<TscType> { type };
            var data = new TscConfigData(expectedList);
            var provider = new TurnSwitchConditionsConfigProvider();
            
            var config = provider.GetTurnSwitchConditionsConfig(data);
            
            Assert.AreEqual(expectedList, config.ConditionsToUse);
        }

        public static IEnumerable<TscType> GetTscTypes()
        {
            var values = Enum.GetValues(typeof(TscType));
            foreach (var type in values)
            {
                yield return (TscType) type;
            }
        }
    }
}