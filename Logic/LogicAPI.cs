using System;
using System.Collections.Generic;
using System.Text;
using Data;

namespace Logic
{
    public abstract class LogicAPI
    {
        //public void Move();
        public event EventHandler<MovableBall>? PositionChangedEvent;

        

        public abstract void addBalls(int numberOfBalls);

        public abstract void removeBalls(int numberOfBalls);

        public abstract Ball getBall(int index);

        public abstract void startSimulation();

        public abstract void stopSimulation();
        public abstract int getBallsCount();

        protected virtual void onPositionChange(MovableBall b)
        {
            PositionChangedEvent?.Invoke(this, b);
        }


    }
}
