using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simulation;

namespace AISimulation
{
    public partial class MainForm : Form
    {
        Display display;

        public MainForm()
        {
            InitializeComponent();
            object[] cfg = LoadCfg();
            Point spawn = (Point)cfg[2];
            Point target = (Point)cfg[3];

            display = new Display(Application.StartupPath + @"\resources\" + cfg[0].ToString(), Application.StartupPath + @"\resources\" + cfg[1].ToString(), spawn, target);
            display.Location = new Point(10, 10);
            display.Size = new Size(this.Width - 160, this.Height - 60);
            display.BackColor = Color.FromArgb(255, 38, 127, 0);
            this.Controls.Add(display);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            display.StartSimulation();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            display.Width = this.Width - 160;
            display.Height = this.Height - 60;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            display.SimulationEngine.LoopSpeed = (float)trackBar1.Value / 100;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 100;
            display.SimulationEngine.LoopSpeed = trackBar1.Value / 100;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            display.Render = checkBox1.Checked;
        }

        private void grid_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            display.Grid = grid_checkBox.Checked;
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            //string structure = display.SimulationEngine.CarRanking[display.SimulationEngine.CarRanking.Length - 1].Brain.StringifyBrainStructure();
            //string path = @"saves\save.car";
            //File.Create(path).Dispose();
            //File.WriteAllText(path, structure);

            FileStream fstream = new FileStream(@"saves\save.carStream", FileMode.Create);
            MemoryStream stream = display.SimulationEngine.CarRanking[display.SimulationEngine.CarRanking.Length - 1].Brain.BuildStructureStream();
            stream.WriteTo(fstream);
            fstream.Close();
            stream.Close();

            MessageBox.Show("Saved");
        }

        private void load_button_Click(object sender, EventArgs e)
        {
            //string path = Application.StartupPath + @"\saves\save.car";
            //string structure = File.ReadAllText(path);
            //display.SimulationEngine.LoadCarStructure(structure);
            display.SimulationEngine.LoadCarStructure(new FileStream(@"saves\save.carStream", FileMode.Open));
        }

        private void datails_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            display.Details = datails_checkBox.Checked;
        }

        private object[] LoadCfg()
        {
            string[] str = File.ReadLines(Application.StartupPath + @"\cfg\image.cfg").ToArray();
            string background = str[0].Substring(str[0].IndexOf(": \"") + 3, str[0].IndexOf("\";") - str[0].IndexOf(": \"") - 3);
            string simulation = str[1].Substring(str[1].IndexOf(": \"") + 3, str[1].IndexOf("\";") - str[1].IndexOf(": \"") - 3);

            Point[] points = new Point[2];
            for (int i = 2; i < 4; i++)
            {
                string x = str[i].Substring(str[i].IndexOf(": \"") + 3, str[i].IndexOf(",") - str[i].IndexOf(": \"") - 3);
                string y = str[i].Substring(str[i].IndexOf(",") + 2, str[i].IndexOf("\";") - str[i].IndexOf(",") - 2);

                points[i - 2] = new Point(int.Parse(x), int.Parse(y));
            }

            return new object[] { background, simulation, points[0], points[1] };
        }
    }
}
