using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Something.Like.Abstractions;

/// <summary>An abstract class to compose Polling background service and Receiver implementation classes</summary>
/// <remarks>See more: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services#consuming-a-scoped-service-in-a-background-task</remarks>
/// <typeparam name="TReceiverService">Receiver implementation class</typeparam>
public abstract class PollingServiceBase<TReceiverService>(
	IServiceProvider serviceProvider,
	ILogger<PollingServiceBase<TReceiverService>> logger)
	: BackgroundService where TReceiverService : IReceiverService
{
	/// <inheritdoc />
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		logger.LogInformation("Starting polling service");
		await DoWork(stoppingToken);
	}

	private async Task DoWork(CancellationToken stoppingToken)
	{
		// Make sure we receive updates until Cancellation Requested
		while (!stoppingToken.IsCancellationRequested)
		{
			try
			{
				// Create a new IServiceScope on each iteration. This way we can leverage the benefits
				// of Scoped TReceiverService and typed HttpClient - we'll grab a "fresh" instance each time
				using var scope = serviceProvider.CreateScope();
				var receiver = scope.ServiceProvider.GetRequiredService<TReceiverService>();

				await receiver.ReceiveAsync(stoppingToken);
			}
			catch (Exception ex)
			{
				logger.LogError("Polling failed with exception: {Exception}", ex);

				// Cooldown if something goes wrong
				await TimeSpan.FromSeconds(5).SafeDelayAsync(stoppingToken);
			}
		}
	}
}
