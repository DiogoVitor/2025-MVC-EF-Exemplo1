using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVC_EF.Exemplo1.Models;

public class Autor
{
    public int AutorID { get; set; }
    public string AutorNome { get; set; }
    public DateTime? AutorDataNascimento { get; set; }
    public string? AutorEmail { get; set; }

    public ICollection<AutorLivro>? LivrosDoAutor { get; set; }
}

public class AutorConfiguration : IEntityTypeConfiguration<Autor>
{
    public void Configure(EntityTypeBuilder<Autor> builder)
    {
        builder.HasKey(p => p.AutorID);

        builder.HasIndex(p => p.AutorNome);

        builder.Property(p => p.AutorNome).HasMaxLength(80).IsRequired();
        builder.Property(p => p.AutorEmail).HasMaxLength(80);

        // Corrigido para definir o valor padrão da data corretamente
        builder.Property(p => p.AutorDataNascimento)
            .HasDefaultValue(new DateTime(1970, 1, 1));  // Definindo o valor default de 01/01/1970
    }
}