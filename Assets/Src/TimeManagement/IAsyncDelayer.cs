using System;
using System.Threading.Tasks;

namespace Src.TimeManagement
{
    public interface IAsyncDelayer
    {
        public Task Delay(TimeSpan time);
    }
}