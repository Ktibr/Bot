using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Something.Like.Data;

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

	/// <summary>
	/// Convert string from Base64
	/// </summary>
	/// <param name="base64String"></param>
	/// <returns></returns>
	public static string FromBase64(this string base64String)
	{
		if (string.IsNullOrEmpty(base64String))
			return string.Empty;

		try
		{
			byte[] bytes = Convert.FromBase64String(base64String);
			return Encoding.Default.GetString(bytes);
		}
		catch
		{
			return string.Empty;
		}
	}

	/// <summary>
	/// Get the Description from the DescriptionAttribute.
	/// </summary>
	/// <param name="enumValue"></param>
	/// <returns></returns>
	private static string GetDescription(this Enum enumValue)
		=> enumValue.GetType()
			.GetMember(enumValue.ToString())
			.First()
			.GetCustomAttribute<DescriptionAttribute>()?
			.Description ?? string.Empty;

	/// <summary>
	/// Parse Command
	/// </summary>
	/// <param name="command"></param>
	/// <returns></returns>
	public static ActionCommand ParseCommand(this string command)
	{
		if (string.IsNullOrEmpty(command))
			return null;

		try
		{
			var items = command.Split('|');
			if (items.Length < 3)
				return ActionCommand.Empty;

			return new ActionCommand
			{
				Action = items[0].TryParseEnum<ActionType>(out var actionType) ? actionType : ActionType.None,
				Currency = items[1].TryParseEnum<CurrencyType>(out var currencyType) ? currencyType : CurrencyType.None,
				Contract = items[2],
				Ref = items.Length > 3 ? items[3] : null
			};
		}
		catch
		{
			return ActionCommand.Empty;
		}
	}

	/// <summary>
	/// Try to convert string to enum 
	/// </summary>
	/// <param name="value"></param>
	/// <param name="result"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	private static bool TryParseEnum<T>(this string value, out T result) where T : struct
		=> Enum.TryParse(value, true, out result);

	/// <summary>
	/// Convert To Prompt String
	/// </summary>
	/// <param name="command"></param>
	/// <returns></returns>
	public static string ToPromptString(this ActionCommand command)
	{
		if (command == null)
			return "Unknown action";

		return new HtmlMessage()
			.Text("You're going to : ").BoldText(command.Action.GetDescription()).NewLine()
			.Text("       Currency : ").BoldText(command.Currency.GetDescription()).NewLine()
			.Text("       Contract : ").BoldText(command.Contract).NewLine()
			.Text("            Ref : ").BoldText(command.Ref).NewLine()
			.Text("       Quantity : ")
			.ToString();
	}
}
