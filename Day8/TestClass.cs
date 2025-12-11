using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Day8
{
	[TestClass]
	public class TestClass
    {
		const string textTest = "162,817,812\n57,618,57\n906,360,560\n592,479,940\n" +
			"352,342,300\n466,668,158\n542,29,236\n431,825,988\n739,650,466\n" +
			"52,470,668\n216,146,977\n819,987,18\n117,168,530\n805,96,715\n346,949,466\n" +
			"970,615,88\n941,993,340\n862,61,35\n984,92,344\n425,690,689";

		List<Position> positions;

		public TestClass() 
		{
			var lines = textTest.Split('\n');

			positions = new List<Position>();

			foreach (string line in lines)
			{
				string[] parts = line.Trim().Split(',');

				Position pos = new Position(
					int.Parse(parts[0]),
					int.Parse(parts[1]),
					int.Parse(parts[2])
				);

				positions.Add(pos);
			}
		}

		[TestMethod]
		public void TestMethod1() 
		{
			Engine engine = new Engine(positions);

			engine.Execute(10);

			Assert.AreEqual(40, engine.ProductOfCircuits);
		}



		[TestMethod]
		public void TestMethod2()
		{
			EngineFast engine = new EngineFast(positions);

			engine.Execute(10);

			Assert.AreEqual(40, engine.ProductOfCircuits);
		}

	}
}
