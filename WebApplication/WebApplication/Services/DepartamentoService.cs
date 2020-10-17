using System.Collections.Generic;
using System.Linq;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class DepartamentoService
    {
        private readonly WebApplicationContext _context;

        public DepartamentoService(WebApplicationContext context)
        {
            _context = context;
        }

        public List<Departamento> FindAll()
        {
            return _context.Departamento.OrderBy(x => x.nome).ToList();
        }
    }
}
