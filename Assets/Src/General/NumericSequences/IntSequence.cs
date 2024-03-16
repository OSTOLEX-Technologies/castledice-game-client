using System.Collections.Generic;

namespace Src.General.NumericSequences
{
    public class IntSequence : IIntSequence
    {
        private readonly List<int> _sequence;
        private readonly int _defaultNumber;
        private int _currentIndex;
        
        public IntSequence(List<int> sequence, int defaultNumber)
        {
            _sequence = sequence;
            _defaultNumber = defaultNumber;
        }
        
        public int Next()
        {
            if (_sequence.Count == 0 || _currentIndex >= _sequence.Count) return _defaultNumber;
            return _sequence[_currentIndex++];
        }
    }
}