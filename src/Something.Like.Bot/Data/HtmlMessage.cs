using System.Text;

namespace Something.Like.Data;

/// <summary>
/// HTML Message
/// </summary>
public class HtmlMessage
{
	private readonly StringBuilder _builder = new();

	/// <summary>
	/// Text
	/// </summary>
	/// <param name="text"></param>
	/// <returns></returns>
	public HtmlMessage Text(string text)
	{
		_builder.Append(text);
		return this;
	}

	/// <summary>
	/// Bold Text
	/// </summary>
	/// <param name="text"></param>
	/// <returns></returns>
	public HtmlMessage BoldText(string text)
	{
		_builder.Append($"<b>{text}</b>");
		return this;
	}

	/// <summary>
	/// New Line
	/// </summary>
	/// <returns></returns>
	public HtmlMessage NewLine()
	{
		_builder.Append('\n');
		return this;
	}

	/// <inheritdoc />
	public override string ToString()
		=> _builder.ToString();
}
