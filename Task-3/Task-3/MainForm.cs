using System;
using System.Windows.Forms;
using static Task_3.OpenGL;


namespace Task_3
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
            renderControl1.set_pos_min(Convert.ToSingle(numericUpDown.Value));
            renderControl1.Invalidate();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            renderControl1.set_pos_max(Convert.ToSingle(numericUpDown.Value));
            renderControl1.Invalidate();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            renderControl1.set_point(Convert.ToInt32(numericUpDown.Value));
            renderControl1.Invalidate();
        }
    }
}

