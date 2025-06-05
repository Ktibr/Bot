using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Something.Like.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Something.Like.Bot;

/// <summary>
/// Compose Receiver and UpdateHandler implementation
/// </summary>
/// <param name="botClient"></param>
/// <param name="updateBotHandler"></param>
/// <param name="logger"></param>
public class ReceiverBotService(
	ITelegramBotClient botClient,
	UpdateBotHandler updateBotHandler,
	ILogger<ReceiverBotService> logger) : IReceiverService
{
	/// <summary>
	/// Start to service Updates with the provided Update Handler class
	/// </summary>
	public async Task ReceiveAsync(CancellationToken stoppingToken)
	{
		// ToDo: we can inject ReceiverOptions through IOptions container
		var receiverOptions = new ReceiverOptions { DropPendingUpdates = true, AllowedUpdates = [] };

		var me = await botClient.GetMe(stoppingToken);
		logger.LogInformation("Start receiving updates for '{BotName}'", me.Username ?? "My Awesome Bot");

		// Start receiving updates
		await botClient.ReceiveAsync(updateBotHandler, receiverOptions, stoppingToken);
	}
}
