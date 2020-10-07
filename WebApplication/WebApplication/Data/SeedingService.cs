using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Models.Enums;

namespace WebApplication.Data
{
    public class SeedingService
    {
        private WebApplicationContext _context;

        public SeedingService(WebApplicationContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Departamento.Any() || _context.Vendedor.Any() || _context.RegistroVenda.Any())
            {
                return;
            }

            Departamento d1 = new Departamento(1, "Gamer");
            Departamento d2 = new Departamento(2, "Hardware");
            Departamento d3 = new Departamento(3, "Livros");
            Departamento d4 = new Departamento(4, "Carros");

            Vendedor v1 = new Vendedor(1, "Igor Pansini", "impansini@gmail.com", 9000.0, new DateTime(2000, 12, 9), d1);
            Vendedor v2 = new Vendedor(2, "Gustavo Henrique", "gustavo.henrique@hotmail.com", 7000.0, new DateTime(1997, 03, 24), d2);
            Vendedor v3 = new Vendedor(3, "Adryan Saldanha", "adryan_saldanha@gmail.com", 7000.0, new DateTime(1998, 10, 12), d3);
            Vendedor v4 = new Vendedor(4, "Andre Henrique", "andrehenrique@yahoo.com", 7000.0, new DateTime(1986, 11, 10), d4);

            RegistroVenda rv1 = new RegistroVenda(1, new DateTime(2020,10,7), 10000.0, StatusVenda.Pendente, v1);
            RegistroVenda rv2 = new RegistroVenda(2, new DateTime(2019, 11, 28), 13000.0, StatusVenda.Faturado, v1);
            RegistroVenda rv3 = new RegistroVenda(3, new DateTime(2020, 8, 15), 9000.0, StatusVenda.Faturado, v2);
            RegistroVenda rv4 = new RegistroVenda(4, new DateTime(2018, 5, 10), 7000.0, StatusVenda.Cancelado, v3);
            RegistroVenda rv5 = new RegistroVenda(5, new DateTime(2020, 1, 21), 5000.0, StatusVenda.Faturado, v4);

            _context.Departamento.AddRange(d1, d2, d3, d4);
            _context.Vendedor.AddRange(v1, v2, v3, v4);
            _context.RegistroVenda.AddRange(rv1, rv2, rv3, rv4, rv5);

            _context.SaveChanges();
        }
    }
}
