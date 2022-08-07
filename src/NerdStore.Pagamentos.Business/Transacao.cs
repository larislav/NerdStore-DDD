using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Pagamentos.Business
{
    // Transação é o objeto devolvido pelo seu gateway de pagamento
    public class Transacao : Entity
    {
        public Guid PedidoId { get; set; }
        public Guid PagamentoId { get; set; }
        public decimal Total { get; set; }
        public StatusTransacao StatusTransacao { get; set; }

        // EF. Rel.
        public Pagamento Pagamento { get; set; }
    }
}