using NerdStore.Catalogo.Domain.Events;
using NerdStore.Core.Communication.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain
{
    //Serviço Cross Aggregate: trabalha com 2 ou mais entidades,
    //ou quando sua regrea de negócio não cabe nem numa camada de aplicação,
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
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;
            if(!produto.PossuiEstoque(quantidade)) return false;

            produto.DebitarEstoque(quantidade);

            //TODO: Parametrizar a quantidade de estoque baixo
            if(produto.QuantidadeEstoque < 10)
            {
                // avisar, enviar e-mail, abrir chamado, realizar nova compra etc.
               await _mediator.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
            }

            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;
            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}
