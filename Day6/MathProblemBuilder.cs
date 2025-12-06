// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

public abstract class MathProblemBuilder
{
    protected string text;

	protected Regex regex = new Regex(@"\s+", RegexOptions.Compiled);

	protected MathProblemBuilder(string text)
    {
        this.text = text;
    }

    protected string[] SplitLines()
    {
        return text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
	}

    protected bool checkLineCount(string[] lines)
    {
        return lines.Length >= 2;
    }

    protected string[][] GetMatrix(bool replace)     
    {
        string[] lines = SplitLines();
        if (!checkLineCount(lines))
        {
            throw new Exception("Input text does not contain enough lines.");
        }

        var eLines = lines.AsEnumerable();

        if (replace)
        {
            eLines = eLines.Select(line => regex.Replace(line, " "));
		}

		string[][] matrix = eLines.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();

		return matrix;
	}

    public abstract IEnumerable<MathProblem> Build();

}