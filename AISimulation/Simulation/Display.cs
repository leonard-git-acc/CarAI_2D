using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation
{
    class Display : Control
    {
        public float Zoom { get => zoom; set => DisplayZoomChanged(value); }
        public bool Render { get => render; set => render = value; }
        public bool Details { get => details; set => details = value; }
        public bool Grid { get => grid; set => grid = value; }

        public new Bitmap BackgroundImage { get => backgroundImage; set => backgroundImage = value; }
        public PointF BackgroundImageLocation { get => backgroundImageLocation; set => BackgroundImageLocationChanged(value); }
        public Engine SimulationEngine { get => simulationEngine; set => simulationEngine = value; }


        private float zoom = 1.0F;
        private bool render = true;
        private bool details = false;
        private bool grid = false;


        private Thread displayLoopThread;
        private Bitmap backgroundImage;
        private PointF backgroundImageLocation = new Point(0, 0);
        private Size backgroundImageSize = new Size();
        private Image backgroundImageDef = Image.FromFile(Application.StartupPath + @"\resources\background5.png");
        private Bitmap backgroundImageGrid;
        private Point MouseLocation;

        private Size parkourSize;
        private Engine simulationEngine;

        private Bitmap[][] car_color;
        private int lod = 5;

        private int trackedCar = -1;

        public Display(string backgroundImage, string simulationImage, Point spawn, Point target)
        {
            this.Height = 10;
            this.Width = 10;
            this.DoubleBuffered = true;
            this.backgroundImageDef = Image.FromFile(backgroundImage);
            this.BackgroundImage = new Bitmap(backgroundImageDef);
            this.backgroundImageSize = backgroundImageDef.Size;
            this.LoadCarLoD();

            // Start Engine
            SimulationEngine = new Engine(this, simulationImage, spawn, target);
            parkourSize = SimulationEngine.ParkourBitmap.Size;
            SimulationEngine.UpdateEngine += UpdateSimulation;

            backgroundImageGrid = GridToBitmap(simulationEngine.ParkourGrid);
            displayLoopThread = new Thread(new ThreadStart(DisplayLoop));

            this.Paint += Draw;
            this.LocationChanged += DisplayLocationChanged;
            this.SizeChanged += DisplaySizeChanged;
            this.MouseDown += DisplayMouseDown;
            this.MouseMove += DisplayMouseMove;
            this.MouseWheel += DisplayMouseWheel;
            this.Disposed += DisplayDisposed;
        }

        public PointF ConvertPoint(PointF point)
        {
            float xnew = (point.X * Width / parkourSize.Width) * Zoom;
            float ynew = point.Y * xnew / point.X;

            return new PointF(xnew + backgroundImageLocation.X, ynew + backgroundImageLocation.Y);
        }

        private void DisplayLoop()
        {
            while (true)
            {
                if (Render)
                {
                    this.Invoke(new MethodInvoker(delegate { this.Refresh(); }));
                    Thread.Sleep(25);
                }
                else
                {
                    this.Invoke(new MethodInvoker(delegate { this.Refresh(); }));
                    Thread.Sleep(250);
                }
            }
        }

        private void LoadCarLoD()
        {
            int lodPerc = 22;
            car_color = new Bitmap[4][];

            for (int iter = 0; iter < 4; iter++)
            {
                car_color[iter] = new Bitmap[lod];
                Bitmap startImg = new Bitmap(Image.FromFile(Application.StartupPath + @"\resources\cars\car" + iter + ".png"));
                int width = startImg.Width;
                int height = startImg.Height;
                int percWidth = width / 100 * lodPerc;
                int percHeight = percWidth * height / width;

                for (int i = 0; i < car_color[iter].Length; i++)
                {
                    car_color[iter][i] = new Bitmap(startImg, width, height);
                    width -= percWidth;
                    height -= percHeight;
                }
                startImg.Dispose();
            }
        }

        private void DisplaySizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImageRescale();
            this.Refresh();
        }

        private void DisplayZoomChanged(float _zoom)
        {
            zoom = _zoom;

            //downscale if image is smaller than its actual size
            this.BackgroundImageRescale();
            this.Refresh();
        }

        private void DisplayLocationChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void DisplayMouseDown(object sender, MouseEventArgs e)
        {
            MouseLocation = e.Location;

            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < simulationEngine.Cars.Length; i++)
                {
                    PointF centre = ConvertPoint(simulationEngine.Cars[i].Centre);
                    
                    float a = 10 * Zoom;

                    if(MouseLocation.X > centre.X - a && MouseLocation.X < centre.X + a &&
                       MouseLocation.Y > centre.Y - a && MouseLocation.Y < centre.Y + a)
                    {
                        trackedCar = i;
                    }
                }
            }
        }

        private void DisplayMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                BackgroundImageLocation = new PointF(BackgroundImageLocation.X + (e.Location.X - MouseLocation.X), BackgroundImageLocation.Y + (e.Location.Y - MouseLocation.Y));
                MouseLocation = e.Location;
            }
        }

        private void DisplayMouseWheel(object sender, MouseEventArgs e)
        {
            MouseLocation = e.Location;
            if (e.Delta > 0)
            {
                if (Zoom < 10)
                    Zoom = Zoom + (Zoom / 10);
            }
            else
            {
                if (Zoom > 0.05)
                    Zoom = Zoom - (Zoom / 10);
            }

        }

        private void DisplayDisposed(object sender, EventArgs e)
        {
            StopSimulation();
        }

        private void BackgroundImageRescale()
        {
            //Downscale picture to minimize size by zooming out
            if (backgroundImageSize.Width <= backgroundImageDef.Width)
            {
                int bwidth = backgroundImageSize.Width; // (int)(Width * Zoom);
                int bheight = backgroundImageSize.Height; // (int)(backgroundImageDef.Height * Width / backgroundImageDef.Width * Zoom);
                this.backgroundImage?.Dispose();
                this.BackgroundImage = new Bitmap(backgroundImageDef, new Size(Math.Max(bwidth, 10), Math.Max(bheight, 10)));
            }
            else
            {
                this.BackgroundImage = new Bitmap(backgroundImageDef);
            }
        }

        private void BackgroundImageLocationChanged(PointF bLocation)
        {
            backgroundImageLocation = bLocation;
            this.Refresh();
        }

        public void StartSimulation()
        {
            SimulationEngine.LoopThread.Start();
            displayLoopThread.Start();
        }

        public void StopSimulation()
        {
            SimulationEngine.StopEngine();
            displayLoopThread.Abort();
        }

        private void UpdateSimulation(object sender, EventArgs e)
        {
            //this.Refresh();
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 1);
            Font font = new Font(new FontFamily("Arial"), 10F);
            SolidBrush brush = new SolidBrush(Color.LimeGreen);

            if (Render)
            {
                //Draw and scale background
                int bwidth = (int)(Width * Zoom);
                int bheight = (int)(backgroundImageDef.Height * Width / backgroundImageDef.Width * Zoom);
                backgroundImageSize.Width = bwidth;
                backgroundImageSize.Height = bheight;

                TrackCar(trackedCar, simulationEngine.Cars);

                e.Graphics.DrawImage(BackgroundImage, BackgroundImageLocation.X, BackgroundImageLocation.Y, bwidth, bheight);

                //Draw Cars
                DrawGrid(e, grid);
                DrawCars(e);
                DrawDetails(e, details);

                Rectangle border = new Rectangle(0, 0, Width - 1, Height - 1);
                e.Graphics.DrawRectangle(pen, border);
            }
            if(details)
                e.Graphics.DrawString(SimulationEngine.LoopsPerSecond.ToString(), font, brush, 10.0F, 10.0F);
        }

        private void DrawCars(PaintEventArgs e)
        {
            try
            {
                foreach (Car car in SimulationEngine.Cars)
                {
                    PointF lfront = ConvertPoint(car.LeftFront);
                    PointF rfront = ConvertPoint(car.RightFront);
                    PointF rback = ConvertPoint(car.RightBack);
                    PointF lback = ConvertPoint(car.LeftBack);

                    int lodLvl = Math.Min((int)(4.0F / zoom), 4);
                    Bitmap bmp = car_color[car.Color][lodLvl];
                    PointF[] points = new PointF[] { rback, rfront, lback };

                    if (details)
                    {
                        foreach (Eye eye in car.Eyes)
                        {
                            PointF loc = ConvertPoint(eye.Location[eye.Location.Length - 1]);
                            e.Graphics.DrawLine(new Pen(Color.Red), loc, ConvertPoint(car.Centre));
                            if (Grid)
                                foreach (PointF pt in eye.Location)
                                {
                                    PointF ptC = ConvertPoint(pt);
                                    e.Graphics.FillEllipse(new SolidBrush(Color.Red), ptC.X - 1, ptC.Y - 1, 3, 3);
                                }
                        }
                        e.Graphics.DrawPolygon(new Pen(Color.LimeGreen), new PointF[] { rback, rfront, lfront, lback});
                        e.Graphics.DrawString(lodLvl.ToString(), new Font(new FontFamily("Arial"), 20F), new SolidBrush(Color.Red), Width - 30, Height - 30);
                    }

                    if(lodLvl >= 1)
                        e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                    e.Graphics.DrawImage(bmp, points);
                }
            }
            catch
            {
            }
        }

        private void DrawGrid(PaintEventArgs e, bool active)
        {
            if (active)
            {
                int bwidth = (int)(Width * Zoom);
                int bheight = (int)(backgroundImageDef.Height * Width / backgroundImageDef.Width * Zoom);

                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                e.Graphics.DrawImage(backgroundImageGrid, BackgroundImageLocation.X, BackgroundImageLocation.Y, bwidth, bheight);
            }
        }

        private void DrawDetails(PaintEventArgs e, bool active) // needs to be rewritten
        {
            if (active)
            {
                int detailsX = Width * 3 / 4;
                int detailsY = 0;
                int detailsWidth = Width / 4;
                int detailsHeight = Height / 4;

                Rectangle rec = new Rectangle(detailsX, detailsY, detailsWidth, detailsHeight);
                Pen pen = new Pen(Color.Black);
                SolidBrush brush = new SolidBrush(Color.Gray);
                e.Graphics.FillRectangle(brush, rec);
                e.Graphics.DrawRectangle(pen, rec);

                if (SimulationEngine.CarRanking != null || trackedCar > -1)
                {
                    Neuron[][] neuronLayers;
                    if (trackedCar > -1)
                    {
                        neuronLayers = SimulationEngine.Cars[trackedCar].Brain.AllLayers;
                    }
                    else
                    {
                        neuronLayers = SimulationEngine.CarRanking[SimulationEngine.CarRanking.Length - 1].Brain.AllLayers;
                    }

                    int neuronDistanceX = detailsWidth / 10;
                    int neuronDistanceY = 5;
                    int neuronSize = detailsHeight / neuronLayers[1].Length - 10;

                    for (int iteration = 0; iteration < neuronLayers.Length; iteration++)
                    {
                        for (int iter = 0; iter < neuronLayers[iteration].Length; iter++)
                        {
                            for (int i = 0; i < neuronLayers[iteration][iter].Weight.Length; i++)
                            {
                                if (neuronLayers[iteration][iter].Weight[i] != 0)
                                {
                                    Point p1 = new Point((detailsX + neuronDistanceX - detailsWidth / neuronLayers.Length) + neuronSize / 2, detailsHeight / neuronLayers[1].Length * i + neuronSize / 2);
                                    Point p2 = new Point(detailsX + neuronDistanceX + neuronSize / 2, detailsY + neuronDistanceY + neuronSize / 2);
                                    if (neuronLayers[iteration][iter].Weight[i] > 0)
                                        e.Graphics.DrawLine(new Pen(Color.White), p1, p2);
                                    if (neuronLayers[iteration][iter].Weight[i] < 0)
                                        e.Graphics.DrawLine(new Pen(Color.Black), p1, p2);
                                }
                            }

                            neuronDistanceY = detailsHeight / neuronLayers[1].Length * (iter + 1);
                        }
                        neuronDistanceX += detailsWidth / neuronLayers.Length;
                        neuronDistanceY = 5;
                    }

                    neuronDistanceX = detailsWidth / 10;
                    neuronDistanceY = 5;
                    neuronSize = detailsHeight / neuronLayers[1].Length - 10;

                    for (int iteration = 0; iteration < neuronLayers.Length; iteration++)
                    {
                        for (int iter = 0; iter < neuronLayers[iteration].Length; iter++)
                        {
                            Rectangle ellipse = new Rectangle(detailsX + neuronDistanceX, detailsY + neuronDistanceY, neuronSize, neuronSize);
                            SolidBrush ellbrush;
                            if (iteration == 0)
                            {
                                ellbrush = new SolidBrush(Color.Red);
                            }
                            else if (iteration == neuronLayers.Length - 1)
                            {
                                ellbrush = new SolidBrush(Color.Green);
                            }
                            else
                            {
                                ellbrush = new SolidBrush(Color.Yellow);
                            }
                            e.Graphics.FillEllipse(ellbrush, ellipse);

                            neuronDistanceY = detailsHeight / neuronLayers[1].Length * (iter + 1);
                        }
                        neuronDistanceX += detailsWidth / neuronLayers.Length;
                        neuronDistanceY = 5;
                    }
                }
            }

        }

        private void TrackCar(int id, Car[] cars)
        {
            if (trackedCar > -1)
            {
                PointF centre = cars[id].Centre;
                float xnew = (centre.X * Width / parkourSize.Width) * Zoom;
                float ynew = centre.Y * xnew / centre.X;
                PointF newCentre = new PointF(xnew * -1 + Width / 2, ynew * -1 + Height / 2);

                backgroundImageLocation = newCentre;
            }
        }

        private Bitmap GridToBitmap(GridUnit[][] grid)
        {
            Bitmap bitmap = new Bitmap(grid[0].Length, grid.Length);
            bitmap.MakeTransparent(bitmap.GetPixel(0, 0));
            int maxVal = GridUnit.GetByLocation(SimulationEngine.SpawnLocation, SimulationEngine.ParkourGrid, SimulationEngine.GridUnitSize).Value;

            foreach (GridUnit[] unitRow in grid)
            {
                foreach (GridUnit unit in unitRow)
                {
                    if (unit.Value >= 0)
                    {
                        int alpha = 200;
                        int red = Math.Max(0, 255 - unit.Value / (maxVal / 200 + 1));
                        int green = Math.Max(0, 255 - unit.Value / (maxVal / 200 + 1));
                        int blue = Math.Min(255, 0 + unit.Value / (maxVal / 200 + 1));

                        for (int iter = 0; iter < simulationEngine.GridUnitSize; iter++)
                        {
                            for (int i = 0; i < simulationEngine.GridUnitSize; i++)
                            {
                                bitmap.SetPixel(unit.GridLocation.X, unit.GridLocation.Y, Color.FromArgb(alpha, red, green, blue));
                            }
                        }
                    }
                }
            }

            bitmap.Save(Application.StartupPath + "/resources/grid.png", System.Drawing.Imaging.ImageFormat.Png);

            return bitmap;
        }


    }
}
