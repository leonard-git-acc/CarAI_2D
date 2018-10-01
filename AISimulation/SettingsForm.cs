using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Simulation
{
    public partial class SettingsForm : Form
    {
        public SettingsForm(MainForm _form)
        {
            InitializeComponent();
            InitializeCustomComponent();
            form = _form;

            previewCar = new Car(new Point(preview_panel.Width / 2, preview_panel.Height / 2), new byte[1][][], new Engine());

            LoadConfig();
            LoadValues();

            simBmp = new Bitmap(Image.FromFile(SimPath));
            carBmp = new Bitmap(Image.FromFile(Application.StartupPath + @"\resources\cars\car0.png"));
            
            CropSimImg();
        }

        public string SimPath;
        public string OverPath;

        public int CarRotation;
        public int CarWidth;
        public int CarLength;

        public Point SpawnLocation;
        public Point TargetLocation;

        private Car previewCar;
        private Bitmap simBmp;
        private Bitmap spawnSnippet;
        private Bitmap carBmp;

        private MainForm form;

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            
        }

        private void LoadConfig()
        {
            string[] cfg = File.ReadAllLines(@"cfg\config.cfg");

            SimPath = cfg[0];
            OverPath = cfg[1];

            CarRotation = Convert.ToInt32(cfg[2]);
            CarWidth = Convert.ToInt32(cfg[3]);
            CarLength = Convert.ToInt32(cfg[4]);

            SpawnLocation = new Point(Convert.ToInt32(cfg[5]), Convert.ToInt32(cfg[6]));
            TargetLocation = new Point(Convert.ToInt32(cfg[7]), Convert.ToInt32(cfg[8]));
        }

        private string[] SaveConfig()
        {
            string[] cfg = new string[]
            {
                SimPath,
                OverPath,
                CarRotation.ToString(),
                CarWidth.ToString(),
                CarLength.ToString(),
                SpawnLocation.X.ToString(),
                SpawnLocation.Y.ToString(),
                TargetLocation.X.ToString(),
                TargetLocation.Y.ToString()
            };

            return cfg;
        }

        private void LoadValues()
        {
            simImgPath_textBox.Text = SimPath;
            overImgPath_textBox.Text = OverPath;

            rotation_trackBar.Value = CarRotation / 5;


            width_textBox.Text = CarWidth.ToString();
            length_textBox.Text = CarLength.ToString();

            spawnLocationX_textBox.Text = SpawnLocation.X.ToString();
            spawnLocationY_textBox.Text = SpawnLocation.Y.ToString();

            targetLocationX_textBox.Text = TargetLocation.X.ToString();
            targetLocationY_textBox.Text = TargetLocation.Y.ToString();

            previewCar.Rotation = CarRotation;
            previewCar.Width = CarWidth;
            previewCar.Length = CarLength;
            previewCar.CentreChanged(previewCar.Centre);
        }

        private void CropSimImg()
        {
            if (spawnSnippet != null)
                spawnSnippet.Dispose();

            spawnSnippet = CropImage(simBmp, new Rectangle(SpawnLocation.X - preview_panel.Width / 2, SpawnLocation.Y - preview_panel.Height / 2, preview_panel.Width, preview_panel.Height));

            Bitmap CropImage(Bitmap source, Rectangle section)
            {
                Bitmap bmp = new Bitmap(section.Width, section.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

                g.Dispose();

                return bmp;
            }
        }

        private void preview_panel_Paint(object sender, PaintEventArgs e)
        {
            PointF lfront = previewCar.LeftFront;
            PointF rfront = previewCar.RightFront;
            PointF rback = previewCar.RightBack;
            PointF lback = previewCar.LeftBack;
            PointF[] points = new PointF[] { rback, rfront, lback };

            e.Graphics.DrawImage(spawnSnippet, 0, 0);
            e.Graphics.DrawImage(carBmp, points);
        }

        private void rotation_trackBar_Scroll(object sender, EventArgs e)
        {
            CarRotation = rotation_trackBar.Value * 5;

            previewCar.Rotation = rotation_trackBar.Value * 5;
            previewCar.CentreChanged(previewCar.Centre);
            preview_panel.Refresh();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            box.Text = box.Text.Replace("+", string.Empty).Replace("-", string.Empty);

            try
            {
                if (box.Name.Contains("width"))
                {
                    int val = Convert.ToInt32(box.Text);
                    CarWidth = val;
                    previewCar.Width = val;
                }
                else if (box.Name.Contains("length"))
                {
                    int val = Convert.ToInt32(box.Text);
                    CarLength = val;
                    previewCar.Length = val;
                }
                else if (box.Name.Contains("spawnLocationX"))
                {
                    int val = Convert.ToInt32(box.Text);
                    SpawnLocation.X = val;
                    CropSimImg();
                }
                else if (box.Name.Contains("spawnLocationY"))
                {
                    int val = Convert.ToInt32(box.Text);
                    SpawnLocation.Y = val;
                    CropSimImg();
                }
                else if (box.Name.Contains("targetLocationX"))
                {
                    int val = Convert.ToInt32(box.Text);
                    TargetLocation.X = val;
                }
                else if (box.Name.Contains("targetLocationY"))
                {
                    int val = Convert.ToInt32(box.Text);
                    TargetLocation.Y = val;
                }

                previewCar.CentreChanged(previewCar.Centre);
                preview_panel.Refresh();
            }
            catch
            {
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox box = sender as TextBox;
            try
            {
                if(e.KeyChar == '+')
                {
                    int val = Convert.ToInt32(box.Text);
                    val += 5;
                    box.Text = val.ToString();
                }

                else if (e.KeyChar == '-')
                {
                    int val = Convert.ToInt32(box.Text);
                    val -= 5;
                    box.Text = val.ToString();
                }
            }
            catch
            { }
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string path = "";

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.InitialDirectory = Application.StartupPath + @"\resources";
            openDialog.Filter = "Images|*.BMP;*.JPG;*.JPEG;*.TIF;*.TIFF;*.PNG";

            DialogResult result = openDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                path = openDialog.FileName;
                bool isValid = imageIsValid(path);

                if (btn.Name.Contains("sim") && isValid)
                {
                    simImgPath_textBox.Text = path;
                    simImgPath_textBox.SelectionStart = simImgPath_textBox.Text.Length - 1;
                    simImgPath_textBox.SelectionLength = 0;

                    overImgPath_textBox.Text = path;
                    overImgPath_textBox.SelectionStart = overImgPath_textBox.Text.Length - 1;
                    overImgPath_textBox.SelectionLength = 0;

                    SimPath = path;
                    OverPath = path;
                    simBmp = new Bitmap(Image.FromFile(SimPath));
                    CropSimImg();
                    preview_panel.Refresh();
                }
                else if (btn.Name.Contains("over"))
                {
                    overImgPath_textBox.Text = path;
                    overImgPath_textBox.SelectionStart = overImgPath_textBox.Text.Length - 1;
                    overImgPath_textBox.SelectionLength = 0;

                    OverPath = path;
                }
                else if (!isValid)
                {
                    MessageBox.Show("The Image is not completely framed by a black frame", "An Error has occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            bool imageIsValid(string imgPath)
            {
                Bitmap bmp = new Bitmap(imgPath);

                for (int y = 0; y < bmp.Height; y++)
                {
                    if (bmp.GetPixel(0, y).ToArgb() != Color.Black.ToArgb() || bmp.GetPixel(bmp.Width - 1, y).ToArgb() != Color.Black.ToArgb())
                    {
                        return false;
                    }
                }

                for (int x = 0; x < bmp.Width; x++)
                {
                    if (bmp.GetPixel(x, 0).ToArgb() != Color.Black.ToArgb() || bmp.GetPixel(x, bmp.Height - 1).ToArgb() != Color.Black.ToArgb())
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            string[] cfg = SaveConfig();
            File.WriteAllLines(@"cfg\config.cfg", cfg);
            MessageBox.Show("Settings Saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            form.ConfirmSettings();
            this.Hide();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (form.SimulationDisplay == null)
                form.Close();
            form.control_panel.Enabled = true;

            this.Hide();
            e.Cancel = true;
        }
    }
}
