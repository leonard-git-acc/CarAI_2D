using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    static class ParkourImage
    {
        public static byte[][][] ParseImage(Bitmap image) //Parses image into a byte array
        {
            byte[][][] pixelArray = new byte[image.Height][][];

            for (int yaxis = 0; yaxis < image.Height; yaxis++) // which row
            {
                byte[][] pixelRow = new byte[image.Width][];
                for (int xaxis = 0; xaxis < image.Width; xaxis++)// which pixel
                {
                    byte[] pixel = new byte[4];
                    Color pixelColor = image.GetPixel(xaxis, yaxis);
                    pixel[0] = pixelColor.A;
                    pixel[1] = pixelColor.R;
                    pixel[2] = pixelColor.G;
                    pixel[3] = pixelColor.B;
                    pixelRow[xaxis] = pixel;
                }
                pixelArray[yaxis] = pixelRow;
            }

            return pixelArray;
        }
    }

    public class GridUnit
    {
        public int Value = -1; // Distance to the target
        public Point Location; // Location in the given image
        public Point GridLocation; // Location in the grid
        public int UnitSize;
        public byte[][][] UnitBitmap; // Pixels that are contained by this unit
        public int AccessibilityPercentage; // % of black pixels

        public GridUnit(Point location, Point gridLocation, int unitSize)
        {
            Location = location;
            GridLocation = gridLocation;
            UnitSize = unitSize;
        }

        public void GetUnit(byte[][][] bitmap) // get accessibility percentage and pixels
        {
            byte[][][] unitBitmap = new byte[UnitSize][][];
            int blackPixelCount = 0;

            for (int iter = 0; iter < UnitSize; iter++)
            {
                byte[][] unitRow = new byte[UnitSize][];
                for (int i = 0; i < UnitSize; i++)
                {
                    byte[] black = new byte[] { 255, 0, 0, 0 };
                    byte[] pixel = bitmap[Location.Y + iter][Location.X + i];
                    unitRow[i] = pixel;
                    if (CompareColor(black, pixel))
                    {
                        blackPixelCount++;
                    }
                }
                unitBitmap[iter] = unitRow;
            }

            UnitBitmap = unitBitmap;
            AccessibilityPercentage = 100 - (100 * blackPixelCount / (UnitSize * UnitSize));
        }

        private bool CompareColor(byte[] c1, byte[] c2)
        {
            return c1[0] == c2[0] && c1[1] == c2[1] && c1[2] == c2[2] && c1[3] == c2[3];
        }

        public static GridUnit[][] CreateGrid(byte[][][] bitmap, int unitSize) // Creates a grid of a given image
        {
            int width = bitmap[0].Length / unitSize;
            int height = bitmap.Length / unitSize;

            GridUnit[][] grid = new GridUnit[height][];

            for (int iter = 0; iter < height; iter++)
            {
                GridUnit[] gridRow = new GridUnit[width];

                for (int i = 0; i < width; i++)
                {
                    GridUnit unit = new GridUnit(new Point(i * unitSize, iter * unitSize), new Point(i, iter), unitSize);
                    unit.GetUnit(bitmap);
                    gridRow[i] = unit;
                }
                grid[iter] = gridRow;
            }

            return grid;
        }

        public static void FindPath(Point startingLocation, GridUnit[][] grid, int unitSize) // get value for each grid that lies in path
        {
            GridUnit startUnit = GetByLocation(startingLocation, grid, unitSize); // starting unit

            List<GridUnit> selectedUnits = new List<GridUnit>() { startUnit }; // curently active units
            List<GridUnit> neighbourUnits = new List<GridUnit>(); // next units

            bool active = true;
            int index = 0;

            while (active)
            {
                for (int i = 0; i < selectedUnits.Count; i++)
                {
                    selectedUnits[i].Value = index;
                    GridUnit[] neighbours = GetNeighbours(selectedUnits[i], grid);
                    foreach (GridUnit unit in neighbours)
                    {
                        if (unit.Value == -1 && unit.AccessibilityPercentage >= 85)
                        {
                            unit.Value = -2; // selected units get id -2 to eliminate reselection
                            neighbourUnits.Add(unit);
                        }
                    }
                }

                if (neighbourUnits.Count > 0) // if no possible units are found
                {
                    selectedUnits = new List<GridUnit>(neighbourUnits);
                    neighbourUnits = new List<GridUnit>();
                    index++;
                }
                else
                {
                    active = false;
                }
            }
        }

        public static GridUnit[] GetNeighbours(GridUnit unit, GridUnit[][] grid)
        {
            GridUnit[] neighbours = new GridUnit[4];
            int gridx = unit.GridLocation.X;
            int gridy = unit.GridLocation.Y;

            neighbours[0] = grid[gridy][gridx + 1];
            neighbours[1] = grid[gridy][gridx - 1];
            neighbours[2] = grid[gridy + 1][gridx];
            neighbours[3] = grid[gridy - 1][gridx];

            return neighbours;
        }

        public static GridUnit GetByLocation(Point location, GridUnit[][] grid, int unitSize)
        {
            int x = (int)Math.Floor((double)(location.X / unitSize));
            int y = (int)Math.Floor((double)(location.Y / unitSize));
            x = Math.Min(grid[0].Length - 1, Math.Max(0, x));
            y = Math.Min(grid.Length - 1, Math.Max(0, y));

            return grid[y][x];
        }
    }
}
