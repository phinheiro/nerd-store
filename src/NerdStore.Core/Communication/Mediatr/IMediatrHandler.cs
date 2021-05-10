using System.Threading.Tasks;
using NerdStore.Core.Messages;

namespace NerdStore.Core.Communication.Mediatr
{
    public interface IMediatrHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<bool> EnviarComando<T>(T comando) where T : Command;
    }
}
