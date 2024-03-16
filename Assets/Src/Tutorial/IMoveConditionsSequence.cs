using Src.General.MoveConditions;

namespace Src.Tutorial
{
    public interface IMoveConditionsSequence
    {
        IMoveCondition GetCurrentCondition();
        void MoveToNextCondition();
        bool HasNext();
    }
}