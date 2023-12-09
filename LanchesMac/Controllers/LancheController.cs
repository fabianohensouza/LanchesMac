using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List()
        {
            ViewData["Title"] = "Todos os lanches";
            ViewData["Date"] = DateTime.Now.Date;

            var lanches = _lancheRepository.Lanches;

            ViewBag.Total = "Total de lanches : ";
            ViewBag.TotalLanches = lanches.Count();

            return View(lanches);
        }
    }
}
