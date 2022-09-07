using ControleFinanceiro.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using ControleFinanceiro.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.CrossCutting.Ioc
{
    public static class DependencyResolver
    {
        /// <summary>
        /// Parametriza o AddDependencyResolver 
        /// </summary>
        /// <param name="services">IoC</param>
        /// <returns></returns>
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            RegisterApplications(services);
            RegisterRepositories(services);
        }

        private static void RegisterApplications(IServiceCollection services)
        {
            services.AddScoped<ILancamentoApplication, LancamentoApplication>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            // services.AddScoped<ILancamentoRepository, LancamentoRepository>();
        }
    }
}
