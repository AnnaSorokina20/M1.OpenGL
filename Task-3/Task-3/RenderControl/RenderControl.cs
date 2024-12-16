using System;
using System.Collections.Generic;
using static System.Math;


namespace Task_3
{
    public partial class RenderControl : OpenGL
    {
        private float x_min, x_max;
        private float y_min = -2, y_max = 2.5f;
        private int point;
        private List<float> point_zeros = new List<float>();
        private float increment = 0;
        public RenderControl()
        {
            this.x_min = -0.2f;
            this.x_max = 0.8f;
            this.point = 480; // Кількість точок
            this.increment = (Abs(x_min) + Abs(x_max)) / Convert.ToSingle(point);
            InitializeComponent();
        }

        public void set_pos_min(float x_min)
        {
            this.x_min = x_min;
        }
        public void set_pos_max(float x_max)
        {
            this.x_max = x_max;
        }
        public void set_point(int point)
        {
            this.point = point;
            this.increment = (Abs(x_min) + Abs(x_max)) / Convert.ToSingle(point);
        }

        private void get_info_fun()
        {
            y_min = 5;
            y_max = -5;
            point_zeros.Clear(); // Очищення списку нулів
            float y = 0;
            float prev_y = float.MaxValue;

            for (float x = x_min; x < x_max; x += increment)
            {
                y = fun(x);

                // Оновлення Ymin і Ymax
                if (y < y_min) y_min = y - 0.1f;
                if (y > y_max) y_max = y + 0.1f;

                // Пошук точок перетину з віссю X
                if (prev_y * y <= 0)
                {
                    point_zeros.Add(x - increment / 2); // Середина інтервалу
                }
                prev_y = y;
            }
        }

        private float fun(float x)
        {
            return 3 * Convert.ToSingle(Sin(0.2 * x + Sin(2 * x)));
        }

        private void draw_main_axes()
        {
            glLineWidth(1);
            glColor3ub(200, 200, 200);
            glBegin(GL_LINES);
            glVertex2d(x_min, 0);
            glVertex2d(x_max, 0);
            glVertex2d(0, y_min);
            glVertex2d(0, y_max);
            glEnd();
        }

        private void draw_fun()
        {
            glLineWidth(2);
            glColor3ub(255, 0, 0); // Червоний колір графіка
            glBegin(GL_LINE_STRIP);
            for (float x = x_min; x < x_max; x += increment)
            {
                glVertex2f(x, fun(x));
            }
            glEnd();
        }

        private void draw_poin_fun(float x)
        {
            glPointSize(8);
            glColor3ub(255, 255, 0); // Жовті точки
            glBegin(GL_POINTS);
            glVertex2f(x, 0);
            glEnd();
        }

        private void OnRender(object sender, EventArgs e)
        {
            glClear(GL_COLOR_BUFFER_BIT);
            glLoadIdentity();
            glViewport(0, 0, Width, Height);

            get_info_fun(); // Розрахунок функції та точок перетину
            glOrtho(x_min, x_max, y_min, y_max, -1, 1);

            draw_main_axes(); // Осі координат
            draw_fun();       // Графік функції
            foreach (var zero in point_zeros)
                draw_poin_fun(zero); // Точки перетину
        }
        

    }
}


