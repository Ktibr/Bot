using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Something.Like.Api;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Something.Like.Telegram;

/// <inheritdoc />
public class UpdateBotHandler(IApiClient apiClient, ILogger<UpdateBotHandler> logger) : IUpdateHandler
{
	/// <inheritdoc />
	public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source,
		CancellationToken cancellationToken)
	{
		logger.LogInformation("HandleError: {Exception}", exception);

		// Cooldown in case of network connection error
		if (exception is RequestException)
			await TimeSpan.FromSeconds(2).SafeDelayAsync(cancellationToken);
	}

	/// <inheritdoc />
	public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken ctx)
	{
		ctx.ThrowIfCancellationRequested();

		await (update switch
		{
			{ Message: { } message } => OnMessage(botClient, message, ctx),
			{ EditedMessage: { } message } => OnMessage(botClient, message, ctx),
			_ => UnknownUpdateHandlerAsync(update)
		});
	}

	private async Task OnMessage(ITelegramBotClient botClient, Message msg, CancellationToken ctx)
	{
		logger.LogInformation("Receive message type: {MessageType}", msg.Type);

		if (msg.Text is not { } messageText)
			return;

		Message sentMessage = await (messageText.Split(' ')[0] switch
		{
			"/api_test" => ApiTest(botClient, msg, ctx),
			_ => Usage(botClient, msg)
		});

		logger.LogInformation("The message was sent with id: {SentMessageId}", sentMessage.Id);
	}

	private async Task<Message> ApiTest(ITelegramBotClient botClient, Message msg, CancellationToken ctx)
	{
		var response = await apiClient.SendAsync(new ApiMessage(), ctx);

		var botMessage = await botClient.SendMessage(msg.Chat, response.Message, parseMode: ParseMode.Html,
			replyMarkup: new ReplyKeyboardRemove(), cancellationToken: ctx);

		return botMessage;
	}

	private static Task<Message> Usage(ITelegramBotClient botClient, Message msg)
	{
		const string usage = """
		                         <b><u>Bot menu</u></b>:
		                         /api_test          - test api message
		                     """;
		return botClient.SendMessage(msg.Chat, usage, parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
	}

	private Task UnknownUpdateHandlerAsync(Update update)
	{
		logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
		return Task.CompletedTask;
	}
}
