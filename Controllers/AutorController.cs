using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EF.Exemplo1.Models;

namespace MVC_EF.Exemplo1.Controllers
{
    public class AutorController : Controller
    {
        private readonly DbContext _context;

        public AutorController(DbContext context)
        {
            _context = context;
        }

        // Método Index - Listar todos os autores
        public async Task<IActionResult> Index()
        {
            var autores = await _context.Set<Autor>().ToListAsync();
            return View(autores);
        }

        // Método Create - Exibir formulário para criar um autor
        public IActionResult Create()
        {
            return View();
        }

        // Método Create (POST) - Salvar o autor no banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AutorNome,AutorDataNascimento,AutorEmail")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }
    }
}