using System;
using Data;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
namespace Logic
{
    public class BallsLogic : LogicAPI
    {
        public BallsCollection Balls { get; set; }
        public Table Table { get; set; }

        public CancellationTokenSource CancelSimulationSource { get; private set; }

        private bool _started = false;

        public BallsLogic(double w, double h)
        {
            Table = new Table(w, h);
            Balls = new BallsCollection();
            CancelSimulationSource = new CancellationTokenSource();
        }

        protected override void onPositionChange(MovableBall args)
        {
            base.onPositionChange(args);
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

        public override void addBalls(int numberOfBalls)
        {
            var rng = new Random();
            for (int i = 0; i < numberOfBalls; i++)
            {
                
                var r = (rng.NextDouble() * 20) + 20;
                var x = (rng.NextDouble() * (Table.Width - (2 * r)) + r);
                var y = (rng.NextDouble() * (Table.Height - (2 * r)) + r);
                var vx = (rng.NextDouble() - 0.5) * 20;
                var vy = (rng.NextDouble() - 0.5) * 20;
                this.Balls.AddBall(new Ball(new Vector2((float)x, (float)y), r, new Vector2((float)vx, (float)vy)));
            }

        }

        public override void removeBalls(int numberOfBalls)
        {
            for(int i = 0; i < numberOfBalls; i++)
            {
                Balls.RemoveLastBall();
            }
        }

        public override int getBallsCount()
        {
            return Balls.GetBallsCount();
        }

        public override void startSimulation()
        {
            if (CancelSimulationSource.IsCancellationRequested) return;

            CancelSimulationSource = new CancellationTokenSource();

            for (var i = 0; i < Balls.GetBallsCount(); i++)
            {
                var ball = new MovableBall(Balls.GetBall(i), i, this, Table.Width, Table.Height);
                ball.PositionChange += (_, args) => onPositionChange(ball);
                Task.Factory.StartNew(ball.Move, CancelSimulationSource.Token);
            }

        }



        public override void stopSimulation()
        {
            this.CancelSimulationSource.Cancel();
        }

        public override Ball getBall(int index)
        {
            return Balls.GetBall(index);
        }
    }
}
