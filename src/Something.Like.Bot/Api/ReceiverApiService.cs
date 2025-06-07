using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Something.Like.Abstractions;

namespace Something.Like.Api;

/// <summary>
/// Compose Receiver and UpdateHandler implementation
/// </summary>
/// <param name="apiClient"></param>
/// <param name="updateHandler"></param>
/// <param name="logger"></param>
public class ReceiverApiService(
	IApiClient apiClient,
	UpdateApiHandler updateHandler,
	ILogger<ReceiverApiService> logger) : IReceiverService
{
	/// <inheritdoc />
	public async Task ReceiveAsync(CancellationToken stoppingToken)
	{
		// ToDo: we can inject ReceiverOptions through IOptions container
		var receiverOptions = new ApiReceiverOptions();

		logger.LogInformation("Start receiving updates for API");

		await apiClient.ReceiveAsync(updateHandler, receiverOptions, stoppingToken);
	}
}
