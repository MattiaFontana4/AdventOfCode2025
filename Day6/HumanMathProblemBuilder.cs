// See https://aka.ms/new-console-template for more information

public class HumanMathProblemBuilder : MathProblemBuilder
{
	public HumanMathProblemBuilder(string text) : base(text)
	{

	}

    public override IEnumerable<MathProblem> Build()
    {
		var matrix = GetMatrix(true);

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

			problems.Add(new MathProblem(column));
		}

		return problems;
	}
}