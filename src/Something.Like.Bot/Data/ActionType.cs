using System.ComponentModel;

namespace Something.Like.Data;

/// <summary>
/// Action Type
/// </summary>
public enum ActionType
{
	/// <summary>
	/// Unknown action
	/// </summary>
	[Description("Unknown action")] None = 0,

	/// <summary>
	/// Buy
	/// </summary>
	[Description("Buy")] Buy = 1,

	/// <summary>
	/// Sell
	/// </summary>
	[Description("Sell")] Sell = 2
}
