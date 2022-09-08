using ControleFinanceiro.Api;
using ControleFinanceiro.Api.Loggin;
using ControleFinanceiro.CrossCutting.Ioc;
using ControleFinanceiroInfrastructure.Contexts;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ControleFinanceiroApi
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// Configuracao da API
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuracao da API</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Adiciona os servidores ao container. Esse metodo e chamado pelo runtime.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<SqlServerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: options =>
                {
                    options.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                }));

            services.AddAutoMapper(typeof(WebApiAutoMapperProfile));

            services.AddLoggingSerilog();

            services.AddDependencyResolver();

            services.AddHealthChecks();
        }

        /// <summary>
        /// Configura as requisicoes da API. Esse metodo e chamado pelo runtime.
        /// </summary>
        /// <param name="app">aplicacao</param>
        /// <param name="env">ambiente</param>
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseAuthorization();

            app.MapControllers();

            app.UseHealthChecks("/health/liveness", new HealthCheckOptions
            {
                Predicate = _ => false,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }
    }
}
