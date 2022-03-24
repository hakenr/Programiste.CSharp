Test("{[()]}", true);
Test("{(})", false);
Test("{[(])}", false);
Test("{{[[(())]]}}", true);
Test("(){}[]", true);
Test("(]", false);
Test("[(])", false);
Test("[({})](]", false);
Test("", true);
Test("pepa", true);
Test("(pepa)", true);
Test("10 * [123 + 7 (6 + 2)]", true);
Test("10 * [123 + 7 (6 + 2))", false);

void Test(string input, bool expected)
{
	var actual = BracesValidator.IsValid(input);
	Console.WriteLine($"'{input}' - {actual} {(actual == expected ? String.Empty : "TEST FAILED")}");
}

public class BracesValidator
{
	public static bool IsValid(string braces)
	{
		var stack = new Stack<char>();
		foreach (var c in braces)
		{
			if (c == '(' || c == '{' || c == '[')
			{
				stack.Push(c);
			}
			else if (c == ')' || c == '}' || c == ']')
			{
				if (stack.Count == 0)
				{
					return false;
				}
				var top = stack.Pop();
				if (c == ')' && top != '(')
				{
					return false;
				}
				if (c == '}' && top != '{')
				{
					return false;
				}
				if (c == ']' && top != '[')
				{
					return false;
				}
			}
		}
		return stack.Count == 0;
	}
}