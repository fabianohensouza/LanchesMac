using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModel;
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

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches
                            .Where(l => l.Categoria.CategoriaNome.Equals(categoria))
                            .OrderBy(l => l.LancheId);

                categoriaAtual = categoria;
            }

            var lanchesListViewModel = new LanchesListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            ViewData["Title"] = "Categoria: " + categoriaAtual;
            return View(lanchesListViewModel);
        }

        public IActionResult Details(int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanches
                                    .FirstOrDefault(l => l.LancheId == lancheId);

            return View(lancheSelecionado);
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lancheRepository.Lanches.OrderBy(x => x.CategoriaId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches
                    .Where(x => x.Nome.ToLower().Contains(searchString.ToLower()));

                if (lanches.Any())
                    categoriaAtual = "Lanches";
                else
                    categoriaAtual = "Nenhum Lanche encontrado";
            }

            return View("~/Views/Lanche/List.cshtml", new LanchesListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            });
        }
    }
}
