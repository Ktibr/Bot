using System;
using Microsoft.Extensions.Logging;
using Something.Like.Abstractions;

namespace Something.Like.Api;

/// <summary>
/// Compose Polling and ReceiverService implementations
/// </summary>
/// <param name="serviceProvider"></param>
/// <param name="logger"></param>
public class PollingApiService(IServiceProvider serviceProvider, ILogger<PollingApiService> logger)
	: PollingServiceBase<ReceiverApiService>(serviceProvider, logger);
