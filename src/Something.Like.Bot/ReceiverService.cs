using Microsoft.Extensions.Logging;
using Something.Like.Abstractions;
using Telegram.Bot;

namespace Something.Like.Bot;

/// <summary>
/// Compose Receiver and UpdateHandler implementation
/// </summary>
/// <param name="botClient"></param>
/// <param name="updateHandler"></param>
/// <param name="logger"></param>
public class ReceiverService(
	ITelegramBotClient botClient,
	UpdateHandler updateHandler,
	ILogger<ReceiverServiceBase<UpdateHandler>> logger)
	: ReceiverServiceBase<UpdateHandler>(botClient, updateHandler, logger);
