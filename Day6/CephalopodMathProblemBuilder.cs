// See https://aka.ms/new-console-template for more information


internal class CephalopodMathProblemBuilder : MathProblemBuilder
{
    public CephalopodMathProblemBuilder(string text) : base(text) { }

    public override IEnumerable<MathProblem> Build()
    {
        var charMatrix = base.SplitLines().Select(e => e.ToArray());

        char[][] swappedCharMatrix = new char[charMatrix.First().Length][];

        for (int i = 0; i < swappedCharMatrix.Length; i++)
        {
            swappedCharMatrix[i] = new char[charMatrix.Count()];
            for (int j = 0; j < charMatrix.Count(); j++)
            {
                swappedCharMatrix[i][j] = charMatrix.ElementAt(j)[i];
            }
		}

		string[] strings = swappedCharMatrix
            .Select(e => new string(e))
            .ToArray();


        var result = BuildMathProblems(strings);

        return result;
    }

    private List<MathProblem> BuildMathProblems(string[] strings)
    {
		var result = new List<MathProblem>();
        
        List<string> operators = new List<string>();
        bool isOperatorFinded = false;

		for (int i = 0; i < strings.Length; i++)
        {
            
            if (string.IsNullOrWhiteSpace(strings[i])) 
            { 
                result.Add(new MathProblem(operators));
                operators.Clear();
                isOperatorFinded = false ;
			}
            else
            {
                string part = strings[i].Trim();
                if (strings[i].Contains('*') || strings[i].Contains('+'))
                {
					if (isOperatorFinded)
					{
						throw new Exception("Multiple operators found in the same part.");
					}

					if (strings[i].Contains('*'))
                    {
                        string op = "*";
                        operators.Add(op);
                        part = part.Replace("*", "").Trim();
                        isOperatorFinded = true;
                    }
                    else if (strings[i].Contains('+'))
                    {
                        string op = "+";
						operators.Add(op);
						part = part.Replace("+", "").Trim();
                        isOperatorFinded = true;
                    }

                    operators.Add(part);

				}
                else
                {
                    operators.Add(strings[i].Trim());
                }
			}
		}


		result.Add(new MathProblem(operators));

		return result;
	}
}