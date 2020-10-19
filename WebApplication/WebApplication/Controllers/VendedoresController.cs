using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Models.ViewModels;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedorService;
        private readonly DepartamentoService _departamentoService;

        public VendedoresController(VendedorService vendedorService, DepartamentoService departamentoService)
        {
            _vendedorService = vendedorService;
            _departamentoService = departamentoService;
        }
        public IActionResult Index()
        {
            var list = _vendedorService.FindAll();
            return View(list);
        }

        public IActionResult Adicionar()
        {
            var departamentos = _departamentoService.FindAll();
            var viewModel = new SellerFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(Vendedor vendedor)
        {
            _vendedorService.Insert(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _vendedorService.FindById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deletar(int id)
        {
            _vendedorService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _vendedorService.FindById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
    }
}
