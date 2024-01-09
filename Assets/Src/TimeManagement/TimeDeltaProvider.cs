namespace Src.TimeManagement
{
    public class TimeDeltaProvider : ITimeDeltaProvider
    {
        public float GetDeltaTime()
        {
            return UnityEngine.Time.deltaTime;
        }
        
        public float GetFixedDeltaTime()
        {
            return UnityEngine.Time.fixedDeltaTime;
        }
    }
}