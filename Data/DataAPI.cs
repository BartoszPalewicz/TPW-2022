using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Data
{
    public abstract class DataAPI
    {
        public abstract void AddBall(Ball b);

        public abstract void RemoveLastBall();
        public abstract Ball GetBall(int i);
        public abstract int GetBallsCount();
        
        public static Ball CreateNewBall(Vector2 p, double r, Vector2 v)
        {
            return new Ball(p, r, v);
        }
        public static Table CreateNewTable(double w, double h)
        {
            return new Table(w, h); 
        }

        public static DataAPI CreateBallCollection()
        {
            return new BallsCollection();
        }

    }
}
