using System.Collections.Generic;
using UnityEngine;

namespace Src.General.NumericSequences
{
    [CreateAssetMenu(fileName = "IntSequenceConfig", menuName = "Configs/NumericSequences/IntSequenceConfig")]
    public class IntSequenceConfig : ScriptableObject
    {
        [SerializeField] private List<int> sequence;
        [SerializeField] private int defaultNumber;
        
        public IReadOnlyList<int> Sequence => sequence;
        public int DefaultNumber => defaultNumber;
    }
}