using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using static System.Math;


namespace Task_4
{
    public partial class RenderControl : OpenGL
    {
        // параметри для гіперболи
        public double A = 1;
        public double B = 1;
        public double X0 = 0;
        public double Y0 = 0;

        // параметри для еліпса
        public double x = 2, y = 1;

        public float lx1 = -1, ly1 = -1, lx2 = 1, ly2 = 1;
        private float x_min, x_max;
        private float y_min, y_max;
        private double size = 0;
        s_data_render data_render;
        private struct vec2
        {
            public vec2(float x, float y)
            {
                this.x = x;
                this.y = y;
            }
            public double x;
            public double y;
        }
        private struct s_data_render
        {
            public s_data_render(bool? temp = null)
            {
                this.figure = new List<string>();
                this.points = new List<List<float>>();
            }
            public List<string> figure;
            public List<List<float>> points;
            public void clear()
            {
                this.points.Clear();
                this.figure.Clear();
            }
        }

        private List<vec2> point_zeros = new List<vec2>();
        public string name = "radioButton1";
        public RenderControl()
        {
            InitializeComponent();
            data_render = new s_data_render(null);
        }
        public void set_pos_min(float x_min)
        {
            this.x_min = x_min;
        }
        public void set_pos_max(float x_max)
        {
            this.x_max = x_max;
        }


        void Hyperbola_gen(string name, double xCenter, double yCenter, double A, double B, int pointCount = 360)
        {
            double start = -3.0;
            double end = 3.0;
            double step = (end - start) / pointCount;

            // перша гілка (x > 0)
            data_render.figure.Add(name);
            for (double t = start; t <= end; t += step)
            {
                double X = A * Math.Cosh(t);
                double Y = B * Math.Sinh(t);
                data_render.points.Add(new List<float> { (float)(X + xCenter), (float)(Y + yCenter) });
            }
            data_render.points.Add(null);

            // друга гілка  (x < 0)
            data_render.figure.Add(name);
            for (double t = start; t <= end; t += step)
            {
                double X = -A * Math.Cosh(t);
                double Y = B * Math.Sinh(t);
                data_render.points.Add(new List<float> { (float)(X + xCenter), (float)(Y + yCenter) });
            }
            data_render.points.Add(null);
        }







        void Ellipse_gen(string name, double xCenter, double yCenter, double rx, double ry = double.MaxValue, int pointCount = 360)
        {
            data_render.figure.Add(name);
            if (ry == double.MaxValue)
            {
                ry = rx;
            }
            double step = 2.0 * Math.PI / pointCount;
            for (double angle = 0; angle < 2.0 * Math.PI; angle += step)
            {
                double dx = rx * Math.Cos(angle);
                double dy = ry * Math.Sin(angle);
                List<float> temp = new List<float>();
                temp.Add(Convert.ToSingle(dx + xCenter));
                temp.Add(Convert.ToSingle(dy + yCenter));
                data_render.points.Add(temp);
            }
            data_render.points.Add(null);
        }
        void Stroke_draw()
        {
            glColor3ub(255, 255, 255);
            glBegin(GL_LINE_STRIP);
            foreach (List<float> i in data_render.points)
            {
                if (i != null)
                    glVertex2f(i[0], i[1]);
                else
                {
                    glEnd();
                    glBegin(GL_LINE_STRIP);
                }
            }
            glEnd();
        }
        private void AnalyzeCurvesAndFindIntersections()
        {
            y_min = float.MaxValue;
            y_max = float.MinValue;
            x_min = float.MaxValue;
            x_max = float.MinValue;

            int indx = 0;
            float l_x = 0, l_y = 0;
            float b_x = float.MaxValue, b_y = float.MaxValue;

            foreach (List<float> point in data_render.points)
            {
                if (point != null)
                {
                    l_x = point[0];
                    l_y = point[1];

                    if (b_x == float.MaxValue) b_x = l_x;
                    if (b_y == float.MaxValue) b_y = l_y;

                    // оновлення меж
                    if (Math.Min(y_min, l_y) == l_y)
                        y_min = l_y - 0.1f;
                    if (Math.Max(y_max, l_y) == l_y)
                        y_max = l_y + 0.1f;

                    if (Math.Min(x_min, l_x) == l_x)
                        x_min = l_x - 0.1f;
                    if (Math.Max(x_max, l_x) == l_x)
                        x_max = l_x + 0.1f;

                    // перевіряємо перетин
                    if (indx >= 0 && indx < data_render.figure.Count && data_render.figure[indx] == "++"
                        && b_x != float.MaxValue && b_y != float.MaxValue)
                    {
                        if (checkIntersectionOfTwoLineSegments(
                            new vec2(b_x, b_y),
                            new vec2(l_x, l_y),
                            new vec2(lx1, ly1),
                            new vec2(lx2, ly2)))
                        {
                            point_zeros.Add(new vec2(b_x, b_y));
                        }
                    }

                    b_x = l_x;
                    b_y = l_y;
                }
                else
                {
                    indx++;

                    b_x = float.MaxValue;
                    b_y = float.MaxValue;
                }
            }
        }


        private void draw_main_axes()
        {
            glEnable(GL_LINE_STIPPLE);
            glLineStipple(2, 0X00FF);
            glLineWidth(1);
            glColor3ub(155, 155, 155);
            for (double i = 0.5; i <= size; i += 0.5)
            {
                glBegin(GL_LINES);
                glVertex2d(i, -size);
                glVertex2d(i, size);
                glEnd();
                glBegin(GL_LINES);
                glVertex2d(-size, i);
                glVertex2d(size, i);
                glEnd();
            }
            for (double i = -0.5; i >= -size; i -= 0.5)
            {
                glBegin(GL_LINES);
                glVertex2d(i, -size);
                glVertex2d(i, size);
                glEnd();
                glBegin(GL_LINES);
                glVertex2d(-size, i);
                glVertex2d(size, i);
                glEnd();

            }
            glDisable(GL_LINE_STIPPLE);

            glColor3ub(0, 0, 0);
            glBegin(GL_LINES);
            glVertex2d(-size, 0);
            glVertex2d(size, 0);
            glColor3ub(0, 0, 0);
            glBegin(GL_LINES);
            glVertex2d(0, -size);
            glVertex2d(0, size);
            glEnd();

        }

        private bool checkIntersectionOfTwoLineSegments(vec2 p1, vec2 p2, vec2 p3, vec2 p4)
        {
            if (p2.x < p1.x)
            {
                vec2 tmp = p1;
                p1 = p2;
                p2 = tmp;
            }
            if (p4.x < p3.x)
            {
                vec2 tmp = p3;
                p3 = p4;
                p4 = tmp;
            }
            if (p2.x < p3.x)
            {
                return false;
            }
            if ((p1.x - p2.x == 0) && (p3.x - p4.x == 0))
            {

                if (p1.x == p3.x)
                {
                    if (!((Math.Max(p1.y, p2.y) < Math.Min(p3.y, p4.y)) || (Math.Min(p1.y, p2.y) > Math.Max(p3.y, p4.y))))
                    {

                        return true;
                    }
                }

                return false;
            }
            double Xa = p1.x;
            double A2 = (p3.y - p4.y) / (p3.x - p4.x);
            double b2 = p3.y - A2 * p3.x;
            double Ya = A2 * Xa + b2;
            if (p1.x - p2.x == 0)
            {

                if (p3.x <= Xa && p4.x >= Xa && Math.Min(p1.y, p2.y) <= Ya &&
                        Math.Max(p1.y, p2.y) >= Ya)
                {

                    return true;
                }

                return false;
            }

            Xa = p3.x;
            double A1 = (p1.y - p2.y) / (p1.x - p2.x);
            double b1 = p1.y - A1 * p1.x;
            Ya = A1 * Xa + b1;
            if (p3.x - p4.x == 0)
            {

                if (p1.x <= Xa && p2.x >= Xa && Math.Min(p3.y, p4.y) <= Ya &&
                        Math.Max(p3.y, p4.y) >= Ya)
                {

                    return true;
                }

                return false;
            }

            A1 = (p1.y - p2.y) / (p1.x - p2.x);
            A2 = (p3.y - p4.y) / (p3.x - p4.x);
            b1 = p1.y - A1 * p1.x;
            b2 = p3.y - A2 * p3.x;

            if (A1 == A2)
            {
                return false;
            }

            Xa = (b2 - b1) / (A1 - A2);
            if ((Xa < Math.Max(p1.x, p3.x)) || (Xa > Math.Min(p2.x, p4.x)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void draw_poin(vec2 point)
        {
            glPointSize(8);
            glColor3ub(0, 0, 0);
            glEnable(GL_POINT_SMOOTH);
            glBegin(GL_POINTS);
            glVertex2f(Convert.ToSingle(point.x), Convert.ToSingle(point.y));
            glEnd();
            glPointSize(5);
            glColor3ub(255, 0, 0);
            glDisable(GL_POINT_SMOOTH);
            glBegin(GL_POINTS);
            glVertex2f(Convert.ToSingle(point.x), Convert.ToSingle(point.y));
            glEnd();
        }
        private void draw_poin(vec2 point, Color color)
        {
            glPointSize(8);
            glColor3ub(0, 0, 0);
            glEnable(GL_POINT_SMOOTH);
            glBegin(GL_POINTS);
            glVertex2f(Convert.ToSingle(point.x), Convert.ToSingle(point.y));
            glEnd();
            glPointSize(5);
            glColor3ub(color.R, color.G, color.B);
            glBegin(GL_POINTS);
            glVertex2f(Convert.ToSingle(point.x), Convert.ToSingle(point.y));
            glEnd();
            glDisable(GL_POINT_SMOOTH);
        }
        private void draw_line()
        {
            glLineWidth(1);
            glColor3ub(0, 100, 50);
            glBegin(GL_LINES);
            glVertex2d(lx1, ly1);
            glVertex2d(lx2, ly2);
            glEnd();
            draw_poin(new vec2(lx1, ly1), Color.Black);
            draw_poin(new vec2(lx2, ly2), Color.Black);
        }
        private void OnRender(object sender, EventArgs e)
        {
            point_zeros.Clear();
            glClear(GL_COLOR_BUFFER_BIT);
            glLoadIdentity();

            if (Width > Height)
                glViewport((Width - Height) / 2, 0, Height, Height);
            else
                glViewport(0, (Height - Width) / 2, Width, Width);

            data_render.clear();
            Ellipse_gen("+", 0, 0, x, y);
            Hyperbola_gen("++", X0, Y0, A, B);
            AnalyzeCurvesAndFindIntersections();

            float g = y_max, v = x_max;
            size = (v > g) ? v + g : g + v;
            gluOrtho2D(-size, size, -size, size);

            draw_main_axes();
            Stroke_draw();
            draw_line();
            foreach (var i in point_zeros)
                draw_poin(i);
        }

    }
}