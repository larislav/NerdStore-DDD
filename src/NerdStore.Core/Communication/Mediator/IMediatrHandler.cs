using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Core.Communication.Mediator
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

        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
    }
}
