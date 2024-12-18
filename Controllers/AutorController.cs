using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EF.Exemplo1.Models;
using MVC_EF.Exemplo1;  // Adicionando o namespace do ApplicationDbContext

namespace MVC_EF.Exemplo1.Controllers
{
    public class AutorController : Controller
    {
        private readonly ApplicationDbContext _context;  // Usar ApplicationDbContext

        // Injeção de dependência do ApplicationDbContext
        public AutorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método Index - Listar todos os autores
        public async Task<IActionResult> Index()
        {
            var autores = await _context.Autores.ToListAsync();
            return View("~/Views/Autor/Index.cshtml", autores);  // Forçando o caminho da view
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
                _context.Add(autor);  // Adicionando o autor no contexto
                await _context.SaveChangesAsync();  // Salvando no banco de dados
                return RedirectToAction(nameof(Index));  // Redirecionando para a página de listagem
            }
            return View(autor);  // Caso o modelo não seja válido, retornando a mesma view
        }
        
        // Método Edit - Exibir formulário de edição do autor
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }
        
        // Método Edit (POST) - Atualizar o autor no banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AutorID, AutorNome, AutorDataNascimento, AutorEmail")] Autor autor)
        {
            if (id != autor.AutorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autor);  // Atualizando o autor no contexto
                    await _context.SaveChangesAsync();  // Salvando alterações no banco de dados
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Autores.Any(e => e.AutorID == autor.AutorID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));  // Redirecionando para a página de listagem
            }
            return View(autor);  // Caso o modelo não seja válido, retornando a mesma view
        }
        
        // Método Delete - Exibir confirmação de exclusão
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autores
                .FirstOrDefaultAsync(m => m.AutorID == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor != null)
            {
                _context.Autores.Remove(autor);  // Removendo o autor do contexto
                await _context.SaveChangesAsync();  // Salvando a exclusão no banco de dados
            }
            return RedirectToAction(nameof(Index));  // Redirecionando para a página de listagem
        }



    }
}