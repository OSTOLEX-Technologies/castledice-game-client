using System.Collections.Generic;
using Src.General.MoveConditions;

namespace Src.Tutorial
{
    public interface IMoveConditionsListConfig
    {
        List<IMoveCondition> GetMoveConditions();
    }
}