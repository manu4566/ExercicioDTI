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
        static void Main(string[] args) =>
       CreateHostBuilder(args).Build().Run();

        private static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    //Aplicação
                    services.AddScoped<IConverterProduto, ConverterProduto>();
                    services.AddScoped<IConverterUsuario, ConverterUsuario>();
                    services.AddScoped<IConverterVenda, ConverterVenda>();

                    services.AddScoped<IProcessarMsgAcaoProdutoAppService, ProcessarMsgAcaoProdutoAppService>();
                    services.AddScoped<IProcessarMsgAcaoUsuarioAppService, ProcessarMsgAcaoUsuarioAppService>();
                    services.AddScoped<IProcessarMsgAcaoVendaAppService, ProcessarMsgAcaoVendaAppService>();

                    //Domain

                    services.AddScoped<ICadastrarProdutoService, CadastrarProdutoService>();
                    services.AddScoped<ICadastrarUsuarioService, CadastrarUsuarioService>();
                    services.AddScoped<IRealizarVendaService, RealizarVendaService>();

                    //Infra
                    services.AddScoped<IProdutoRepository, ProdutoRepository>();
                    services.AddScoped<IUsuarioRepository, UsuarioRepository>();
                    services.AddScoped<IVendaRepository, VendaRepository>();

                    services.AddScoped<IAzureRepository, AzureRepository>();
                    services.AddScoped<IBaseRepository, BaseRepository>();                  
                    
                });


                


    }
}


