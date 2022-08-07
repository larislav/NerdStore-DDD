using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PagamentoRealizadoEvent : IntegrationEvent
    {
        // Caso houvesse um banco de leitura, teria que obter essas informações
        // deste event para registrar nesse banco de leitura
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid PagamentoId { get; private set; }
        public Guid TransacaoId { get; private set; }
        public decimal Total { get; private set; }

        public PagamentoRealizadoEvent(Guid pedidoId, Guid clienteId, Guid pagamentoId,
            Guid transacaoId, decimal total)
        {
            AggregateId = pagamentoId;
            PedidoId = pedidoId;
            ClienteId = clienteId;
            PagamentoId = pagamentoId;
            TransacaoId = transacaoId;
            Total = total;
        }

    } 
}
