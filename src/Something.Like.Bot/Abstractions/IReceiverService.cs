using System.Threading;
using System.Threading.Tasks;

namespace Something.Like.Abstractions;

/// <summary>
/// Receiver Service
/// </summary>
public interface IReceiverService
{
	/// <summary>
	/// Receive
	/// </summary>
	/// <param name="stoppingToken"></param>
	/// <returns></returns>
	Task ReceiveAsync(CancellationToken stoppingToken);
}
