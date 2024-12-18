using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EF.Exemplo1.Models;

namespace MVC_EF.Exemplo1.Controllers;

public class EditoraController : Controller
{
    private readonly ApplicationDbContext _contexto;
    private readonly int _pagesize;

    public EditoraController(ApplicationDbContext contexto, IConfiguration configuration)
    {
        _contexto = contexto;
        var pagesize = Convert.ToInt32(configuration
            .GetSection("ViewOptions")
            .GetSection("PageSize").Value);
        _pagesize = pagesize > 0 ? pagesize : 10;
    }

    // Corrigido: Implementação do método Index
    public IActionResult Index()
    {
        // Busca todas as editoras no banco de dados
        var editoras = _contexto.Editoras.ToList();

        // Passa os dados para a View
        return View(editoras);
    }

    public IActionResult Create()
    {
        var novo = new EditoraEditViewModel();

        return View(novo);
    }

    [HttpPost, ActionName("Create")]
    public IActionResult CreatePost(EditoraEditViewModel editora)
    {
        if (!ModelState.IsValid)
        {
            var novo = new EditoraEditViewModel
            {
                EditoraNome = editora.EditoraNome,
                EditoraLogradouro = editora.EditoraLogradouro,
                EditoraNumero = editora.EditoraNumero,
                EditoraComplemento = editora.EditoraComplemento,
                EditoraCidade = editora.EditoraCidade,
                EditoraUF = editora.EditoraUF,
                EditoraPais = editora.EditoraPais,
                EditoraCEP = editora.EditoraCEP,
                EditoraTelefone = editora.EditoraTelefone
            };

            return View(novo);
        }

        var novaEditora = new Editora
        {
            EditoraNome = editora.EditoraNome,
            EditoraLogradouro = editora.EditoraLogradouro,
            EditoraNumero = editora.EditoraNumero,
            EditoraComplemento = editora.EditoraComplemento,
            EditoraCidade = editora.EditoraCidade,
            EditoraUF = editora.EditoraUF,
            EditoraPais = editora.EditoraPais,
            EditoraCEP = editora.EditoraCEP,
            EditoraTelefone = editora.EditoraTelefone
        };

        _contexto.Editoras.Add(novaEditora);

        try
        {
            _contexto.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e);
            throw;
        }

        return RedirectToAction(nameof(Index));
    }
}