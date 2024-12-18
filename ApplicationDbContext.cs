using Microsoft.EntityFrameworkCore;
using MVC_EF.Exemplo1.Models;

namespace MVC_EF.Exemplo1;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Livro> Livros { get; set; }
    public DbSet<Autor> Autores { get; set; }
    public DbSet<Editora> Editoras { get; set; }
    public DbSet<AutorLivro> AutoresLivro { get; set; }
    public DbSet<OperacaoCompraVenda> Operacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new LivroConfiguration());
        modelBuilder.ApplyConfiguration(new AutorLivroConfiguration());
        modelBuilder.ApplyConfiguration(new AutorConfiguration());
        modelBuilder.ApplyConfiguration(new EditoraConfiguration());
        modelBuilder.ApplyConfiguration(new OperacaoCompraVendaConfiguration());
    }
}