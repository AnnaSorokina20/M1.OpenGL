using System;
using System.Windows.Forms;

namespace Task_7
{
    public partial class SettingsForm : Form
    {
        public int AnimationSpeed { get; private set; } = 50;

        public SettingsForm()
        {
            InitializeComponent();

            tbSpeed.Minimum = 1;
            tbSpeed.Maximum = 100;
            tbSpeed.Value = AnimationSpeed;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tbSpeed_Scroll_1(object sender, EventArgs e)
        {
            AnimationSpeed = tbSpeed.Value;
        }
    }
}
