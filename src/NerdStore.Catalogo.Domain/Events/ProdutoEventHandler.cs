using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.Events
{
    public class ProdutoEventHandler : 
        INotificationHandler<ProdutoAbaixoEstoqueEvent>,
        INotificationHandler<PedidoIniciadoEvent>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueService _estoqueService;
        private readonly IMediatrHandler _mediator;

        public ProdutoEventHandler(IProdutoRepository produtoRepository,
            IEstoqueService estoqueService, IMediatrHandler mediator)
        {
            _produtoRepository = produtoRepository;
            _estoqueService = estoqueService;
            _mediator = mediator;
        }
        public async Task Handle(ProdutoAbaixoEstoqueEvent mensagem, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorId(mensagem.AggregateId);

            // enviar um e-mail para aquisição de mais produtos
            // Criar envio de e-mail na camada de infra
        }

        //Evento de integração:
        //inserir numa fila e ter um subscription nele
        public async Task Handle(PedidoIniciadoEvent mensagem, CancellationToken cancellationToken)
        {
            var result = await _estoqueService.DebitarListaProdutosPedido(mensagem.ProdutosPedido);
            // Lembrando que Eventos são sempre no passado, ou seja, já aconteceu

            if (result)
            {
                // PedidoEstoqueConfirmadoEvent vai ser interpretado pelo contexto de pagamento,
                //que vai entender como sinal verde para iniciar o processo de pagamento.

                await _mediator.PublicarEvento(new PedidoEstoqueConfirmadoEvent(mensagem.PedidoId, 
                    mensagem.ClienteId, mensagem.Total, mensagem.ProdutosPedido, mensagem.NomeCartao,
                    mensagem.NumeroCartao, mensagem.ExpiracaoCartao, mensagem.CvvCartao));
            }
            else
            {
                await _mediator.PublicarEvento(new PedidoEstoqueRejeitadoEvent(mensagem.PedidoId, mensagem.ClienteId));
            }
        }
    }
}
