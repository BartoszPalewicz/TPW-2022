using System;
using System.Collections.Generic;
using System.Text;
using Data;

namespace Logic
{
    public abstract class LogicAPI
    {
        //public void Move();
        public event EventHandler<Ball> PositionChangedEvent;

        public void InvokePositionChangedEvent(Ball b)
        {
            PositionChangedEvent?.Invoke(this, b);
        }
    }
}
