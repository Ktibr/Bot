using System.Threading;
using System.Threading.Tasks;

namespace Something.Like.Api;

/// <summary>
/// API Update Handler
/// </summary>
public interface IApiUpdateHandler
{
	/// <summary>
	/// Handle Update
	/// </summary>
	/// <param name="update"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task HandleUpdateAsync(ApiUpdate update, CancellationToken cancellationToken);

	/// <summary>
	/// Handle Error
	/// </summary>
	/// <param name="exception"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task HandleErrorAsync(ApiException exception, CancellationToken cancellationToken);
}
