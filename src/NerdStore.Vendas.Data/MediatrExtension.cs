using System.Linq;
using System.Threading.Tasks;
using NerdStore.Core.Communication.Mediatr;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Vendas.Data
{
    public static class MediatrExtension
    {
        public static async Task PublicarEventos(this IMediatrHandler mediator, VendasContext context){
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Eventos != null && x.Entity.Eventos.Any());

            var domainEvent = domainEntities
                .SelectMany(x => x.Entity.Eventos)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainEvent
                .Select(async (domainEvent) =>
                {
                    await mediator.PublicarEvento(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}