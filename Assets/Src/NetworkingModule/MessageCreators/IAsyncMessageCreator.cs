using System.Threading.Tasks;
using Riptide;

namespace Src.NetworkingModule.MessageCreators
{
    public interface IAsyncMessageCreator
    {
        Task<Message> GetMessageAsync();
    }
}