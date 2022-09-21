using ControleFinanceiro.CrossCutting.Ioc;
using ControleFinanceiro.Domain.Model;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiroInfrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace ControleFinanceiro.CronJob
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            var configuration = BuildAppConfiguration();
            

            _ = RunAsync(configuration);
        }

        private static async Task RunAsync(IConfiguration configuration)
        {
            Log.Information("Inicio do Job");
            try
            {
                var services = new ServiceCollection();
                BuildServices(services, configuration);

                using (var provider = services.BuildServiceProvider())
                {
                    var lancamentoService = provider.GetRequiredService<ILancamentoApplication>();
                    var emailService = provider.GetRequiredService<IEmailService>();

                    Log.Information("Buscando os dias com saldo negativo");
                    var fluxoCaixaNegativo = await lancamentoService.ObterDiasSaldoNegativo();

                    if (fluxoCaixaNegativo.Any(f => f.Data.ToShortDateString == DateTime.Now.ToShortDateString))
                        await emailService.SendAsync(GerarMensagemEmail());

                    Log.Information("Job executado com sucesso");
                }
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "O Job foi encerrado de forma inesperada!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static Email GerarMensagemEmail()
        {
            return new Email()
            {
                Para = "contoso@gmail.com",
                De = "controlefinanceiroapp@gmail.com",
                Assunto = $"Saldo Negativo no dia de hoje {DateTime.Now.ToShortDateString}",
                Mensagem = "Atneção, no dia de hoje você está com saldo negativo."
            };           
        }

        #region Iniciando container de execução

        private static IConfigurationRoot BuildAppConfiguration()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.Information()
                .CreateLogger();

            var directory = Directory.GetParent(Directory.GetCurrentDirectory());
            var fuulName = string.Empty;
            if (directory != null && directory.Parent != null && directory.Parent.Parent != null)
                fuulName = directory.Parent.Parent.FullName;

            var solutionSettings = Path.Combine(fuulName, "appsettings.json");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(solutionSettings, optional: true, reloadOnChange: false) // When running using dotnet run
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false); // When app is published

            return configuration.AddEnvironmentVariables().Build();
        }

        private static void BuildServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddDependencyResolver();
            services.AddDbContext<SqlServerContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString"),
                sqlServerOptionsAction: options =>
                {
                    options.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                }));
        }

        #endregion
    }
}