using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation
{
    public class DrawPad : UserControl
    {
        public enum ToolType
        {
            PathTool,
            TargetTool,
            Eraser
        }

        public ToolType Tool;
        public Bitmap ParkourBitmap;

        private Graphics imageGraphics;
        private Size imageSize;
        private int lineWidth = 40;
        private Point mouseLocation;

        private List<Bitmap> undoBmps;
        private List<Bitmap> redoBmps;

        private MainForm mainForm;
        private Button confirm_button;
        private Button cancel_button;
        private Button undo_button;
        private Button redo_button;
        private Button reset_button;
        private Button tools_button;

        private Color drawColor;

        public DrawPad(MainForm form)
        {
            mainForm = form;

            Reset();

            Tool = ToolType.PathTool;
            drawColor = Color.White;

            undoBmps = new List<Bitmap>();
            redoBmps = new List<Bitmap>();

            this.DoubleBuffered = true;
            this.Paint += DrawFormPaint;
            this.Resize += OnResize;
            this.MouseDown += OnMouseDown;
            this.MouseMove += OnMouseDraw;
            this.MouseUp += OnMouseUp;

            confirm_button = new Button();
            confirm_button.Size = new Size(60, 45);
            confirm_button.Location = new Point(10, 10);
            confirm_button.Text = "Drawing Complete";
            confirm_button.BackColor = Color.ForestGreen;
            confirm_button.Click += confirm_button_Click;
            this.Controls.Add(confirm_button);

            cancel_button = new Button();
            cancel_button.Size = new Size(60, 45);
            cancel_button.Location = new Point(10, 65);
            cancel_button.Text = "Cancel Drawing";
            cancel_button.BackColor = Color.DarkRed;
            cancel_button.Click += cancel_button_Click; ;
            this.Controls.Add(cancel_button);

            undo_button = new Button();
            undo_button.Size = new Size(60, 23);
            undo_button.Location = new Point(80, 10);
            undo_button.Text = "Undo";
            undo_button.BackColor = Color.White;
            undo_button.Click += undo_button_Click;
            this.Controls.Add(undo_button);

            redo_button = new Button();
            redo_button.Size = new Size(60, 23);
            redo_button.Location = new Point(150, 10);
            redo_button.Text = "Redo";
            redo_button.BackColor = Color.White;
            redo_button.Click += redo_button_Click;
            this.Controls.Add(redo_button);

            reset_button = new Button();
            reset_button.Size = new Size(60, 23);
            reset_button.Location = new Point(220, 10);
            reset_button.Text = "New";
            reset_button.BackColor = Color.White;
            reset_button.Click += reset_button_Click;
            this.Controls.Add(reset_button);

            tools_button = new Button();
            tools_button.Size = new Size(120, 23);
            tools_button.Location = new Point(290, 10);
            tools_button.Text = "Tool: Path";
            tools_button.BackColor = Color.White;
            tools_button.Click += tools_button_Click;
            this.Controls.Add(tools_button);
        }



        public void Reset()
        {
            if (ParkourBitmap != null)
            {
                ParkourBitmap.Dispose();
                foreach (var bmp in undoBmps)
                {
                    bmp.Dispose();
                }

                foreach (var bmp in redoBmps)
                {
                    bmp.Dispose();
                }

                undoBmps = new List<Bitmap>();
                redoBmps = new List<Bitmap>();
            }

            ParkourBitmap = new Bitmap(1920, 1080);
            Graphics g = Graphics.FromImage(ParkourBitmap);
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, this.ParkourBitmap.Width, this.ParkourBitmap.Height);
        }

        private void DrawFormPaint(object sender, PaintEventArgs e)
        {
            imageSize = new Size(this.Width, this.Width * ParkourBitmap.Height / ParkourBitmap.Width);
            e.Graphics.DrawImage(ParkourBitmap, new Rectangle(0, 0, imageSize.Width, imageSize.Height));

            Point imgPoint = ConvertPoint(mouseLocation);
            int width = lineWidth * this.Width / ParkourBitmap.Width;
            Color draw = Color.FromArgb(255, Math.Abs(drawColor.R - 55), Math.Abs(drawColor.G - 55), Math.Abs(drawColor.B - 55));
            DrawCircle(e.Graphics, mouseLocation.X, mouseLocation.Y, width, draw);
        }

        private void OnResize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        Point? prevPoint = null;
        private void OnMouseDraw(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.imageGraphics = Graphics.FromImage(ParkourBitmap);
                Point imgPoint = ConvertPoint(e.Location);

                if (prevPoint != null)
                    imageGraphics.DrawLine(new Pen(new SolidBrush(drawColor), lineWidth * 2), (Point)prevPoint, imgPoint);
                FillCircle(imageGraphics, imgPoint.X, imgPoint.Y, lineWidth, drawColor);

                prevPoint = imgPoint;
            }

            mouseLocation = e.Location;
            this.Refresh();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            prevPoint = null;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                undoBmps.Add(ParkourBitmap.Clone() as Bitmap);
                if (undoBmps.Count > 5)
                {
                    undoBmps[0].Dispose();
                    undoBmps.RemoveAt(0);
                }

                this.imageGraphics = Graphics.FromImage(ParkourBitmap);
                Point imgPoint = ConvertPoint(e.Location);
                FillCircle(imageGraphics, imgPoint.X, imgPoint.Y, lineWidth, drawColor);

                prevPoint = imgPoint;
                this.Refresh();
            }
        }

        private Point ConvertPoint(Point pt)
        {
            int x = Math.Max(Math.Min(Convert.ToInt32((float)pt.X * ParkourBitmap.Width / this.imageSize.Width), ParkourBitmap.Width - lineWidth - 11), lineWidth + 10);
            int y = Math.Max(Math.Min(Convert.ToInt32((float)pt.Y * ParkourBitmap.Height / this.imageSize.Height), ParkourBitmap.Height - lineWidth - 11), lineWidth + 10);

            return new Point(x, y);
        }

        private void DrawCircle(Graphics imgGraphics, int x, int y, int radius, Color color)
        {
            imgGraphics.DrawEllipse(new Pen(color, 2), new Rectangle(x - radius, y - radius, radius * 2, radius * 2));
        }

        private void FillCircle(Graphics imgGraphics, int x, int y, int radius, Color color)
        {
            imgGraphics.FillEllipse(new SolidBrush(color), new Rectangle(x - radius, y - radius, radius * 2, radius * 2));
        }

        private void confirm_button_Click(object sender, EventArgs e)
        {
            mainForm.Settings.DrawingComplete();
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            mainForm.TopMost = false;
            mainForm.Settings.TopMost = true;
        }

        private void redo_button_Click(object sender, EventArgs e)
        {
            if (redoBmps.Count > 0)
            {
                int index = redoBmps.Count - 1;

                undoBmps.Add(ParkourBitmap);
                if (undoBmps.Count > 5)
                {
                    undoBmps[0].Dispose();
                    undoBmps.RemoveAt(0);
                }

                ParkourBitmap = redoBmps[index];
                redoBmps.RemoveAt(index);
                this.Refresh();
            }
        }

        private void undo_button_Click(object sender, EventArgs e)
        {
            if (undoBmps.Count > 0)
            {
                int index = undoBmps.Count - 1;

                redoBmps.Add(ParkourBitmap);
                if (redoBmps.Count > 5)
                {
                    redoBmps[0].Dispose();
                    redoBmps.RemoveAt(0);
                }

                ParkourBitmap = undoBmps[index];
                undoBmps.RemoveAt(index);

                this.Refresh();
            }
        }

        private void reset_button_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void tools_button_Click(object sender, EventArgs e)
        {
            int tool = (int)Tool;
            tool++;
            if (tool < 3)
            {
                Tool = (ToolType)(tool);
            }
            else
            {
                Tool = (ToolType)(0);
            }

            switch (Tool)
            {
                case ToolType.PathTool:
                    tools_button.Text = "Tool: Path";
                    drawColor = Color.White;
                    break;
                case ToolType.TargetTool:
                    tools_button.Text = "Tool: Target";
                    drawColor = Color.Red;
                    break;
                case ToolType.Eraser:
                    tools_button.Text = "Tool: Eraser";
                    drawColor = Color.Black;
                    break;
            }
        }
    }
}
