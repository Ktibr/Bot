using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Something.Like.Api;

/// <inheritdoc />
public class ApiClient(ApiConfiguration apiConfiguration, HttpClient httpClient) : IApiClient
{
	/// <inheritdoc />
	public async Task ReceiveAsync(IApiUpdateHandler updateHandler, ApiReceiverOptions options = null,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(updateHandler);

		// TODO
		await Task.Delay(30000, cancellationToken);
	}
}
