// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

internal class MathProblemBuilder
{
    private string text;

	Regex regex = new Regex(@"\s+", RegexOptions.Compiled);


	public MathProblemBuilder(string text)
    {
        this.text = text;
    }

    public IEnumerable<MathProblem> Build()
    {
        string[] lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length < 2)
        {
            throw new Exception("Input text does not contain enough lines.");
        }

        string[][] matrix = lines.Select(line => regex.Replace(line," "))
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();

        List<MathProblem> problems = new List<MathProblem>();


        int dim0 = matrix.GetLength(0);
        int dim1 = matrix[0].GetLength(0);


		for (int i = 0; i < dim1; i++)
        {
            string[] column = new string[dim0];

			for (int j = 0; j < dim0; j++)
            {
                column[j] = matrix[j][i];
			}

            problems.Add( new MathProblem(column));
		}

		return problems;
	}
}