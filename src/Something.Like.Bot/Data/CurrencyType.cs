using System.ComponentModel;

namespace Something.Like.Data;

/// <summary>
/// Currency
/// </summary>
public enum CurrencyType
{
	/// <summary>
	/// Unknown currency
	/// </summary>
	[Description("Unknown currency")] None = 0,

	/// <summary>
	/// Solana
	/// </summary>
	[Description("Solana")] Solana = 1,

	/// <summary>
	/// Ethereum
	/// </summary>
	[Description("Ethereum")] Ethereum = 2
}
