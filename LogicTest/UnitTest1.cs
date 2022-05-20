using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Data;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace LogicTest
{

	
	[TestClass]
    public class LogicTests
    {
        [TestMethod]
        public void EventTest()
        {
			var interactionCount = 0;
			LogicAPI bl = new BallsLogic(200, 200);
			bl.addBalls(10);
			Assert.AreEqual(10, bl.getBallsCount());

			var startPositionList = new List<Vector2>();
			for (int i = 0; i < bl.getBallsCount(); i++)
			{
				startPositionList.Add(bl.getBall(i).Position);
			}

			bl.PositionChangedEvent += (_, _) =>
			{
				interactionCount++;
				Console.WriteLine("xd");
				if (interactionCount >= 50)
				{
					bl.stopSimulation();
				}
			};
			bl.startSimulation();
			while (interactionCount < 50)
			{ }
			Assert.IsTrue(interactionCount >= 49);
			for (int i = 0; i < bl.getBallsCount(); i++)
			{
				if (startPositionList[i] == bl.getBall(i).Position)
				{
					Assert.Fail();
				}
			}





			}

		[TestMethod]
		public void BallTest()
		{
			LogicAPI bl = new BallsLogic(200, 200);
			bl.addBalls(10);
			Assert.AreEqual(10, bl.getBallsCount());
			bl.removeBalls(2);
			Assert.AreEqual(8, bl.getBallsCount());



		}
	}

}