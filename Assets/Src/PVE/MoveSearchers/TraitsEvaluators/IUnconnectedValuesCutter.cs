using Src.PVE.TraitsEvaluators;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public interface IUnconnectedValuesCutter<T>
    {
        /// <summary>
        /// Replaces unitsState elements that are not connected to baseState with freeState.
        /// For example, if we take 0 as base state, 1 as free state and 2 as unitState, then this method
        /// should turn this matrix:
        /// 0 1 1 2
        /// 1 2 1 2
        /// 1 1 1 2
        /// 2 2 2 2
        /// into this matrix:
        /// 0 1 1 1
        /// 1 2 1 1
        /// 1 1 1 1
        /// 1 1 1 1
        /// </summary>
        /// <param name="inputMatrix"></param>
        /// <param name="unitState"></param>
        /// <param name="baseState"></param>
        /// <param name="freeState"></param>
        /// <returns></returns>
        public T[,] CutUnconnectedUnits(T[,] inputMatrix, T unitState, T baseState, T freeState);
    }
}