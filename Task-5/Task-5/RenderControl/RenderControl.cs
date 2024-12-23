using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;

namespace Task_5
{
    public partial class RenderControl : OpenGL
    {
        public bool Depth { get; set; } = true;
        public bool ModOrtho { get; set; } = true;
        public bool Mode { get; set; } = false;

        const string fmt = "+0.0;-0.0;±0.0";

        uint Axes, Sphere, Disk, Cylinder;
        private double rx = +20;
        private double ry = -30;
        private IntPtr Quadric = gluNewQuadric();
        public RenderControl() : base()
        {
            InitializeComponent();
            MouseWheel += OnWheel;
        }

        double size = 10.0f;
        double AspectRatio { get => (double)Width / Height; }
        double xMin { get => (AspectRatio > 1) ? -size * AspectRatio : -size; }
        double xMax { get => (AspectRatio > 1) ? +size * AspectRatio : +size; }
        double yMin { get => (AspectRatio < 1) ? -size / AspectRatio : -size; }
        double yMax { get => (AspectRatio < 1) ? +size / AspectRatio : +size; }
        double zMin { get => -size * 1.0; }
        double zMax { get => +size * 1.0; }

        private void Axis(double size)
        {

            double a = size / 10.0;
            glBegin(GL_LINES);
            glColor3ub(255, 0, 0);
            glVertex3d(-size, 0, 0); glVertex3d(+size, 0, 0);
            glColor3ub(0, 255, 0);
            glVertex3d(0, -size, 0); glVertex3d(0, +size, 0);
            glColor3ub(0, 0, 255);
            glVertex3d(0, 0, -size); glVertex3d(0, 0, +size);
            glEnd();
            glColor3ub(155, 155, 155);
            for (double i = -size; i <= size; i += 1)
            {
                glBegin(GL_LINES);
                glVertex3d(-size, 0, i); glVertex3d(+size, 0, i);
                glEnd();
                glBegin(GL_LINES);
                glVertex3d(i, 0, -size); glVertex3d(i, 0, +size);
                glEnd();
            }
            glColor3ub(0, 0, 0);
            DrawText("+X", size + a, 0, 0);
            DrawText("+Y", 0, size + a, 0);
            DrawText("+Z", 0, 0, size + a);
        }

        void GLMat()
        {
            float[] MAmb = { 0.2f, 0.2f, 0.2f };

            float[] MDif = { 0.8f, 0.8f, 0.8f };

            float[] MSpec = { 0.6f, 0.6f, 0.6f };

            float Light = 0.7f * 128;

            glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, MAmb);

            glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, MDif);

            glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, MSpec);

            glMaterialf(GL_FRONT, GL_SHININESS, Light);
        }

        private void RenderContextCreated(object sender, EventArgs e)
        {
            Axes = glGenLists(4);

            glNewList(Axes, GL_COMPILE);
            Axis(9.0f);
            glEndList();

            Rendering();
        }


        private void Rendering()
        {
            if (Mode)
                gluQuadricDrawStyle(Quadric, GLU_LINE);
            else
                gluQuadricDrawStyle(Quadric, GLU_FILL);

            glCallList(Axes);

            // Сфера
            Sphere = Axes + 1;
            glNewList(Sphere, GL_COMPILE);
            glColor3ub(44, 15, 55);
            GLMat();
            glPushMatrix();
            glTranslatef(-1.5f, 1.5f, 3.5f);
            gluSphere(Quadric, 2.0f, 100, 20);
            glPopMatrix();
            glEndList();

            // Конус
            Cylinder = Axes + 2;
            glNewList(Cylinder, GL_COMPILE);
            glColor3ub(12, 134, 56);
            GLMat();
            glPushMatrix();
            glTranslatef(3.5f, -3.0f, 3.0f);
            glRotatef(-90, 1, 0, 0);
            gluCylinder(Quadric, 3.0f, 0.0f, 2.5f, 100, 20);
            glPopMatrix();
            glEndList();

            // Частковий диск
            Disk = Axes + 3;
            glNewList(Disk, GL_COMPILE);
            glColor3ub(100, 25, 60);
            GLMat();
            glPushMatrix();
            glTranslatef(-2.0f, -2.5f, -4.0f);
            glRotatef(90, 1, 0, 0);
            gluPartialDisk(Quadric, 0.5f, 2.5f, 100, 20, 180, 90);
            glPopMatrix();
            glEndList();

            glCallList(Sphere);
            glCallList(Cylinder);
            glCallList(Disk);
        }



        bool fDown = false;
        double x0, y0;
        private void OnDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                fDown = true;
                x0 = e.Location.X;
                y0 = e.Location.Y;
            }
        }

        private void OnUp(object sender, MouseEventArgs e)
        {
            if (fDown && (e.Button == MouseButtons.Left))
                fDown = false;
        }

        private void OnMove(object sender, MouseEventArgs e)
        {
            if (fDown)
            {
                ry -= (x0 - e.Location.X) / 5.0;
                rx -= (y0 - e.Location.Y) / 5.0;
                CheckAngle(ref ry);
                CheckAngle(ref rx);
                x0 = e.Location.X;
                y0 = e.Location.Y;
                Invalidate();
            }
        }

        private void OnWheel(object sender, MouseEventArgs e)
        {
            size += e.Delta / 2000.0;
            Invalidate();
        }

        private void CheckAngle(ref double angle)
        {
            if (angle > 360) angle -= 360;
            if (angle < -360) angle += 360;
        }

        private void OnRender(object sender, EventArgs e)
        {

            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

            glMatrixMode(GL_PROJECTION);
            glLoadIdentity();

            if (ModOrtho)
            {
                gluPerspective(45, AspectRatio, 0.1, 1000.0);
            }
            else
            {
                glOrtho(xMin, xMax, yMin, yMax, zMin, zMax);
            }

            glMatrixMode(GL_MODELVIEW);


            glLoadIdentity();
            glViewport(0, 0, Width, Height);

            if (ModOrtho)
                glTranslated(0, 0, -size);
            glRotated(rx, 1, 0, 0);
            glRotated(ry, 0, 1, 0);

            glEnable(GL_LIGHT0);
            glLightModeli(GL_LIGHT_MODEL_TWO_SIDE, 0);

            glMatrixMode(GL_MODELVIEW);
            glPushMatrix();
            glLoadIdentity();

            glMatrixMode(GL_PROJECTION);
            glPushMatrix();
            glLoadIdentity();
            gluOrtho2D(0, Width, Height, 0);
            glColor(Color.LightGray);
            DrawText($"Rx: {rx.ToString(fmt)}°", 10, 20);
            DrawText($"Ry: {ry.ToString(fmt)}°", 10, 40);
            DrawText($"size 1:{size.ToString("0.0")}", 10, 60);
            glPopMatrix();

            glMatrixMode(GL_MODELVIEW);
            glPopMatrix();


            glColor(Color.Black);

            if (Depth) glEnable(GL_DEPTH_TEST);


            float[] tmp1 = { 0.4f, 0.4f, 0.6f, 1f };
            float[] tmp2 = { 0f, 0f, 0.8f, 1f };
            float[] tmp3 = { 0f, 0f, 0.15f, 1f };
            glLightfv(GL_LIGHTING, GL_DIFFUSE, tmp1);
            glLightfv(GL_LIGHTING, GL_SPECULAR, tmp2);
            glLightfv(GL_LIGHTING, GL_AMBIENT, tmp3);

            float[] LightPosition = { 0, 0, 0, 1f };
            glLightfv(GL_LIGHTING, GL_POSITION, LightPosition);

            glCallList(Axes);

            glEnable(GL_COLOR_MATERIAL);
            glEnable(GL_LIGHTING);
            Rendering();
            glCallList(Sphere);
            glCallList(Disk);
            glCallList(Cylinder);

            if (Depth) glDisable(GL_DEPTH_TEST);

            glDisable(GL_COLOR_MATERIAL);
            glDisable(GL_LIGHTING);
        }

    }
}