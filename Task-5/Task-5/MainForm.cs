using System.Windows.Forms;
using static Task_5.OpenGL;


namespace Task_5
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void cbDepthTest_CheckedChanged(object sender, System.EventArgs e)
        {
            rc.Depth = cbDepthTest.Checked;
            rc.Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            System.Windows.Forms.CheckBox m_e = (System.Windows.Forms.CheckBox)sender;
            if (m_e.Checked == true)
            {
                checkBox2.Checked = false;
            }
            if (m_e.Checked == false)
            {
                checkBox2.Checked = true;
            }
            rc.ModOrtho = m_e.Checked;
            rc.Invalidate();
        }

        private void checkBox2_CheckedChanged(object sender, System.EventArgs e)
        {
            System.Windows.Forms.CheckBox m_e = (System.Windows.Forms.CheckBox)sender;
            if (m_e.Checked == true)
            {
                checkBox1.Checked = false;
            }
            if (m_e.Checked == false)
            {
                checkBox1.Checked = true;
            }
            rc.ModOrtho = !m_e.Checked;
            rc.Invalidate();
        }

        private void checkBox4_CheckedChanged(object sender, System.EventArgs e)
        {
            System.Windows.Forms.CheckBox m_e = (System.Windows.Forms.CheckBox)sender;
            if (m_e.Checked == true)
            {
                checkBox3.Checked = false;
            }
            if (m_e.Checked == false)
            {
                checkBox3.Checked = true;
            }
            rc.Mode = !m_e.Checked;
            rc.Invalidate();
        }

        private void checkBox3_CheckedChanged(object sender, System.EventArgs e)
        {
            System.Windows.Forms.CheckBox m_e = (System.Windows.Forms.CheckBox)sender;
            if (m_e.Checked == true)
            {
                checkBox4.Checked = false;
            }
            if (m_e.Checked == false)
            {
                checkBox4.Checked = true;
            }
            rc.Mode = m_e.Checked;
            rc.Invalidate();
        }
    }
}
