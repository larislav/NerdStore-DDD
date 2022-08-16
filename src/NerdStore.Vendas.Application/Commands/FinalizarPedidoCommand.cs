using NerdStore.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Commands
{
    public class FinalizarPedidoCommand : Command
    {
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }

        public FinalizarPedidoCommand(Guid pedidoId, Guid clienteId)
        {
            AggregateId = pedidoId;
            ClienteId = clienteId;
            PedidoId = pedidoId;
        }
    }
}
