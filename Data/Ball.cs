using System.Numerics;

namespace Data
{
    public class Ball
    {
        public Vector2 Position { get; set; }
        public double Radius { get; set; }
        public Vector2 Velocity { get; set; }

        public Ball(Vector2 p, double r, Vector2 v)
        {
            Position = p;
            Radius = 40;
            Velocity = v;
        }
    }
}
