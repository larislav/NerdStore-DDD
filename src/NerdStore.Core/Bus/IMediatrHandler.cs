using NerdStore.Core.Messages;

namespace NerdStore.Core.Bus
{
    // O mediator pode ser uma interface para o Bus, mas não é um Bus
    public interface IMediatrHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
    }
}
