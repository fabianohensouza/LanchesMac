﻿using LanchesMac.Areas.Admin.Services;
using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminRelatorioVendasController : Controller
    {

        private readonly RelatorioVendasServices _relatorioVendasServices;

        public AdminRelatorioVendasController(RelatorioVendasServices relatorioVendasServices)
        {
            _relatorioVendasServices = relatorioVendasServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RelatorioVendaSimples(DateTime? minDate, 
                                                               DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _relatorioVendasServices.FindByDateAsync(minDate, maxDate);
            return View(result);
        }
    }
}
