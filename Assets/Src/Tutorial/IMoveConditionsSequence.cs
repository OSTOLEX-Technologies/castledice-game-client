using Src.PVE.MoveConditions;

namespace Src.Tutorial
{
    public interface IMoveConditionsSequence
    {
        IMoveCondition GetCurrentCondition();
        void MoveToNextCondition();
    }
}