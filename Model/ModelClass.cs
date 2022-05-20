using System;
using Logic;
using System.Numerics;

namespace Model
{
    public class OnPositionChangeUiAdapterEventArgs : EventArgs
    {
        public readonly Vector2 Position;
        public readonly int Id;

        public OnPositionChangeUiAdapterEventArgs(Vector2 position, int id)
        {
            this.Position = position;
            Id = id;
        }
    }

    public class ModelClass
    {
        public double boardWidth;
        public double boardHeight;
        public int ballsNumber;
        public LogicAPI? logic;
        public event EventHandler<OnPositionChangeUiAdapterEventArgs>? BallPositionChange;


        public ModelClass()
        {
            boardWidth = 650;
            boardHeight = 400;
            logic = new BallsLogic(boardWidth, boardHeight);
            ballsNumber = 0;

            logic.PositionChangedEvent += (sender, b) =>
            {
                BallPositionChange?.Invoke(this, new OnPositionChangeUiAdapterEventArgs(b.GetBall().Position, b.id));
            };
        }
        public void StartSimulation()
        {
            logic.addBalls(ballsNumber);
            logic.startSimulation();
        }

        public void StopSimulation()
        {
            logic.stopSimulation();
            logic = new BallsLogic(boardWidth, boardHeight);
            logic.PositionChangedEvent += (sender, b) =>
            {
                BallPositionChange?.Invoke(this, new OnPositionChangeUiAdapterEventArgs(b.GetBall().Position, b.id));
            };
        }

        public void SetBallNumber(int amount)
        {
            ballsNumber = amount;
        }

        public int GetBallsCount()
        {
            return ballsNumber;
        }

        public void OnBallPositionChange(OnPositionChangeUiAdapterEventArgs args)
        {
            BallPositionChange?.Invoke(this, args);
        }
    }
}
