using NerdStore.Core.Messages;

namespace NerdStore.Core.Bus
{
    // O mediator pode ser uma interface para o Bus, mas não é um Bus
    // O mediator é baseado em request e notificação.
    // Toda vez que eu envio um comando, como um Send, é um request.
    // E um evento é algo que quero apenas disparar, então o Publish é uma notificação
    // de alguma coisa
    public interface IMediatrHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;

        Task<bool> EnviarComando<T>(T comando) where T : Command;
    }
}
