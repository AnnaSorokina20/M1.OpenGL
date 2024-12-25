using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Windows.Forms;
using static System.Math;

namespace Task_6
{
    public partial class RenderControl : OpenGL
    {
        public event Action UpdateInfo;

        private double ax = +20;
        private double ay = -30;
        private double m = 1;

        private bool fDown = false;
        private double x0, y0;

        private double a = 0.4;  // довжина 1 сегмента
        private double b = 0.5;  // довжина 2 сегмента
        private double c => 2 * b; // довжина 3 сегмента

        private double s = 0.3;  // Параметр S (рух по осі X)
        private double ds = 1.0 / 100.0;

        private double az = 0; // кут φ
        private double aw = 0; // кут θ
        private double ag = 0; // кут ψ

        public double S
        {
            get => s;
            set => s = (value >= -1 && value <= 1) ? value : s;
        }

        public double aX => ax;
        public double aY => ay;
        public double aZ => az;
        public double aW => aw;
        public double Ag => ag;
        public double M => m;
        public double dist { get; set; }

        public bool gridX { get; set; }
        public bool gridY { get; set; }
        public bool gridZ { get; set; }
        public bool Perspectiv;

        private double size = 2.5;
        private double AspectRatio => (double)Width / Height;
        private double xMin => (AspectRatio > 1) ? -size * AspectRatio : -size;
        private double xMax => (AspectRatio > 1) ? +size * AspectRatio : +size;
        private double yMin => (AspectRatio < 1) ? -size / AspectRatio : -size;
        private double yMax => (AspectRatio < 1) ? +size / AspectRatio : +size;
        private double zMin => -size * 2.0;
        private double zMax => +size * 2.0;

        public RenderControl()
        {
            InitializeComponent();
            MouseWheel += OnMouseWheel;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                fDown = true;
                x0 = e.Location.X;
                y0 = e.Location.Y;
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (fDown && e.Button == MouseButtons.Left)
                fDown = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (fDown)
            {
                ay -= (x0 - e.X) / 2.0;
                ax -= (y0 - e.Y) / 2.0;
                x0 = e.X;
                y0 = e.Y;
                Invalidate();
            }
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            m += e.Delta / 1000.0;
            Invalidate();
        }

        private void RenderControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up) az += 1.5;
            if (e.KeyCode == Keys.Down) az -= 1.5;
            if (e.KeyCode == Keys.Left) S -= ds;
            if (e.KeyCode == Keys.Right) S += ds;
            if (e.KeyCode == Keys.PageUp) aw += 1.5;
            if (e.KeyCode == Keys.PageDown) aw -= 1.5;
            if (e.KeyCode == Keys.Home) ag += 1.5;
            if (e.KeyCode == Keys.End) ag -= 1.5;

            Invalidate();
        }

        private void OnRender(object sender, EventArgs e)
        {
            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

            glMatrixMode(GL_PROJECTION);
            glLoadIdentity();

            if (Perspectiv)
                gluPerspective(45, AspectRatio, 0.1, 1000.0);
            else
                glOrtho(xMin, xMax, yMin, yMax, zMin, zMax);

            glMatrixMode(GL_MODELVIEW);
            glLoadIdentity();
            glViewport(0, 0, Width, Height);

            if (Perspectiv)
                glTranslated(0, 0, -size);

            glRotated(ax, 1, 0, 0);
            glRotated(ay, 0, 1, 0);
            glScaled(m, m, m);

            glEnable(GL_LIGHTING);
            glEnable(GL_COLOR_MATERIAL);
            glEnable(GL_DEPTH_TEST);
            glEnable(GL_LIGHT0);

            glColor(Color.Black);
            Axes(1.9);

            dist = S;

            // 1 сегмент
            glTranslated(S, 0, 0);
            glRotated(az, 0, 0, -1);
            Segment(a, Color.Red, 5);

            // 2 сегмент
            glTranslated(0, a, 0);
            glRotated(aw, 0, 0, -1);
            Segment(b, Color.Green, 5);

            // 3 сегмент
            glTranslated(0, b, 0);
            glRotated(ag, 0, 0, -1);
            Segment(c, Color.Blue, 5);

            glDisable(GL_LIGHTING);
            glDisable(GL_COLOR_MATERIAL);
            glDisable(GL_DEPTH_TEST);

            UpdateInfo?.Invoke();
        }

        private void Axes(double size)
        {
            double a = size / 15.0;
            glBegin(GL_LINES);
            glVertex3d(-a, 0, 0); glVertex3d(+size, 0, 0);
            glVertex3d(0, -a, 0); glVertex3d(0, +size, 0);
            glVertex3d(0, 0, -a); glVertex3d(0, 0, +size);
            glEnd();
            DrawText("+X", size + a, 0, 0);
            DrawText("+Y", 0, size + a, 0);
            DrawText("+Z", 0, 0, size + a);

            Grid(gridX, gridY, gridZ, size, -size * 0.1, 5);
        }

        private void Segment(double size, Color color, int w)
        {
            glColor(color);
            glLineWidth(w);
            glBegin(GL_LINE_STRIP);
            glVertex3d(0.0, 0.0, size * 0.1);
            glVertex3d(0.0, size, size * 0.1);
            glVertex3d(0.0, size, -size * 0.1);
            glVertex3d(0.0, 0.0, -size * 0.1);
            glVertex3d(0.0, 0.0, size * 0.1);
            glEnd();
            glLineWidth(1);
        }

        private void Grid(bool x, bool y, bool z, double s, double shift, int count = 10)
        {
            double h = s / count;
            glColor(Color.AliceBlue);
            glEnable(GL_LINE_STIPPLE);
            glLineStipple(1, 0xCCCC);
            glBegin(GL_LINES);
            for (int i = 0; i <= count; i++)
            {
                if (z) { glVertex3d(i * h, 0, shift); glVertex3d(i * h, s, shift); }
                if (y) { glVertex3d(0, shift, i * h); glVertex3d(s, shift, i * h); }
                if (x) { glVertex3d(shift, 0, i * h); glVertex3d(shift, s, i * h); }
            }
            glEnd();
            glDisable(GL_LINE_STIPPLE);
        }
    }
}


