using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Something.Like.Api;
using Something.Like.Bot;

namespace Something.Like.Main;

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
				// bot settings
				services
					.AddBotConfiguration(context.Configuration)
					.AddTelegramBotClient("default_bot_client")
					.AddScoped<UpdateBotHandler>()
					.AddScoped<ReceiverBotService>()
					.AddHostedService<PollingBotService>();

				// api settings
				services
					.AddApiConfiguration(context.Configuration)
					.AddApiClient("default_api_client")
					.AddScoped<UpdateApiHandler>()
					.AddScoped<ReceiverApiService>()
					.AddHostedService<PollingApiService>();
			})
			.Build()
			.RunAsync();
}
