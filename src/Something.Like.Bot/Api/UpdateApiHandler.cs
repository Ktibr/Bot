using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace Something.Like.Api;

/// <inheritdoc />
public class UpdateApiHandler(ITelegramBotClient botClient, ILogger<UpdateApiHandler> logger) : IApiUpdateHandler
{
	/// <inheritdoc />
	public async Task HandleUpdateAsync(ApiUpdate update, CancellationToken cancellationToken)
	{
	}

	/// <inheritdoc />
	public async Task HandleErrorAsync(Exception exception, CancellationToken cancellationToken)
	{
	}
}
