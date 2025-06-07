using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Something.Like.Configuration;

namespace Something.Like.Api;

/// <inheritdoc />
public class ApiClient(ApiConfiguration apiConfiguration, HttpClient httpClient) : IApiClient
{
	private static readonly ApiUpdate[] EmptyApiUpdates = [];

	/// <inheritdoc />
	public async Task ReceiveAsync(IApiUpdateHandler updateHandler, ApiReceiverOptions options = null,
		CancellationToken ctx = default)
	{
		ArgumentNullException.ThrowIfNull(updateHandler);

		while (!ctx.IsCancellationRequested)
		{
			var updates = EmptyApiUpdates;
			try
			{
				// TODO : call API

				await TimeSpan.FromSeconds(10).SafeDelayAsync(ctx);
			}
			catch (OperationCanceledException)
			{
				return;
			}
			catch (Exception exception)
			{
				try
				{
					await updateHandler.HandleErrorAsync(exception, ctx);
				}
				catch (OperationCanceledException)
				{
					return;
				}
			}

			foreach (var update in updates)
			{
				try
				{
					await updateHandler.HandleUpdateAsync(update, ctx);
				}
				catch (OperationCanceledException)
				{
					return;
				}
				catch (Exception ex)
				{
					try
					{
						await updateHandler.HandleErrorAsync(ex, ctx)
							.ConfigureAwait(false);
					}
					catch (OperationCanceledException)
					{
						// ignored
					}
				}
			}
		}
	}

	/// <inheritdoc />
	public Task<ApiResponse> SendAsync(ApiMessage message, CancellationToken cancellationToken = default)
		=> Task.FromResult(new ApiResponse() { Message = "<b><u>API Message</u></b>" });
}
