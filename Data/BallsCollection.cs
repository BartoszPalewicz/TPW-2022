using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class BallsCollection
    {
        private List<Ball> balls;

        public BallsCollection()
        {
            balls = new List<Ball>();
        }

        public int BallsCount()
        {
            return balls.Count;
        }
        public void AddBall(Ball ball)
        {
            balls.Add(ball);
        }

        public Ball GetBall(int index)
        {
            return balls[index];
        }
    }
}
