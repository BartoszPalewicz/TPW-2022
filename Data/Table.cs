using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Table
    {
        public double Width { get; }
        public double Height { get; }

        public Table(double w, double h)
        {
            Width = w;
            Height = h;
        }
    }
}
