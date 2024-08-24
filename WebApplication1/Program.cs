using Microsoft.EntityFrameworkCore;
using WebApplication1.Datos;

var builder = WebApplication.CreateBuilder(args);

// Configurar la cadena de conexión desde el archivo de configuración
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configurar el contexto de datos de Entity Framework Core para usar PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar el pipeline de la solicitud HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=Index}/{id?}");

app.Run();

