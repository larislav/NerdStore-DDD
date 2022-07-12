using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.Messages
{
    public abstract class Message
    {
        // Todo evento será entregue através da ferramenta Mediator

        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        public Message()
        {
            //GetType().Name = obtendo o nome da classe que está herdando a classe Message
            MessageType = GetType().Name;
        }
    }
}
