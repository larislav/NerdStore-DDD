using NerdStore.Catalogo.Domain.Events;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects.DTO;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain
{
    //Serviço Cross Aggregate: trabalha com 2 ou mais entidades,
    //ou quando sua regra de negócio não cabe nem numa camada de aplicação,
    //nem numa entidade
    public class EstoqueService : IEstoqueService
    {
        //Serviço de domínio não é a camada de aplicação
        //EstoqueService vai resolver um problema que a
        //própria classe Produto não é capaz de resolver,
        //porque envolve acesso a dados externos etc.

        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatrHandler _mediator;
        public EstoqueService(IProdutoRepository produtoRepository, IMediatrHandler mediator)
        {
            _produtoRepository = produtoRepository;
            _mediator = mediator;
        }
        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            if (!await DebitarItemEstoque(produtoId, quantidade)) return false;

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DebitarListaProdutosPedido(ListaProdutosPedido lista)
        {
            foreach (var item in lista.Itens)
            {
                if (!await DebitarItemEstoque(item.Id, item.Quantidade)) return false;
            }

            return await _produtoRepository.UnitOfWork.Commit();
        }

        private async Task<bool> DebitarItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);
            if (produto == null) return false;

            if (!produto.PossuiEstoque(quantidade))
            {
                await _mediator.PublicarNotificacao(new DomainNotification("Estoque", $"Produto = {produto.Nome} sem estoque"));
                return false;
            }

            produto.DebitarEstoque(quantidade);

            // TODO: 10 pode ser parametrizável em arquivo de configuração
            if(produto.QuantidadeEstoque < 10)
            {
                await _mediator.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
            }

            _produtoRepository.Atualizar(produto);
            return true;
        }

        public async Task<bool> ReporListaProdutosPedido(ListaProdutosPedido lista)
        {
            foreach (var item in lista.Itens)
            {
                await ReporItemEstoque(item.Id, item.Quantidade);
            }

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var sucesso = await ReporItemEstoque(produtoId, quantidade);

            if (!sucesso) return false;

            return await _produtoRepository.UnitOfWork.Commit();
        }

        private async Task<bool> ReporItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if(produto == null) return false;

            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);

            return true;

        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}
