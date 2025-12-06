using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Day6
{

	[TestClass]
	public class TestClass
    {
		private string input;

		public TestClass()
		{
			input = "123 328  51 64 \n 45 64  387 23 \n  6 98  215 314\n*   +   *   +  ";
		}



		[TestMethod]
        public void TestCephalopodMathProblem()
        {

			HumanMathProblemBuilder builder = new HumanMathProblemBuilder(input);
			var mathProblem = builder.Build();

			decimal finalResult = mathProblem.Select(e => e.Solve()).Sum();

			Assert.AreEqual(4277556, finalResult);


		}


		[TestMethod]
		public void TestHumanMathProblem()
		{
			CephalopodMathProblemBuilder cephalopodMathProblemBuilder = new CephalopodMathProblemBuilder(input);

			var a = cephalopodMathProblemBuilder.Build().Select(e => e.Solve());

			decimal cephalopodFinalResult = a.Sum();

			Assert.AreEqual(3263827, cephalopodFinalResult);
		}


	}
}

