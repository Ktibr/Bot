using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Something.Like.Api;
using Something.Like.Configuration;
using Something.Like.Telegram;
using Telegram.Bot;

namespace Something.Like;

/// <summary>
/// Configuration Helper
/// </summary>
public static class ConfigHelper
{
	/// <summary>
	/// Environment Variables Mask
	/// </summary>
	public const string EnvVarMask = "ENV_LIKE_";

	private const string BotToken = EnvVarMask + "BOT_TOKEN";
	private const string ApiToken = EnvVarMask + "API_TOKEN";
	private const string ApiAddress = EnvVarMask + "API_ADDRESS";

	private static T GetSection<T>(this IConfiguration configuration,
		string sectionName = null) where T : class
		=> configuration
			.GetSection(string.IsNullOrEmpty(sectionName) ? typeof(T).Name : sectionName)
			.Get<T>();

	private static string GetEnvVar(this string envVarName, IConfiguration configuration)
		=> configuration.GetSection(envVarName).Value;

	private static IServiceCollection AddTelegramBotClient(this IServiceCollection services)
	{
		services
			.AddHttpClient(nameof(BotConfiguration))
			.RemoveAllLoggers()
			.AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
			{
				var botConfiguration = sp.GetService<IOptions<BotConfiguration>>()?.Value;
				ArgumentNullException.ThrowIfNull(botConfiguration);
				TelegramBotClientOptions options = new(botConfiguration.Token);
				return new TelegramBotClient(options, httpClient);
			});

		return services;
	}

	private static IServiceCollection AddTelegramBotConfigurationOptions(this IServiceCollection services,
		IConfiguration configuration, string sectionName = null)
	{
		var botConfiguration = configuration
			.GetSection<BotConfiguration>(sectionName);

		var token = BotToken.GetEnvVar(configuration);
		if (!string.IsNullOrEmpty(token))
			botConfiguration.Token = token;

		return services
			.AddSingleton(Options.Create(botConfiguration));
	}

	/// <summary>
	/// Add Bot Configuration
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configuration"></param>
	/// <param name="sectionName"></param>
	/// <returns></returns>
	public static IServiceCollection AddBotTelegramConfiguration(this IServiceCollection services,
		IConfiguration configuration, string sectionName = null)
		=> services
			.AddTelegramBotConfigurationOptions(configuration, sectionName)
			.AddTelegramBotClient()
			.AddScoped<UpdateBotHandler>()
			.AddScoped<ReceiverBotService>()
			.AddHostedService<PollingBotService>();

	private static IServiceCollection AddApiHttpClient(this IServiceCollection services,
		IConfiguration configuration, string sectionName = null)
	{
		var apiConfiguration = configuration
			.GetSection<ApiConfiguration>(sectionName);

		ArgumentNullException.ThrowIfNull(apiConfiguration);

		var token = ApiToken.GetEnvVar(configuration);
		if (!string.IsNullOrEmpty(token))
			apiConfiguration.Token = token;

		var address = ApiAddress.GetEnvVar(configuration);
		if (!string.IsNullOrEmpty(address))
			apiConfiguration.Address = address;

		services
			.AddHttpClient(nameof(ApiConfiguration), client =>
			{
				client.BaseAddress = new Uri(apiConfiguration.Address);
				client.DefaultRequestHeaders.Add("X-API-TOKEN", apiConfiguration.Token);
			})
			.RemoveAllLoggers()
			.AddTypedClient<IApiClient>((httpClient, _) => new ApiClient(apiConfiguration, httpClient));

		return services;
	}

	/// <summary>
	/// Add Api Configuration
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configuration"></param>
	/// <param name="sectionName"></param>
	/// <returns></returns>
	public static IServiceCollection AddApiConfiguration(this IServiceCollection services,
		IConfiguration configuration, string sectionName = null)
		=> services
			.AddApiHttpClient(configuration, sectionName)
			.AddScoped<UpdateApiHandler>()
			.AddScoped<ReceiverApiService>()
			.AddHostedService<PollingApiService>();
}
