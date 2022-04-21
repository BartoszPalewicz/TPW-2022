using System;
using Data;
using System.Numerics;

namespace Logic
{
    public class BallsLogic
    {
        public BallsCollection Balls { get; set; }
        public Table Table { get; set; }

        public BallsLogic(double w, double h)
        {
            Table = new Table(w, h);
            Balls = new BallsCollection();
        }
        
        public void createBall(double r)
        {
            var rng = new Random();
            var x = ((double)rng.NextDouble() * (Table.Width-(2*r))+r);
            var y = ((double)rng.NextDouble() * (Table.Height - (2 * r)) + r);
        }
        public void Move(Ball b)
        {
            Vector2 featurePosition = b.Position + b.Velocity;
            if (featurePosition.X - b.Radius < 0 && featurePosition.X + b.Radius > Table.Width)
            {
                b.Velocity = b.Velocity * new Vector2(1, -1);
            }

            if (featurePosition.Y - b.Radius < 0 && featurePosition.Y + b.Radius > Table.Height)
            {
                b.Velocity = b.Velocity * new Vector2(-1, 1);
            }

            b.Position = b.Position + b.Velocity;
        }
    }
}
