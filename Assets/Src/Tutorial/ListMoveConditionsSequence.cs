using System;
using System.Collections.Generic;
using Src.General.MoveConditions;

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
            if (_conditions.Count == 0)
            {
                throw new InvalidOperationException("Conditions list is empty");
            }
            return _conditions[_currentConditionIndex];
        }

        public void MoveToNextCondition()
        {
            if (_conditions.Count == 0)
            {
                throw new InvalidOperationException("Conditions list is empty");
            }
            if (_currentConditionIndex == _conditions.Count - 1)
            {
                throw new InvalidOperationException("No more conditions in the list");
            }
            _currentConditionIndex++;
        }

        public bool HasNext()
        {
            return _currentConditionIndex < _conditions.Count - 1;
        }
    }
}