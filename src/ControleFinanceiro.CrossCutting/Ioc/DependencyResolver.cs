using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiro.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ControleFinanceiro.CrossCutting.Ioc
{
    [ExcludeFromCodeCoverage]
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
            services.AddScoped<IEmailService, EmailApplication>();
            services.AddScoped<ISmtpClientGenerator, SmtpClientGenerator>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<ILancamentoRepository, LancamentoRepository>();
        }
    }
}