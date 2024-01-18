namespace Src.TimeManagement
{
    public class FixedTimeDeltaProvider : ITimeDeltaProvider
    {
        public float GetDeltaTime()
        {
            return UnityEngine.Time.fixedDeltaTime;
        }
    }
}