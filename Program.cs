using Microsoft.EntityFrameworkCore;
using MVC_EF.Exemplo1;

var builder = WebApplication.CreateBuilder(args);

// Adicionando os serviços ao contêiner.
builder.Services.AddControllersWithViews();

// Configurando o DbContext para usar o PostgreSQL.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Pgsql"))
);

var app = builder.Build();

// Configuração do pipeline de requisições HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor HSTS padrão é de 30 dias. Para produção, altere conforme necessário.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Livro}/{action=Index}/{id?}");

app.Run();