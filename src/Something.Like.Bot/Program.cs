using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Something.Like.Settings;

namespace Something.Like.Bot;

internal static class Program
{
	public static Task Main(string[] args)
		=> Host
			.CreateDefaultBuilder(args)
			.ConfigureHostConfiguration(config => config.AddEnvironmentVariables("ASPNETCORE_"))
			.ConfigureAppConfiguration((_, configBuilder) =>
			{
				configBuilder
					.AddEnvironmentVariables()
					.AddCommandLine(args);
			})
			.ConfigureServices((context, services) =>
			{
				services
					.AddBotConfiguration(context.Configuration)
					.AddTelegramBotClient("default_bot_client")
					.AddScoped<UpdateHandler>()
					.AddScoped<ReceiverService>()
					.AddHostedService<PollingService>();
			})
			.Build()
			.RunAsync();
}
