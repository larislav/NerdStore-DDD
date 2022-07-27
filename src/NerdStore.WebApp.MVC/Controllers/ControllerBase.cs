using Microsoft.AspNetCore.Mvc;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class ControllerBase : Controller
    {
        // Simular cliente logado
        protected Guid ClienteId = Guid.Parse("4D42138C-2C8B-40A7-BAD2-0A36B5364FFA");
    }
}
