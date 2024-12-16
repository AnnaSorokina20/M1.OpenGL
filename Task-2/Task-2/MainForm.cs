using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static Task_2.OpenGL;


namespace Task_2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, System.EventArgs e)
        {
            fillButton.Checked = true;
        }

        private void fillButton_CheckedChanged(object sender, System.EventArgs e)
        {
            if (fillButton.Checked)
            {
                renderControl1.isL = false;
                renderControl1.isP = false;
                renderControl1.isT = true;
                renderControl1.Invalidate();
            }
        }

        private void lineButton_CheckedChanged(object sender, System.EventArgs e)
        {
            if (lineButton.Checked)
            {
                renderControl1.isT = false;
                renderControl1.isP = false;
                renderControl1.isL = true;
                renderControl1.Invalidate();
            }
        }

        private void pointButton_CheckedChanged(object sender, System.EventArgs e)
        {
            if (pointButton.Checked)
            {
                renderControl1.isT = false;
                renderControl1.isL = false;
                renderControl1.isP = true;
                renderControl1.Invalidate();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, System.EventArgs e)
        {
            renderControl1.g = Convert.ToInt32(((NumericUpDown)sender).Value);
            renderControl1.Invalidate();
        }

        private void numericUpDown2_ValueChanged(object sender, System.EventArgs e)
        {
            renderControl1.v = Convert.ToInt32(((NumericUpDown)sender).Value);
            renderControl1.Invalidate();
        }

    }
}
