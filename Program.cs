using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Components;
using SistemaQuinielaMundialistasV2.Data;
using SistemaQuinielaMundialistasV2.Repositories;
using SistemaQuinielaMundialistasV2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

string connectionString = builder.Configuration.GetConnectionString("QuinielasDb")
    ?? throw new InvalidOperationException("No se encontró la conexión 'QuinielasDb'.");

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<DatabaseStatusService>();
builder.Services.AddSingleton<PasswordService>();
builder.Services.AddScoped<SessionService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AdminUsuarioService>();
builder.Services.AddScoped<FechaSimuladaService>();
builder.Services.AddScoped<PartidoService>();
builder.Services.AddScoped<PronosticoService>();
builder.Services.AddScoped<NotificacionService>();
builder.Services.AddScoped<RankingService>();
builder.Services.AddScoped<InsigniaV2Service>();
builder.Services.AddScoped<QuinielaV2Service>();
builder.Services.AddScoped<EstadisticaV2Service>();
builder.Services.AddSingleton<ReporteExportService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await DatabaseInitializer.InitializeAsync(app.Services);

app.Run();
