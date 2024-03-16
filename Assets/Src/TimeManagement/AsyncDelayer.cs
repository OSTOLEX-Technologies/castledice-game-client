using System;
using System.Threading.Tasks;

namespace Src.TimeManagement
{
    public class AsyncDelayer : IAsyncDelayer
    {
        public Task Delay(TimeSpan time)
        {
            return Task.Delay(time);
        }
    }
}