using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Something.Like.Api;

namespace Something.Like;

/// <summary>
/// API Update Handler
/// </summary>
[PublicAPI]
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
	Task HandleErrorAsync(Exception exception, CancellationToken cancellationToken);
}
