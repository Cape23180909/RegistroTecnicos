using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Components;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Services;

namespace RegistroTecnicos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var ConStr = builder.Configuration.GetConnectionString("SqlConStr");
            builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConStr));
            //Iconos de Bootstrap
            builder.Services.AddBlazorBootstrap();
            //Injectamos el Service
            builder.Services.AddScoped<TecnicoService>();
            builder.Services.AddScoped<TipoTecnicoService>();
            builder.Services.AddScoped<ClienteService>();
            builder.Services.AddScoped<TrabajoService>();
            builder.Services.AddScoped<PrioridadService>();
            builder.Services.AddScoped<ArticuloService>();
            builder.Services.AddScoped<CotizacionService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}