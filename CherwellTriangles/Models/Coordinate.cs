using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CherwellTriangles.Models
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Label
        {
            get
            {
                return string.Format("(X: {0}, Y: {1})", X, Y);
            }
        }
    }
}