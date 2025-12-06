// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

public class MathProblem
{
	private Regex isANumber = new Regex(@"^[0-9]+$", RegexOptions.Compiled);
	private Regex isAValidOperator = new Regex(@"^(\*|\+)$", RegexOptions.Compiled);

	private List<decimal> numbers = new List<decimal>();
	private MathOperator mathOperator;

	public MathProblem(IEnumerable<string> operators)
	{

		bool operatorFound = false;

		foreach (var op in operators)
		{
			if (isANumber.Match(op).Success)
			{
				numbers.Add(decimal.Parse(op));
			}
			else if (isAValidOperator.Match(op).Success)
			{
				if (operatorFound)
				{
					throw new Exception("Multiple operators found.");
				}
				else if (op == "+")
				{
					mathOperator = MathOperator.Addition;
				}
				else if (op == "*")
				{
					mathOperator = MathOperator.Multiplication;
				}
				operatorFound = true;
			}
			else
			{
				throw new Exception($"Invalid operator or number: {op}");
			}
		}


		if (!operatorFound)
		{
			throw new Exception("No operator found.");
		}
	}

	public decimal Solve()
	{
		decimal result = (mathOperator == MathOperator.Addition) ? 0 : 1;

		foreach (var number in numbers)
		{
			if (mathOperator == MathOperator.Addition)
			{
				result += number;
			}
			else if (mathOperator == MathOperator.Multiplication)
			{
				result *= number;
			}
		}

		return result;

	}
}

public enum MathOperator
{
	Addition,
	Multiplication
}