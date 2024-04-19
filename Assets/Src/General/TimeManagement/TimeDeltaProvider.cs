namespace Src.General.TimeManagement
{
    public class TimeDeltaProvider : ITimeDeltaProvider
    {
        public float GetDeltaTime()
        {
            return UnityEngine.Time.deltaTime;
        }
    }
}