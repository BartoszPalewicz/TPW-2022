using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class BallsCollection : DataAPI
    {
        private List<Ball> balls;

        public BallsCollection()
        {
            balls = new List<Ball>();
        }

        public override int GetBallsCount()
        {
            return balls.Count;
        }
        public override void AddBall(Ball ball)
        {
            balls.Add(ball);
        }

        public override Ball GetBall(int index)
        {
            return balls[index];
        }
    }
}
