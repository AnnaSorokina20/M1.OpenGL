using System;
using System.Windows.Forms;

namespace Task_7
{
    public partial class MainForm : Form
    {
        private RenderControl renderControl;

        public MainForm(bool fullscreen = false)
        {
            InitializeComponent();

            renderControl = new RenderControl();
            renderControl.Dock = DockStyle.Fill;
            this.Controls.Add(renderControl);

            if (fullscreen)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            using (SettingsForm settings = new SettingsForm())
            {
                if (settings.ShowDialog() == DialogResult.OK)
                {
                    renderControl.ApplySettings(settings.AnimationSpeed);
                }
            }
        }
        private void OnLoad(object sender, EventArgs e)
        {

        }
    }
}
