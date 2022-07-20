using Microsoft.AspNetCore;
using Modelo.Application.Interfaces;
using Modelo.Application.Mapping;
using Modelo.Application.Services;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Services;
using Modelo.Infra.Data.Interface;
using Modelo.Infra.Data.Repository;
namespace Modelo.Host
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Aplicação
            builder.Services.AddScoped<IConverterProduto, ConverterProduto>();
            builder.Services.AddScoped<IConverterUsuario, ConverterUsuario>();
            builder.Services.AddScoped<IConverterVenda, ConverterVenda>();
            builder.Services.AddScoped<IProcessarMsgAcaoProdutoAppService, ProcessarMsgAcaoProdutoAppService>();
            builder.Services.AddScoped<IProcessarMsgAcaoUsuarioAppService, ProcessarMsgAcaoUsuarioAppService>();
            builder.Services.AddScoped<IProcessarMsgAcaoVendaAppService, ProcessarMsgAcaoVendaAppService>();
            //Domain
            builder.Services.AddScoped<IProdutoService, ProdutoService>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IVendaService, VendaService>();
            //Infra
            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IVendaRepository, VendaRepository>();
            builder.Services.AddScoped<IAzureRepository, AzureRepository>();
            builder.Services.AddScoped<IBaseRepository, BaseRepository>();
            // Add services to the container.
            builder.Services.AddControllers();
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
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}


