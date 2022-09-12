using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiro.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Mail;

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
            RegisterClients(services);
        }

        private static void RegisterApplications(IServiceCollection services)
        {
            services.AddScoped<ILancamentoApplication, LancamentoApplication>();
            services.AddScoped<IEmailService, EmailApplication>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<ILancamentoRepository, LancamentoRepository>();
        }

        private static void RegisterClients(IServiceCollection services)
        {
            services.AddTransient<SmtpClient>((service) =>
            {
                var config = service.GetRequiredService<IConfiguration>();

                return new SmtpClient()
                {
                    Host = config.GetRequiredSection("EmailServer:SmtpHost").Value,
                    Port = Convert.ToInt32(config.GetRequiredSection("EmailServer:SmtpPort").Value),
                    Credentials = new NetworkCredential(
                        config.GetRequiredSection("EmailServer:SmtpUser").Value,
                        config.GetRequiredSection("EmailServer:SmtpPass").Value
                    )
                };
            });
        }
    }
}