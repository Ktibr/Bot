using System;
using System.Threading;
using System.Threading.Tasks;

namespace Something.Like.Abstractions;

/// <summary>
/// Abstractions Helper
/// </summary>
public static class AbstractionsHelper
{
	/// <summary>
	/// Safe Delay
	/// </summary>
	/// <param name="delay"></param>
	/// <param name="ctx"></param>
	public static async Task SafeDelayAsync(this TimeSpan delay, CancellationToken ctx)
	{
		try
		{
			await Task.Delay(delay, ctx);
		}
		catch (TaskCanceledException)
		{
			// 
		}
	}
}
