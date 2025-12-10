using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Day7
{

	[Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
	public class TestClass
	{
		private char[][] matrix;

		public TestClass() 
		{
			this.matrix = new char[][]
			{
				".......S.......".ToCharArray(),
				"...............".ToCharArray(),
				".......^.......".ToCharArray(),
				"...............".ToCharArray(),
				"......^.^......".ToCharArray(),
				"...............".ToCharArray(),
				".....^.^.^.....".ToCharArray(),
				"...............".ToCharArray(),
				"....^.^...^....".ToCharArray(),
				"...............".ToCharArray(),
				"...^.^...^.^...".ToCharArray(),
				"...............".ToCharArray(),
				"..^...^.....^..".ToCharArray(),
				"...............".ToCharArray(),
				".^.^.^.^.^...^.".ToCharArray(),
				"...............".ToCharArray()
			};
		}
		[TestMethod]
		public void TestTachyonManifold()
		{


			TachyonManifold manifold = new TachyonManifold(matrix);

			manifold.Execute();

			Assert.AreEqual(21, manifold.NumTotalSplittings);

			Assert.AreEqual(40, manifold.NumTimelines);
		}
	}
}
