namespace Src.TimeManagement
{
    public interface ITimeDeltaProvider
    {
        float GetDeltaTime();
        float GetFixedDeltaTime();
    }
}