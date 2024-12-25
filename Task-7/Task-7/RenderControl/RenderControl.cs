using System;
using System.Drawing;
using System.Timers;
using static Task_7.OpenGL;

namespace Task_7
{
    public partial class RenderControl : OpenGL
    {
        private Timer animationTimer;
        private float rotationAngle = 0.0f;
        private int animationSpeed = 50; 

        public RenderControl()
        {
            InitializeComponent();
            Render += OnRender;

            animationTimer = new Timer();
            animationTimer.Interval = 1000 / animationSpeed;
            animationTimer.Elapsed += AnimationTimer_Tick;
            animationTimer.AutoReset = true;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, ElapsedEventArgs e)
        {
            rotationAngle += 1.0f;
            if (rotationAngle >= 360.0f)
            {
                rotationAngle = 0.0f;
            }

            this.Invalidate();
        }

        private void OnRender(object sender, EventArgs e)
        {
            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
            glClearColor(0.1f, 0.1f, 0.1f, 1.0f);

            glLoadIdentity();
            glViewport(0, 0, Width, Height);
            glOrtho(-2, 2, -2, 2, -1.0, 1.0);

            DrawRotatingTriangle();
            DrawRotatingCircle();

            SwapBuffers();
        }
        public void ApplySettings(int speed)
        {
            animationSpeed = speed;
            animationTimer.Interval = 1000 / animationSpeed; 
        }

        private void DrawRotatingTriangle()
        {
            glPushMatrix();
            glRotatef(rotationAngle, 0.0f, 0.0f, 1.0f);

            glBegin(GL_TRIANGLES);
            glColor3f(1.0f, 0.0f, 0.0f); glVertex2f(-0.5f, -0.5f);
            glColor3f(0.0f, 1.0f, 0.0f); glVertex2f(0.5f, -0.5f);
            glColor3f(0.0f, 0.0f, 1.0f); glVertex2f(0.0f, 0.5f);
            glEnd();

            glPopMatrix();
        }

        private void DrawRotatingCircle()
        {
            glPushMatrix();
            glTranslatef(1.0f, 1.0f, 0.0f);
            glRotatef(rotationAngle, 0.0f, 0.0f, 1.0f);

            glColor3f(1.0f, 1.0f, 0.0f);
            glBegin(GL_POLYGON);

            int segments = 100;
            float radius = 0.5f;
            for (int i = 0; i < segments; i++)
            {
                double angle = 2 * Math.PI * i / segments;
                glVertex2f((float)Math.Cos(angle) * radius, (float)Math.Sin(angle) * radius);
            }

            glEnd();
            glPopMatrix();
        }

        private void SwapBuffers()
        {
            this.Invalidate();
        }

    }
}
