using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;

namespace Task_2
{
    public partial class RenderControl : OpenGL
    {
        public bool isT = false;
        public bool isP = false;
        public bool isL = false;
        public int g = 1;
        public int v = 1;
        double a = 1500;

        public RenderControl()
        {
            InitializeComponent();
        }

        private void OnRender(object sender, EventArgs e)
        {
            glClear(GL_COLOR_BUFFER_BIT);
            glLoadIdentity();

            // Встановлення ізотропного масштабу
            if (Width > Height)
                glViewport((Width - Height) / 2, 0, Height, Height);
            else
                glViewport(0, (Height - Width) / 2, Width, Width);

            // Налаштування координат
            double size = 0;
            if (v > g)
                size = ((a * 3.5f) * v) + (a * g);
            else
                size = ((a * 3.5f) * g) + (a * v);
            glOrtho(-(a * v) - (a * 3.5f), size, -size, (a * 3.5f), -1, +1);

            if (isT)
                DrawPrimitive(GL_FILL);
            if (isP)
            {
                glPointSize(7);
                DrawPrimitive(GL_POINT);
            }
            if (isL)
            {
                DrawPrimitive(GL_LINE);
            }


        }

        // Метод для малювання плитки
        public void DrawPrimitive(uint mode)
        {
            double g_x;
            double g_y;

            for (int j = 0; j < v; j++)
            {
                g_x = -a * 0.5 * j;
                g_y = -a * 2 * j; // Правильне вертикальне зміщення
                glPolygonMode(GL_FRONT_AND_BACK, mode);
                for (int i = 0; i < g; i++)
                {


                    // Червоний квадрат

                    glBegin(GL_QUADS);
                    glColor(Color.Red);
                    glVertex2d(g_x, g_y);
                    glVertex2d(g_x + a, g_y);
                    glVertex2d(g_x + a, g_y - a);
                    glVertex2d(g_x, g_y - a);
                    glEnd();

                    // Синій жовтий
                    glBegin(GL_TRIANGLE_STRIP);
                    glColor(Color.Yellow);
                    glVertex2d(g_x + a, g_y);
                    glVertex2d(g_x + a, g_y - a);
                    glVertex2d(g_x + a * 2, g_y - (a / 2));
                    glEnd();

                    // Синій трикутник
                    glBegin(GL_TRIANGLE_STRIP);
                    glColor(Color.Blue);
                    glVertex2d(g_x + a, g_y - a);
                    glVertex2d(g_x + a * 2, g_y - (a / 2));
                    glVertex2d(g_x + a * 2, g_y - a * 1.5);
                    glEnd();

                    // Зелений квадрат
                    glBegin(GL_QUADS);
                    glColor(Color.Green);
                    glVertex2d(g_x + a, g_y);
                    glVertex2d(g_x + a * 2, g_y - (a / 2));
                    glVertex2d(g_x + a * 2.5, g_y + a * 0.5);
                    glVertex2d(g_x + a * 1.5, g_y + a);
                    glEnd();


                    // білий трикутник
                    glBegin(GL_TRIANGLE_STRIP);
                    glColor(Color.White);
                    glVertex2d(g_x + a, g_y);
                    glVertex2d(g_x + a / 2, g_y + a);
                    glVertex2d(g_x + a * 1.5, g_y + a);
                    glEnd();

                    // Синій трикутник 2
                    glBegin(GL_TRIANGLE_STRIP);
                    glColor(Color.Blue);
                    glVertex2d(g_x + a * 2, g_y - (a / 2));
                    glVertex2d(g_x + a * 2.5, g_y + a * 0.5);
                    glVertex2d(g_x + a * 3, g_y - a * 0.5);
                    glEnd();

                    g_x += a * 2;
                    g_y -= a * 0.5;
                }
            }

        }
    }
}

