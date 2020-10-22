using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Models.ViewModels;
using WebApplication.Services;
using WebApplication.Services.Exceptions;

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
                return RedirectToAction(nameof(Error), new { msg = "Id não informado."});
            }

            var obj = _vendedorService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { msg = "Id não encontrado." });
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
                return RedirectToAction(nameof(Error), new { msg = "Id não informado." });
            }

            var obj = _vendedorService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { msg = "Id não encontrado." });
            }

            return View(obj);
        }

        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { msg = "Id não informado." });
            }

            var obj = _vendedorService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { msg = "Id não encontrado." });
            }

            List<Departamento> departamentos = _departamentoService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, Vendedor vendedor)
        {
            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { msg = "Id não corresponde." });
            }
            try
            {
                _vendedorService.Update(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { msg = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { msg = e.Message });
            }
        }

        public IActionResult Error(string msg)
        {
            var viewModel = new ErrorViewModel { Msg = msg, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier};
            return View(viewModel);
        }
    }
}
