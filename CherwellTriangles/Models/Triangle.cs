using System;

namespace CherwellTriangles.Models
{
    
    public class Triangle
    {
        public Coordinate X1Y1 { get; set; }
        public Coordinate X2Y2 { get; set; }
        public Coordinate X3Y3 { get; set; }
        public int RowNum { get; set; } 
        public int ColumnNum { get; set; }
        public String Label {
            get
            {
                var label = "";
                switch (RowNum) {
                    case 1:
                        label = string.Format("A{0}", ColumnNum);
                        break;
                    case 2:
                        label = string.Format("B{0}", ColumnNum);
                        break;
                    case 3:
                        label = string.Format("C{0}", ColumnNum);
                        break;
                    case 4:
                        label = string.Format("D{0}", ColumnNum);
                        break;
                    case 5:
                        label = string.Format("E{0}", ColumnNum);
                        break;
                    case 6:
                        label = string.Format("F{0}", ColumnNum);
                        break;
                }    
                return label;

            }
        }

        public Triangle() {
            X1Y1 = new Coordinate { X = 0, Y = 0 };
            X2Y2 = new Coordinate { X = 0, Y = 0 };
            X3Y3 = new Coordinate { X = 0, Y = 0 };
        }

        public Triangle(int x1, int x2, int x3, int y1, int y2, int y3, int row, int column)
        {
            X1Y1 = new Coordinate { X = x1, Y = y1 };
            X2Y2 = new Coordinate { X = x2, Y = y2 };
            X3Y3 = new Coordinate { X = x3, Y = y3 };
            RowNum = row;
            ColumnNum = column;
        }
        
    }
}