using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Something.Like.Api;

namespace Something.Like;

/// <summary>
/// Api Client
/// </summary>
[PublicAPI]
public interface IApiClient
{
	/// <summary>
	/// Receive a message
	/// </summary>
	/// <param name="updateHandler"></param>
	/// <param name="options"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task ReceiveAsync(IApiUpdateHandler updateHandler, ApiReceiverOptions options = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Send a message
	/// </summary>
	/// <param name="message"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<ApiResponse> SendAsync(ApiMessage message, CancellationToken cancellationToken = default);
}
