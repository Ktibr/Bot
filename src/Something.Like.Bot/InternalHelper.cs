using System;
using System.Threading;
using System.Threading.Tasks;

namespace Something.Like;

/// <summary>
/// Internal Helper
/// </summary>
public static class InternalHelper
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
