using System;
using System.Collections.Generic;
using System.Text;
using Data;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class MovableBall
    {
        private Ball ball;
        public BallsCollection Balls { get; set; }
        public int id;
        private BallsLogic owner;
        public event EventHandler<MovableBall>? PositionChange;

        public double boundryX;
        public double boundryY;
        public MovableBall(Ball b, int id, BallsLogic owner, double boundryX, double boundryY, BallsCollection Balls)
        {
            this.ball = b;
            this.id = id;
            this.owner = owner;
            this.boundryX = boundryX;
            this.boundryY = boundryY;
            this.Balls = Balls;

            

        }

        public Ball GetBall()
        {
            return ball;
        }

        public async void Move()
        {
            while (!owner.CancelSimulationSource.Token.IsCancellationRequested)
            {
                Vector2 featurePosition = this.ball.Position + this.ball.Velocity;

                if (featurePosition.X  < 0 || featurePosition.X + this.ball.Radius > this.boundryX)
                {
                    this.ball.Velocity = this.ball.Velocity * new Vector2(-1, 1);
                }

                if (featurePosition.Y  < 0 || featurePosition.Y + this.ball.Radius > this.boundryY)
                {
                    this.ball.Velocity = this.ball.Velocity * new Vector2(1, -1);
                }

                for(int i = 0; i < Balls.GetBallsCount(); i++)
                {
                    Ball otherBall = Balls.GetBall(i);
                    if(Vector2.Distance(featurePosition, otherBall.Position) < (this.ball.Radius/2 + otherBall.Radius/2))
                    {
                        Vector2 p = this.ball.Velocity;
                        this.ball.Velocity = otherBall.Velocity;
                        otherBall.Velocity = p;
                    }
                }

                this.ball.Position = this.ball.Position + this.ball.Velocity;
                PositionChange?.Invoke(this, this);
                await Task.Delay(20, owner.CancelSimulationSource.Token).ContinueWith(_ => { });
            }
        }
    }
}
