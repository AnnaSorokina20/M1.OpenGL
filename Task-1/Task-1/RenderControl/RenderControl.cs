using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using static Task_1.OpenGL;

namespace Task_1
{
    public partial class RenderControl : OpenGL
    {
        public RenderControl()
        {
            InitializeComponent();
            Render += OnRender; // Підключаємо метод рендерингу до події Render
        }

        private void OnRender(object sender, EventArgs e)
        {
            // Очищення екрану
            glClear(GL_COLOR_BUFFER_BIT);
            glClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            // Встановлення системи координат
            glLoadIdentity();
            glViewport(0, 0, Width, Height);
            glOrtho(-3, 7, -3, 2, -1.0, 1.0); // Межі системи координат згідно із завданням

            // Малювання елементів
            DrawAxes();
            DrawGrid();
            DrawFigure();
            DrawPoints();
        }

        private void DrawFigure()
        {
            glLineWidth(5); // Товщина ліній
            glBegin(GL_LINE_STRIP); // GL_LINE_STRIP, як зазначено у варіанті
            glColor(Color.Blue); // Синій контур

            // Вершини фігури
            glVertex2d(-2.5, -2.5); // Ліва нижня
            glVertex2d(-2.5, -1.5); // Ліва середня
            glVertex2d(-1.5, 1.5);  // Ліва верхня
            glVertex2d(-0.5, 1.5);  // Права верхня
            glVertex2d(0.5, -1.5);  // Права середня
            glVertex2d(0.5, -2.5);  // Права нижня
            glVertex2d(-2.5, -2.5); // Ліва нижня (повернення до початку)

            glEnd(); // Завершуємо малювання
        }

        private void DrawPoints()
        {
            glPointSize(8); // Розмір точок
            glBegin(GL_POINTS);
            glColor(Color.Red); // Червоні точки

            // Координати точок
            double[][] points = new double[][]
            {
                new double[] { -2.5, -2.5 },
                new double[] { -2.5, -1.5 },
                new double[] { -1.5,  1.5 },
                new double[] { -0.5,  1.5 },
                new double[] {  0.5, -1.5 },
                new double[] {  0.5, -2.5 }
            };

            // Малюємо точки
            foreach (var point in points)
            {
                glVertex2d(point[0] + 5, point[1]);
            }
            glEnd();

            // Підписуємо координати
            foreach (var point in points)
            {
                string label = $"({point[0]}, {point[1]})";
                DrawText(label, point[0] + 0.1, point[1] + 0.1); // Підпис першої точки
                string label2 = $"({point[0] + 5}, {point[1]})";
                DrawText(label2, point[0] + 5 + 0.1, point[1] + 0.1); // Підпис зміщеної точки
            }
        }

        private void DrawAxes()
        {
            glLineWidth(2); // Товщина ліній
            glBegin(GL_LINES);
            glColor(Color.White); // Білий колір для осей

            // Вісь X
            glVertex2d(-2.5, 0.0);
            glVertex2d(6.5, 0.0);

            // Вісь Y
            glVertex2d(0.0, -2.5);
            glVertex2d(0.0, 1.5);

            glEnd();
        }

        private void DrawGrid()
        {
            glLineWidth(1); // Товщина ліній для сітки
            glEnable(GL_LINE_STIPPLE); // Увімкнення пунктирних ліній
            glLineStipple(1, 0xAAAA);  // Шаблон пунктиру

            glBegin(GL_LINES);
            glColor(Color.Gray); // Сірий колір для сітки

            // Горизонтальні лінії
            for (double y = -2.5; y <= 1.5; y += 0.5)
            {
                glVertex2d(-2.5, y);
                glVertex2d(6.5, y);
            }

            // Вертикальні лінії
            for (double x = -2.5; x <= 6.5; x += 0.5)
            {
                glVertex2d(x, -2.5);
                glVertex2d(x, 1.5);
            }

            glEnd();
            glDisable(GL_LINE_STIPPLE); // Вимкнення пунктирних ліній
        }

        
    }
}
