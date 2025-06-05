using System.Threading;
using System.Threading.Tasks;

namespace Something.Like.Api;

/// <inheritdoc />
public class UpdateApiHandler : IApiUpdateHandler
{
	/// <inheritdoc />
	public async Task HandleUpdateAsync(ApiUpdate update, CancellationToken cancellationToken)
	{
	}

	/// <inheritdoc />
	public async Task HandleErrorAsync(ApiException exception, CancellationToken cancellationToken)
	{
	}
}
