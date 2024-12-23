using System;
using System.Windows.Forms;
using static Task_4.OpenGL;


namespace Task_4
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, System.EventArgs e)
        {
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            renderControl1.x = (Convert.ToSingle(numericUpDown.Value));
            renderControl1.Invalidate();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            renderControl1.A = (Convert.ToSingle(numericUpDown.Value));
            renderControl1.Invalidate();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            renderControl1.B = (Convert.ToSingle(numericUpDown.Value));
            renderControl1.Invalidate();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            renderControl1.lx1 = (Convert.ToSingle(numericUpDown.Value));
            renderControl1.Invalidate();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            renderControl1.ly1 = (Convert.ToSingle(numericUpDown.Value));
            renderControl1.Invalidate();
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            renderControl1.lx2 = (Convert.ToSingle(numericUpDown.Value));
            renderControl1.Invalidate();
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            renderControl1.ly2 = (Convert.ToSingle(numericUpDown.Value));
            renderControl1.Invalidate();
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            renderControl1.y = (Convert.ToSingle(numericUpDown.Value));
            renderControl1.Invalidate();
        }
    }
}
