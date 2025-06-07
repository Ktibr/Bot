using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Something.Like;

internal static class Startup
{
	public static Task Main(string[] args)
		=> Host
			.CreateDefaultBuilder(args)
			.ConfigureHostConfiguration(config => config.AddEnvironmentVariables(ConfigHelper.EnvVarMask))
			.ConfigureAppConfiguration((_, configBuilder) =>
			{
				configBuilder
					.AddEnvironmentVariables()
					.AddCommandLine(args)
					.AddJsonFile("appsettings.json"); // TODO
			})
			.ConfigureServices((context, services) =>
			{
				services
					.AddBotTelegramConfiguration(context.Configuration)
					.AddApiConfiguration(context.Configuration);
			})
			.Build()
			.RunAsync();
}
