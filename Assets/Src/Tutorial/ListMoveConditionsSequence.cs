using System.Collections.Generic;
using Src.PVE.MoveConditions;

namespace Src.Tutorial
{
    public class ListMoveConditionsSequence : IMoveConditionsSequence
    {
        private readonly List<IMoveCondition> _conditions;
        private int _currentConditionIndex;
        
        public ListMoveConditionsSequence(List<IMoveCondition> conditions)
        {
            _conditions = conditions;
        }
        
        public IMoveCondition GetCurrentCondition()
        {
            throw new System.NotImplementedException();
        }

        public void MoveToNextCondition()
        {
            throw new System.NotImplementedException();
        }
    }
}