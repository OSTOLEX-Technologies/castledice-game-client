using System.Collections.Generic;
using UnityEngine;

namespace Src.General.NumericSequences
{
    [CreateAssetMenu(fileName = "IntSequenceConfig", menuName = "Configs/NumericSequences/IntSequenceConfig")]
    public class IntSequenceConfig : ScriptableObject, IIntSequence
    {
        [SerializeField] private List<int> sequence;
        [SerializeField] private int defaultNumber;
        private int _currentIndex;
        
        public int Next()
        {
            if (sequence.Count == 0 || _currentIndex >= sequence.Count) return defaultNumber;
            return sequence[_currentIndex++];
        }
    }
}