using System.Collections.Generic;
using System.Linq;

namespace CherwellTriangles.Models
{
    public class TriangleContainer
    {
        public int ContainerWidth { get; set; }
        public int ContainerHeight { get; set; }
        public int SegmentWidth { get; set; }
        public int SegmentHeight { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public List<Triangle> Triangles { get; set; }
        public int TriangleCount {
            get
            {
                if (Triangles != null)
                {
                    return Triangles.Count();
                }
                return 0;
            }
        }

        public TriangleContainer () { }

        public TriangleContainer (int containerWidth, int containerHeight, int segmentWidth, int segmentHeight)
        {
            ContainerWidth = containerWidth;
            ContainerHeight = containerHeight;
            SegmentWidth = segmentWidth;
            SegmentHeight = segmentHeight;
            Triangles = new List<Triangle>();
        }

        public void CreateTriangles()
        {
            // Ensure Container width and height can hold even number of triangle segments
            if (ContainerWidth % SegmentWidth != 0)
            {
                while (ContainerWidth % SegmentWidth != 0)
                {
                    ContainerWidth++;
                }
            }
            if (ContainerHeight % SegmentHeight != 0)
            {
                while (ContainerHeight % SegmentHeight != 0)
                {
                    ContainerHeight++;
                }
            }
            
            var maxRows = ContainerHeight / SegmentHeight;
            var maxColumns = ContainerWidth / SegmentWidth;

            int i = 0;
            int j = 0;
            
            // Rows are built from top to bottom, Columns are built from left to right
            for (i = 0; i < maxRows; i++)
            {
                // Since each box can contain 2 triangles, specify coordinates for the box.  First the Y Coordinates
                var upperLeftY = (i * SegmentHeight);
                var upperRightY = upperLeftY;
                var bottomLeftY = upperLeftY + SegmentHeight;
                var bottomRightY = bottomLeftY;
                var lastColumn = 0;
                for (j = 0; j < maxColumns; j++)
                {
                    // Next the X Coordinates; Then use the box coordinates to build the Triangles
                    var upperLeftX = (j * SegmentWidth);                    
                    var upperRightX = upperLeftX + SegmentWidth;                    
                    var bottomLeftX = upperLeftX;                    
                    var bottomRightX = bottomLeftX + SegmentWidth;

                    // Right Angle at Bottom
                    var bottomTriangle = new Triangle
                    {
                        X1Y1 = new Coordinate { X = bottomLeftX, Y = bottomLeftY },
                        X2Y2 = new Coordinate { X = upperLeftX, Y = upperLeftY },
                        X3Y3 = new Coordinate { X = bottomRightX, Y = bottomRightY },
                        RowNum = (i + 1),
                        ColumnNum = (lastColumn + 1)
                    };
                    Triangles.Add(bottomTriangle);

                    // Right Angle at Top
                    var topTriangle = new Triangle
                    {
                        X1Y1 = new Coordinate { X = upperRightX, Y = upperRightY },
                        X2Y2 = new Coordinate { X = bottomRightX, Y = bottomRightY },
                        X3Y3 = new Coordinate { X = upperLeftX, Y = upperLeftY },
                        RowNum = (i + 1),
                        ColumnNum = (lastColumn + 2)
                    };
                    Triangles.Add(topTriangle);
                    lastColumn = (lastColumn + 2);
                }
            }
        }

        public string LookupTriangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            var coordinates = string.Format("(V1x: {0}, V1y: {1}), (V2x: {2}, V2y: {3}), (V3x: {4}, V3y: {5})", x1, y1, x2, y2, x3, y3);
            var message = string.Format("Triangle could not be found based on provided coordinates [{0}].", coordinates);
            
            var triangle = Triangles.FirstOrDefault(item => (item.X1Y1.X == x1 && item.X1Y1.Y == y1) &&
                (item.X2Y2.X == x2 && item.X2Y2.Y == y2) && (item.X3Y3.X == x3 && item.X3Y3.Y == y3));

            //var triangle2 = Triangles.FirstOrDefault(item =>
            //    ((item.X1Y1.X == x1 && item.X1Y1.Y == y1) || (item.X1Y1.X == x2 && item.X1Y1.Y == y2) || (item.X1Y1.X == x3 && item.X1Y1.Y == y3)) &&
            //    ((item.X2Y2.X == x1 && item.X2Y2.Y == y1) || (item.X2Y2.X == x2 && item.X2Y2.Y == y2) || (item.X2Y2.X == x3 && item.X2Y2.Y == y3)) &&
            //    ((item.X3Y3.X == x1 && item.X3Y3.Y == y1) || (item.X3Y3.X == x2 && item.X3Y3.Y == y2) || (item.X3Y3.X == x3 && item.X3Y3.Y == y3)));

            var label = "";
            if (triangle != null)
            {
                label = triangle.Label;
                message = string.Format("Triangle found in Grid {0} using coordinates [{1}]", label, coordinates);
            }

            return "{\"label\": \"" + label + "\", \"message\": \"" + message + "\"}";
        }
    }
}