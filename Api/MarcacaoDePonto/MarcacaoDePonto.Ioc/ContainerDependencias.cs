using MarcacaoDePonto.Repositorio.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcacaoDePonto.Ioc
{
    public class ContainerDependencia
    {
        public static void RegistrarServicos(IServiceCollection services)
        {
            //repositorios
            services.AddScoped<FuncionarioRepositorio, FuncionarioRepositorio>();

            //services
            services.AddScoped<FuncionarioRepositorio, FuncionarioRepositorio>();
            //services.AddScoped<ClienteService, ClienteService>();
        }
    }
}
