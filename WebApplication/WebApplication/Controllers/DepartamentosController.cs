using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class DepartamentosController : Controller
    {
        public IActionResult Index()
        {
            List<Departamento> lista = new List<Departamento>();
            lista.Add(new Departamento { Id = 1, nome = "Eletronicos" });
            lista.Add(new Departamento { Id = 2, nome = "Vestuario" });
            return View(lista);
        }
    }
}
