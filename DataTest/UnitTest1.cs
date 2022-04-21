using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.Numerics;

namespace DataTest
{
    [TestClass]
    public class UnitTest1
    {
        private BallsCollection bc;
        private Ball b1, b2, b3;
        [TestMethod]
        public void BallsCollectionTest()
        {
            bc = new BallsCollection();
            b1 = new Ball(new Vector2(1, 2), 1, new Vector2(0, 0));
            b2 = new Ball(new Vector2(11, 7), 1, new Vector2(0, 0));
            b3 = new Ball(new Vector2(3, 5), 1, new Vector2(0, 0));


            Assert.AreEqual(bc.GetBallsCount(), 0);
            bc.AddBall(b1);
            Assert.AreEqual(bc.GetBallsCount(), 1);
            bc.AddBall(b2);
            Assert.AreEqual(bc.GetBallsCount(), 2);
            bc.AddBall(b3);
            Assert.AreEqual(bc.GetBallsCount(), 3);

            Assert.AreEqual(b1, bc.GetBall(0));
            Assert.AreEqual(b2, bc.GetBall(1));
            Assert.AreEqual(b3, bc.GetBall(2));
        }
    }
}