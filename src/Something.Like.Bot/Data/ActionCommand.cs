namespace Something.Like.Data;

/// <summary>
/// Action Command
/// </summary>
public class ActionCommand
{
	/// <summary>
	/// Empty command
	/// </summary>
	public static readonly ActionCommand Empty = new();

	/// <summary>
	/// Action
	/// </summary>
	public ActionType Action { get; init; }

	/// <summary>
	/// Currency
	/// </summary>
	public CurrencyType Currency { get; init; }

	/// <summary>
	/// Contract
	/// </summary>
	public string Contract { get; init; }

	/// <summary>
	/// Reference
	/// </summary>
	public string Ref { get; init; }
}
