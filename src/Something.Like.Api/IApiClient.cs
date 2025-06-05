using System.Threading;
using System.Threading.Tasks;

namespace Something.Like.Api;

/// <summary>
/// Api Client
/// </summary>
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
}
