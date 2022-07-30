using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class VitrineController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;
        public VitrineController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }


        [HttpGet]
        [Route("")]
        [Route("Vitrine")]
        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoAppService.ObterTodos();
            return View(produtos);
        }

        [HttpGet("produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            return View(produto);
        }
    }
}
