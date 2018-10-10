using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation
{
    public class SettingsControl : UserControl
    {
        public Point SelectedSpawn { get; set; }
        public Point SelectedTarget { get; set; }

        public new Bitmap BackgroundImage { get => backgroundImage; set => BackgroundImageChanged(value); }
        private Bitmap backgroundImage;
        private Size backgroundImageSize;

        public bool Active { get; set; }

        public enum SelectType
        {
            None,
            Spawn,
            Target
        }
        public SelectType SelectableType { get; set; }

        private Bitmap spawnIcon;
        private Bitmap targetIcon;
        private Bitmap crosshairGreen;
        private Bitmap crosshairRed;
        private Size crosshairSize = new Size(100, 100);

        private Point mouseLocation;
        private bool isSelectable = true;

        private MainForm mainForm;

        public SettingsControl(MainForm _mainForm)
        {
            this.Height = 10;
            this.Width = 10;
            this.DoubleBuffered = true;
            this.Visible = false;
            this.Active = false;
            this.SelectableType = SelectType.None;

            mainForm = _mainForm;

            LoadBitmaps();

            this.Paint += Draw;
            this.LocationChanged += ControlLocationChanged;
            this.SizeChanged += ControlSizeChanged;
            this.MouseClick += ControlMouseClick;
            this.MouseMove += ControlMouseMove;
            this.MouseWheel += ControlMouseWheel;
            this.KeyPress += ControlKeyPress;
            this.Disposed += ControlDisposed;
        }

        private void LoadBitmaps()
        {
            spawnIcon = new Bitmap(100, 20);
            targetIcon = new Bitmap(100, 20);

            SolidBrush brush1 = new SolidBrush(Color.Black);
            SolidBrush brush2 = new SolidBrush(Color.LightGray);
            Font font = new Font("Arial", 15.0F);

            using (Graphics g = Graphics.FromImage(spawnIcon))
            {
                g.DrawString("START", font, brush1, 2, 2);
                g.DrawString("START", font, brush2, 0, 0);
            }

            using (Graphics g = Graphics.FromImage(targetIcon))
            {
                g.DrawString("FINISH", font, brush1, 1, 1);
                g.DrawString("FINISH", font, brush2, 0, 0);
            }

            using (Bitmap bmp = new Bitmap(@"resources/icons/crosshair.png"))
            {
                crosshairGreen = replaceColor(bmp, Color.ForestGreen, Color.White);
                crosshairRed = replaceColor(bmp, Color.Red, Color.White);
            }

            Bitmap replaceColor(Bitmap bmp, Color newCol, Color oldCol)
            {
                Bitmap b = new Bitmap(bmp);
                using (Graphics g = Graphics.FromImage(b))
                {
                    ColorMap[] colorMap = new ColorMap[1];
                    colorMap[0] = new ColorMap();
                    colorMap[0].OldColor = oldCol;
                    colorMap[0].NewColor = newCol;
                    ImageAttributes attr = new ImageAttributes();
                    attr.SetRemapTable(colorMap);
                    Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                    g.DrawImage(bmp, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attr);
                }

                return b;
            }
        }

        private Point ConvertPoint(Point pt)
        {
            int xnew = Convert.ToInt32((float)pt.X / backgroundImageSize.Width * backgroundImage.Width) ;
            int ynew = Convert.ToInt32((float)pt.Y / backgroundImageSize.Height * backgroundImage.Height) ;
            return new Point(xnew, ynew);
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            if(backgroundImage != null && Active)
            {
                backgroundImageSize = new Size(this.Width, this.Width * backgroundImage.Height / backgroundImage.Width);
                e.Graphics.DrawImage(backgroundImage, new Rectangle(new Point(0, 0), backgroundImageSize));

                if (mouseLocation.X < backgroundImageSize.Width && mouseLocation.Y < backgroundImageSize.Height)
                {
                    Size size = new Size(Convert.ToInt32(crosshairSize.Width * ((float)backgroundImageSize.Width / backgroundImage.Width)), Convert.ToInt32(crosshairSize.Height * ((float)backgroundImageSize.Height / backgroundImage.Height)));
                    Rectangle crossrec = new Rectangle(mouseLocation.X - size.Width / 2, mouseLocation.Y - size.Height / 2, size.Width, size.Height);
                    Bitmap crosshair;

                    if(isSelectable)
                    {
                        crosshair = crosshairGreen;
                    }
                    else
                    {
                        crosshair = crosshairRed;
                    }
                    
                    e.Graphics.DrawImage(crosshair, crossrec);

                    if (this.SelectableType == SelectType.Spawn)
                    {
                        e.Graphics.DrawImage(spawnIcon, mouseLocation.X, mouseLocation.Y - spawnIcon.Height);
                    }
                    else if (this.SelectableType == SelectType.Target)
                    {
                        e.Graphics.DrawImage(targetIcon, mouseLocation.X, mouseLocation.Y - targetIcon.Height);
                    }
                }
            }
        }

        private new void BackgroundImageChanged(Bitmap img)
        {
            backgroundImage = img;
            this.Refresh();
        }

        private void ControlDisposed(object sender, EventArgs e)
        {
            base.Dispose();
            backgroundImage.Dispose();
        }

        private void ControlKeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void ControlMouseWheel(object sender, MouseEventArgs e)
        {
            
        }

        private void ControlMouseMove(object sender, MouseEventArgs e)
        {
            mouseLocation = e.Location;
            isSelectable = mouseLocation.X < backgroundImageSize.Width && mouseLocation.Y < backgroundImageSize.Height;
            if (isSelectable)
            {
                Point pt = ConvertPoint(mouseLocation);
                Color pixel = backgroundImage.GetPixel(pt.X, pt.Y);
                isSelectable = pixel.ToArgb() != Color.Black.ToArgb();
            }

            this.Refresh();
        }

        private void ControlMouseClick(object sender, MouseEventArgs e)
        {
            if (isSelectable && SelectableType == SelectType.Spawn)
            {
                this.SelectedSpawn = ConvertPoint(e.Location);
                this.Visible = false;
                this.Active = false;
                mainForm.Settings.SelectionComplete();
            }
            else if (isSelectable && SelectableType == SelectType.Target)
            {
                this.SelectedTarget = ConvertPoint(e.Location);
                this.Visible = false;
                this.Active = false;
                mainForm.Settings.SelectionComplete();
            }
        }

        private void ControlSizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void ControlLocationChanged(object sender, EventArgs e)
        {
            
        }


    }
}
