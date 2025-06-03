using System;
using Microsoft.Extensions.Logging;
using Something.Like.Abstractions;

namespace Something.Like.Bot;

/// <summary>
/// Compose Polling and ReceiverService implementations
/// </summary>
/// <param name="serviceProvider"></param>
/// <param name="logger"></param>
public class PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
	: PollingServiceBase<ReceiverService>(serviceProvider, logger);
