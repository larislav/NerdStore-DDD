using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Application.ViewModels;

namespace NerdStore.WebApp.MVC.Controllers.Admin
{
    public class AdminProdutosController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;
        public AdminProdutosController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet("admin-produtos")]
        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoAppService.ObterTodos();
            return View(produtos);
        }

        [Route("novo-produto")]
        public async Task<IActionResult> NovoProduto()
        {
            return View( await PopularCategorias(new ProdutoViewModel()));
        }

        [HttpPost("novo-produto")]
        public async Task<IActionResult> NovoProduto(ProdutoViewModel produtoViewModel)
        {
            ModelState.Remove("Categorias");
            if (!ModelState.IsValid) return View(await PopularCategorias(produtoViewModel));

            await _produtoAppService.AdicionarProduto(produtoViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id)
        {
            var produtoViewModel = await PopularCategorias(await _produtoAppService.ObterPorId(id));
            return View(produtoViewModel);
        }

        [HttpPost("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id, ProdutoViewModel produtoViewModel)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            produtoViewModel.QuantidadeEstoque = produto.QuantidadeEstoque;

            ModelState.Remove("QuantidadeEstoque");
            if (!ModelState.IsValid || produto.Id != id) return View(await PopularCategorias(produtoViewModel));

            await _produtoAppService.AtualizarProduto(produtoViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id)
        {
            var produtoViewModel = await _produtoAppService.ObterPorId(id);
            return View("Estoque", produtoViewModel);
        }

        [HttpPost("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id, int quantidade)
        {
            if(quantidade > 0)
            {
                await _produtoAppService.ReporEstoque(id, quantidade);
            }
            else
            {
                await _produtoAppService.DebitarEstoque(id, quantidade);
            }

            var produtoViewModel = await _produtoAppService.ObterTodos();

            return View("Index", produtoViewModel);
        }

        private async Task<ProdutoViewModel> PopularCategorias(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel.Categorias = await _produtoAppService.ObterCategorias();
            return produtoViewModel;
        }
    }
}
