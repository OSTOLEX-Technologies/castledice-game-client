namespace Src.PVE
{
    public interface IUnconnectedValuesCutter<T>
    {
        /// <summary>
        /// CutUnconnectedValues checks if cells with unitState are connected to cells with baseState.
        /// If not, it changes them to freeState.
        /// States different than unitState, baseState and freeState are not changed.
        /// Example of work:
        /// unitState = 1, baseState = 2, freeState = 0
        /// input matrix:
        /// 2 1 1 0 1
        /// 1 1 1 0 1
        /// 0 0 0 0 1
        /// 1 1 1 1 1
        /// matrix after CutUnconnectedValues:
        /// 2 1 1 0 0
        /// 1 1 1 0 0
        /// 0 0 0 0 0
        /// 0 0 0 0 0
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="unitState"></param>
        /// <param name="baseState"></param>
        /// <param name="freeState"></param>
        void CutUnconnectedValues(T[,] matrix, T unitState, T baseState, T freeState);
    }
}