using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Core.Communication.Mediatr
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediator;

        public MediatrHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> EnviarComando<T>(T comando) where T : Command =>
            await _mediator.Send(comando);

        public async Task PublicarEvento<T>(T evento) where T : Event =>
            await _mediator.Publish(evento);

        public async Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification =>
            await _mediator.Publish(notificacao);
    }
}
