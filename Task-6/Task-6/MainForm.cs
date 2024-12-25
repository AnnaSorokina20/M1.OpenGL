using System.Windows.Forms;
using static Task_6.OpenGL;


namespace Task_6
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void OnUpdateInfo()
        {
            S.Text = $"S = {rc.dist.ToString("0.00")}";
            ag.Text = $"ψ = {(rc.Ag + rc.aZ).ToString("0.0")}°";
            az.Text = $"φ = {rc.aZ.ToString("0.0")}°";
            aw.Text = $"ω = {rc.aW.ToString("0.0")}°";
            M.Text = $"M 1 : {rc.M.ToString("0.0")}";
            //at.Text = $"θ = {rc.Po.angle.ToString("0.0")}°";
            aX.Text = $"α = {rc.aX.ToString("0.0")}°";
            aY.Text = $"β = {rc.aY.ToString("0.0")}°";
        }

        private void gridCheckedChanged(object sender, System.EventArgs e)
        {
            rc.gridX = gridX.Checked;
            rc.gridY = gridY.Checked;
            rc.gridZ = gridZ.Checked;
            rc.Invalidate();
        }
    }
}
