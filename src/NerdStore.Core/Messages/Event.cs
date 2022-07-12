using MediatR;

namespace NerdStore.Core.Messages
{
    // A interface INotification é uma interface de marcação,
    // para saber que esta classe é uma classe que entrega notificações

    // Todo evento é uma mensagem, e deriva de notificação
    public abstract class Event : Message, INotification
    {
        public DateTime TimeStamp { get; private set; }

        public Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
