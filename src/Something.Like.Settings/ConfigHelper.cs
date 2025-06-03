using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace Something.Like.Settings;

/// <summary>
/// Configuration Helper
/// </summary>
public static class ConfigHelper
{
	/// <summary>
	/// Configure section
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configuration"></param>
	/// <param name="sectionName"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static IServiceCollection ConfigureSection<T>(this IServiceCollection services,
		IConfiguration configuration, string sectionName = null) where T : class
		=> services
			.Configure<T>(configuration
				.GetSection(string.IsNullOrEmpty(sectionName) ? typeof(T).Name : sectionName));

	/// <summary>
	/// Add Bot Configuration
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configuration"></param>
	/// <param name="sectionName"></param>
	/// <returns></returns>
	public static IServiceCollection AddBotConfiguration(this IServiceCollection services,
		IConfiguration configuration, string sectionName = null)
	{
		var token = Environment.GetEnvironmentVariable("TOKEN") ?? string.Empty;
		return string.IsNullOrEmpty(token)
			? services
				.ConfigureSection<BotConfiguration>(configuration, sectionName)
			: services
				.AddSingleton(Options.Create(new BotConfiguration { Token = token }));
	}

	/// <summary>
	/// Add TelegramBot Client
	/// </summary>
	/// <param name="services"></param>
	/// <param name="name"></param>
	/// <returns></returns>
	public static IServiceCollection AddTelegramBotClient(this IServiceCollection services, string name)
	{
		services
			.AddHttpClient(name)
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
}
