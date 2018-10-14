using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    public class Eye
    {
        public PointF[] Location { get => location; set => location = value; }
        public PointF Centre { get => centre; set => CentreChanged(value); }
        public float MaxDistance { get => maxDistance; set => maxDistance = value; }
        public float MinDistance { get => minDistance; set => minDistance = value; }
        public float ReceptorDistance { get => receptorDistance; set => receptorDistance = value; }
        public float Rotation { get => rotation; set => rotation = value; }

        private byte[][][] bitmap;
        private Engine engine;
        private PointF[] location;
        private PointF centre;
        private float rotation;
        private float maxDistance;
        private float minDistance;
        private float receptorDistance;

        public Eye(PointF _centre, float _maxDistance, float _minDistance, int receptorCount, float _rotation, byte[][][] _bitmap, Engine _engine)
        {
            centre = _centre;
            maxDistance = _maxDistance;
            minDistance = _minDistance;
            receptorDistance = (maxDistance - minDistance) / receptorCount; 
            rotation = _rotation;
            bitmap = _bitmap;
            engine = _engine;
            location = new PointF[receptorCount];

            for (int i = 0; i < receptorCount; i++)
            {
                location[i] = CalcRotation(centre.X + minDistance + receptorDistance * i, centre.Y);
            }
        }

        public float CheckGround()
        {
            byte[] black = new byte[] { 255, 0, 0, 0 };
            byte[] red = new byte[] { 255, 255, 0, 0 };
            float eyeValue = 0;

            for(int i = location.Length - 1; i >= 0; i--)
            {
                PointF loc = location[i];

                int x = Math.Min(bitmap[0].Length - 1, Math.Max(0, Convert.ToInt32(loc.X)));
                int y = Math.Min(bitmap.Length - 1, Math.Max(0, Convert.ToInt32(loc.Y)));
                byte[] pixel = bitmap[y][x];
                float val = 1.0F / location.Length;

                if (compareColor(pixel, black))
                {
                    eyeValue = val * (location.Length - i);
                }
                else
                {
                    eyeValue += 0.0F;
                }

                bool compareColor(byte[] c1, byte[] c2)
                {
                    return c1[0] == c2[0] && c1[1] == c2[1] && c1[2] == c2[2] && c1[3] == c2[3];
                }
            }

            return eyeValue;
        }

        public float CheckGrid()
        {
            GridUnit[][] grid = engine.ParkourGrid;
            int receptor = location.Length - 1;
            int x = Convert.ToInt32(location[receptor].X);
            int y = Convert.ToInt32(location[receptor].Y);
            GridUnit unit = GridUnit.GetByLocation(new Point(x,y), grid, engine.GridUnitSize);

            return 1.0F - (100.0F * unit.Value / engine.GridMaxValue);
        }

        private void CentreChanged(PointF _centre)
        {
            centre = _centre;
        }

        private PointF CalcRotation(float x, float y)
        {
            double xrot = Math.Cos(Rotation * 0.0174533) * (x - centre.X) - Math.Sin(Rotation * 0.0174533) * (y - centre.Y) + centre.X;
            double yrot = Math.Sin(Rotation * 0.0174533) * (x - centre.X) + Math.Cos(Rotation * 0.0174533) * (y - centre.Y) + centre.Y;

            return new PointF((float)xrot, (float)yrot);
        }
    }
}
