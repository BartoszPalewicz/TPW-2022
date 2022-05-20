using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using Model;

namespace ViewModel
{

    public class RelayCommand : ICommand
    {
        private readonly Action handler;
        private bool isEnabled;

        public RelayCommand(Action handler)
        {
            this.handler = handler;
            IsEnabled = true;
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (value != isEnabled)
                {
                    isEnabled = value;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            handler();
        }
    }


    public class BallProps : INotifyPropertyChanged
    {
        private Vector2 pos;
        public float X
        {
            get { return pos.X; }
            set { pos.X = value; OnPropertyChanged(); }
        }
        public float Y
        {
            get { return pos.Y; }
            set { pos.Y = value; OnPropertyChanged(); }
        }
        public int r { get; set; }

        public BallProps(float x, float y)
        {
            X = x;
            Y = y;
            r = 40;

        }
        public BallProps(Vector2 position)
        {
            X = position.X;
            Y = position.Y;
            r = 40;
        }

        public BallProps()
        {
            X = 0;
            Y = 0;
            r = 40;
        }

        public void ChangePosition(Vector2 position)
        {
            this.X = position.X;
            this.Y = position.Y;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }



    public class AsyncObservableCollection<T> : ObservableCollection<T>
    {
        private SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

        public AsyncObservableCollection()
        {
        }

        public AsyncObservableCollection(IEnumerable<T> list)
            : base(list)
        {
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (SynchronizationContext.Current == _synchronizationContext)
            {
                // Execute the CollectionChanged event on the current thread
                RaiseCollectionChanged(e);
            }
            else
            {
                // Raises the CollectionChanged event on the creator thread
                _synchronizationContext.Send(RaiseCollectionChanged, e);
            }
        }

        private void RaiseCollectionChanged(object param)
        {
            // We are in the creator thread, call the base implementation directly
            base.OnCollectionChanged((NotifyCollectionChangedEventArgs)param);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (SynchronizationContext.Current == _synchronizationContext)
            {
                // Execute the PropertyChanged event on the current thread
                RaisePropertyChanged(e);
            }
            else
            {
                // Raises the PropertyChanged event on the creator thread
                _synchronizationContext.Send(RaisePropertyChanged, e);
            }
        }

        private void RaisePropertyChanged(object param)
        {
            // We are in the creator thread, call the base implementation directly
            base.OnPropertyChanged((PropertyChangedEventArgs)param);
        }
    }
    public class ViewModelClass : INotifyPropertyChanged
    {
        private ModelClass model;
        public AsyncObservableCollection<BallProps> Circles { get; set; }

        public int BallsCount
        {
            get { return model.GetBallsCount(); }
            set
            {
                if (value >= 0)
                {
                    model.SetBallNumber(value);
                    OnPropertyChanged();
                }
            }
        }

        public ICommand IncreaseButton { get; }
        public ICommand DecreaseButton { get; }
        public ICommand StartSimulationButton { get; }
        public ICommand StopSimulationButton { get; }

        public ViewModelClass()
        {
            Circles = new AsyncObservableCollection<BallProps>();

            model = new ModelClass();
            BallsCount = 3;

            IncreaseButton = new RelayCommand(() =>
            {
                BallsCount += 1;
            });
            DecreaseButton = new RelayCommand(() =>
            {
                BallsCount -= 1;
            });

            StartSimulationButton = new RelayCommand(() =>
            {
                model.SetBallNumber(BallsCount);

                for (int i = 0; i < BallsCount; i++)
                {
                    Circles.Add(new BallProps());
                }

                model.BallPositionChange += (sender, argv) =>
                {
                    if (Circles.Count > 0)
                        Circles[argv.Id].ChangePosition(argv.Position);
                };
                model.StartSimulation();
            });

            StopSimulationButton = new RelayCommand(() =>
            {
                model.StopSimulation();
                Circles.Clear();
                model.SetBallNumber(BallsCount);

            });
        }

        // Event for View update
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}


