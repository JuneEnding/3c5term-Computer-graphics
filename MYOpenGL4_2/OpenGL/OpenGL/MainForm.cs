using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace OpenGL
{
    public partial class MainForm : Form
    {
        private int mode = 0;
        private int modeB = 0;
        private int eventmouse = 0;
        Point mouseDownLocation;
        int curPointB = -1;
        private int eventmouseB = 0;

        //List<Surface3D> Object_3D = new List<Surface3D>();
        List<List<Point3D>> Object_3D = new List<List<Point3D>>();

        double scale;
        double angel_OX;
        double angel_OY;
        int minZ; int maxZ;
        Point Point_0 = new Point(0, 0);
        Color basecolor = Color.Red;
        Pen pen_figure_3D;
        List<Point> bezier = new List<Point>();
        List<Point> elips = new List<Point>();

        //_________________________________________________________________________________________________________________
        //_________________________________________________________________________________________________________________
        //_________________________________________________________________________________________________________________

        // начальные параметры отрисовки
        private void IniPaint()
        {
            //
            pen_figure_3D = new Pen(basecolor);
            // точка отсчета по центру формы
            Point_0.X = PictureBox.Width / 2;
            Point_0.Y = PictureBox.Height / 2;
            // установим углы
            angel_OX = 0;
            angel_OY = 0.5;
            // масштаб
            scale = 1.0;
        }
        // проекция 3D на 2D
        private Point convert_3D_in_2D_Point(Point3D val)
        {
            double res_x = -val.z * scale * System.Math.Sin(angel_OX) + val.x * scale * System.Math.Cos(angel_OX) + Point_0.X;
            double res_y = -(val.z * scale * System.Math.Cos(angel_OX) + val.x * scale * System.Math.Sin(angel_OX)) * System.Math.Sin(angel_OY) + val.y * scale * System.Math.Cos(angel_OY) + Point_0.Y;
            return new Point((int)(res_x), (int)(res_y));
        }
        // отрисовка полигона
        private void Polygons(Point[] t, Graphics gr, Color color)
        {
            Pen pen = new Pen(color, 1);
            Brush brush = new SolidBrush(color);
            gr.FillPolygon(brush, t);
        }
        // прозрачность цвета от глубины Z
        private Color GetColorFromSurface(List<Point3D> cursurf, Color basecolor)
        {
            if (cursurf.Count == 0)
                return Color.FromArgb(50, basecolor);

            int z = 0;
            for (int i = 0; i < cursurf.Count; i++)
                z += cursurf[i].z - minZ;
            int c = (maxZ == minZ) ? 100 : (100 * (z / cursurf.Count) / (maxZ - minZ));
            c = Math.Max(c, 0);
            c = Math.Min(c, 200);
            return Color.FromArgb(c + 50, basecolor);
            //return Color.FromArgb(250, basecolor);
        }
        private Color GetColorFromSurface(float c, Color basecolor)
        {
            c = c * 200;
            c = Math.Max(c, 0);
            c = Math.Min(c, 200);
            return Color.FromArgb((int)c + 50, basecolor);
        }

        private Point3Df vector(Point3D p1, Point3D p2)
        {
            return new Point3Df(p2.x - p1.x, p2.y - p1.y, p2.z - p1.z);
        }

        private Point3Df vectornoe(List<Point3D> s)
        {
            Point3Df a = vector(s[2], s[1]);
            Point3Df b = vector(s[2], s[3]);
            return new Point3Df(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }

        private Point3Df normalize(Point3Df v)
        {
            float len = (float)Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
            return new Point3Df(v.x / len, v.y / len, v.z / len);
        }

        private float scalar(Point3Df v1, Point3Df v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        // отрисовка объекта
        //void draw(List<Surface3D> val, Graphics grf)
        private void Draw(List<List<Point3D>> val, Graphics grf)
        {
            // проверка наличия фигуры 3d
            if (val.Count <= 0)
                return;

            // пербираем тайлы
            for (int i = 0; i < val.Count; i++)
            {
                List<Point3D> cursurf = val[i];
                Point3Df v = vectornoe(cursurf);
                Point3Df nv = normalize(v);
                Point3Df light_dir = normalize(new Point3Df(0,-0.5f,-1));
                float intensity = scalar(nv, light_dir);
                if (intensity >= 0)
                {
                    Pen pen = new Pen(GetColorFromSurface(cursurf, basecolor), (int)(scale * 2));
                    Point[] t = new Point[cursurf.Count];
                    for (int j = 0; j < cursurf.Count; j++)
                    {
                        if (paintScelet.Checked)
                        {
                            if (j == cursurf.Count - 1)
                                grf.DrawLine(pen, convert_3D_in_2D_Point(cursurf[0]), convert_3D_in_2D_Point(cursurf[cursurf.Count - 1]));
                            else
                                grf.DrawLine(pen, convert_3D_in_2D_Point(cursurf[j]), convert_3D_in_2D_Point(cursurf[j + 1]));
                        }
                        t[j] = convert_3D_in_2D_Point(cursurf[j]);
                    }

                    if (!paintScelet.Checked)
                    {
                       // Brush brush = new SolidBrush(GetColorFromSurface(cursurf, basecolor));
                        Brush brush = new SolidBrush(GetColorFromSurface(intensity, basecolor));
                        grf.FillPolygon(brush, t);
                    }
                }
            }
        }

        private float Len2D(Point A, Point B)
        {
            int x = A.X - B.X;
            int y = A.Y - B.Y;
            return (float)Math.Sqrt(x * x + y * y);
        }

        private void DrawBezier(List<Point> val, Graphics gr)
        {
            if (val.Count != 4)
                return;

            Pen penRed = new Pen(Color.Red, 2);
            Pen penBlue = new Pen(Color.Blue, 1);
            Pen penBlueB = new Pen(Color.Blue, 2);
            Pen penGreen = new Pen(Color.Green, 2);
            int b = 4;

            // вектора
            gr.DrawLine(penBlue, val[0].X + 1, val[0].Y + 1, val[1].X, val[1].Y + 1);
            gr.DrawLine(penBlue, val[3].X + 1, val[3].Y + 1, val[2].X, val[2].Y + 1);

            gr.DrawBezier(penGreen, val[0], val[1], val[2], val[3]);
            // сами считаем
            //int n = 12;
            //for (int i = 0; i < n; i++)
            //{
            //    float t = (float)i / n;
            //    float c0 = (1 - t) * (1 - t) * (1 - t);
            //    float c1 = (1 - t) * (1 - t) * 3 * t;
            //    float c2 = (1 - t) * t * 3 * t;
            //    float c3 = t * t * t;
            //    float x = c0 * val[0].X + c1 * val[1].X + c2 * val[2].X + c3 * val[3].X;
            //    float y = c0 * val[0].Y + c1 * val[1].Y + c2 * val[2].Y + c3 * val[3].Y;
            //    gr.DrawEllipse(penBlueB, x, y, 3, 3);
            //}

            // опорные точки
            gr.DrawEllipse(penRed, new Rectangle(val[0], new Size(b, b)));
            gr.DrawEllipse(penBlueB, new Rectangle(val[1], new Size(b, b)));
            gr.DrawEllipse(penBlueB, new Rectangle(val[2], new Size(b, b)));
            gr.DrawEllipse(penRed, new Rectangle(val[3], new Size(b, b)));

            // выделенный узел
            if (curPointB >= 0)
                gr.DrawRectangle(penRed, val[curPointB].X - 2, val[curPointB].Y - 2, 9, 9);
        }

        private void DrawElips(List<Point> val, Graphics gr)
        {
            if (val.Count != 3)
                return;

            Pen penRed = new Pen(Color.Red, 2);
            Pen penBlue = new Pen(Color.Blue, 1);
            Pen penBlueB = new Pen(Color.Blue, 2);
            Pen penGreen = new Pen(Color.Green, 2);
            int bord = 4;

            // вектора
            gr.DrawLine(penBlue, val[0].X + 1, val[0].Y + 1, val[1].X, val[1].Y + 1);
            gr.DrawLine(penBlue, val[2].X + 1, val[2].Y + 1, val[1].X, val[1].Y + 1);

            // поверх сами считаем
            int n = (int)DetailNum.Value;
            int x0, x1, y0, y1;
            float r1 = (int)Len2D(val[0], val[1]);
            float r2 = (int)Len2D(val[0], val[1]);
            float a = (int)(r1 + r2) / 2;
            float c = (int)Len2D(val[0], val[2]) / 2;
            float b = (int)Math.Sqrt(a*a + c*c);
            int centrY = (int)(val[2].Y + val[0].Y)/2;

            double step = Math.PI / n;
            x0 = 0;
            y0 = (int)b;
            for (int i = (int)(n * 0.5); i <= (int)(n * 1.5); i++)
            {
                x1 = (int)Math.Round(Math.Cos(i * step) * a);
                y1 = (int)Math.Round(Math.Sin(i * step) * b);
                gr.DrawLine(penGreen, val[0].X + x0, centrY + y0, val[0].X + x1, centrY + y1);
                x0 = x1; y0 = y1;
            }

            // опорные точки
            gr.DrawEllipse(penRed, new Rectangle(val[0], new Size(bord, bord)));
            gr.DrawEllipse(penBlueB, new Rectangle(val[1], new Size(bord, bord)));
            gr.DrawEllipse(penRed, new Rectangle(val[2], new Size(bord, bord)));

            // выделенный узел
            if (curPointB >= 0)
                gr.DrawRectangle(penRed, val[curPointB].X - 2, val[curPointB].Y - 2, 9, 9);
        }
        //_________________________________________________________________________________________________________________
        //_________________________________________________________________________________________________________________
        //_________________________________________________________________________________________________________________

        public MainForm()
        {
            InitializeComponent();
            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty).SetValue(PictureBox, true, null);

            IniPaint();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            TimeInfo.Text = "";
            MouseInfo.Text = "";
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            Graphics image = gr;
            Pen penBlack = new Pen(Color.Black, 1);
            Pen penWhite = new Pen(Color.White, 1);
            //if (mode != 0)
            //gr.Clear(Color.Black);

            int StartTime = Environment.TickCount;
            if (mode == 1)
            {
                int step = 5;

                //for (int k = 0; k < 100; k++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        int y = j * step;
                        for (int i = 0; i < 100; i++)
                        {
                            int x = i * step;
                            gr.DrawLine(penBlack, x, y, x + step, y + step);
                        }
                    }
                }
            }

            if (mode == 2)
            {
                //Vec2i t0[3] = { Vec2i(10, 70), Vec2i(50, 160), Vec2i(70, 80) };
                Point[] t0 = { new Point(10, 70), new Point(50, 160), new Point(70, 80) };
                Point[] t1 = { new Point(180, 50), new Point(150, 1), new Point(70, 180) };
                Point[] t2 = { new Point(180, 150), new Point(120, 160), new Point(130, 180) };

                //triangle(t1[0], t1[1], t1[2], image, Color.White);
                Polygons(t0, image, Color.Red);
                Polygons(t1, image, Color.White);
                Polygons(t2, image, Color.Green);
            }

            if (mode == 4)
            {
                Draw(Object_3D, gr);
            }

            int ResultTime = Environment.TickCount - StartTime;
            TimeInfo.Text = "Time  paint: " + ResultTime.ToString() + " ticks";
        }

        private void RightPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            Pen penBlack = new Pen(Color.Black, 2);
            Pen penRed = new Pen(Color.Red, 2);

            int w = RightPanel.Width;
            int h = RightPanel.Height;
            int border = 20;

            if (mode == 4)
            {
                if (modeB > 0)
                {
                    // ось вращения
                    gr.DrawLine(penBlack, w - border, border, w - border, h - border);
                    gr.DrawArc(penRed, (float)(w - border * 1.5), (float)(border * 1.5), border, border / 2, 300, 260);
                    gr.DrawPolygon(penRed, new PointF[] {
                        new PointF((float)(w - border * 1.5), (float)(border * 1.5)),
                        new PointF((float)(w - border * 1.2), (float)(border * 1.7)),
                        new PointF((float)(w - border * 1.2), (float)(border * 1.5))
                        }
                    );
                }

                if (modeB == 1)
                    DrawBezier(bezier, gr);
                if (modeB == 2)
                    DrawElips(elips, gr);
            }
        }

        //_________________________________________________________________________________________________________________
        //_________________________________________________________________________________________________________________
        //_________________________________________________________________________________________________________________

        private void ButtonLines_Click(object sender, EventArgs e)
        {
            mode = 1;
            PictureBox.Invalidate();
        }

        private void ButtonTriangl_Click(object sender, EventArgs e)
        {
            mode = 2;
            PictureBox.Invalidate();
        }

        private void ButtonLoadObj_Click(object sender, EventArgs e)
        {
            mode = 4;
            if (Object_3D != null)
                Object_3D.Clear();
            IniPaint();
            RightPanel.Invalidate();
            PictureBox.Invalidate();
        }

        private void ButtonCube_Click(object sender, EventArgs e)
        {
            mode = 4;
            if (Object_3D != null)
                Object_3D.Clear();

            // заполним
            int n = 100;
            Object_3D.Add(new List<Point3D>() {
                new Point3D( n,  n, -n),
                new Point3D(-n,  n, -n),
                new Point3D(-n, -n, -n),
                new Point3D( n, -n, -n)
            });
            Object_3D.Add(new List<Point3D>() {
                new Point3D( n, -n,  n),
                new Point3D(-n, -n,  n),
                new Point3D(-n, -n, -n),
                new Point3D( n, -n, -n)
            });
            Object_3D.Add(new List<Point3D>() {
                new Point3D( n,  n,  n),
                new Point3D(-n,  n,  n),
                new Point3D(-n,  n, -n),
                new Point3D( n,  n, -n)
            });
            Object_3D.Add(new List<Point3D>() {
                new Point3D( n,  n,  n),
                new Point3D(-n,  n,  n),
                new Point3D(-n, -n,  n),
                new Point3D( n, -n,  n)
            });

            minZ = -100;
            maxZ = 100;

            {   //вариант через Surface3D
                //Object_3D.Add(new Surface3D(new Point3D(n, n, n),
                //                            new Point3D(-n, n, n),
                //                            new Point3D(-n, -n, n),
                //                            new Point3D(n, -n, n)
                //                            ));
                //
                //Object_3D.Add(new Surface3D(new Point3D(n, n, -n),
                //                            new Point3D(-n, n, -n),
                //                            new Point3D(-n, -n, -n),
                //                            new Point3D(n, -n, -n)
                //                            ));
                //
                //Object_3D.Add(new Surface3D(new Point3D( n, -n,  n),
                //                            new Point3D(-n, -n,  n),
                //                            new Point3D(-n, -n, -n),
                //                            new Point3D( n, -n, -n)
                //                            ));
                //
                //Object_3D.Add(new Surface3D(new Point3D( n, n,  n),
                //                            new Point3D(-n, n,  n),
                //                            new Point3D(-n, n, -n),
                //                            new Point3D( n, n, -n)
                //                            ));
            }

            IniPaint();
            PictureBox.Invalidate();
        }

        private void ButtonPyramid_Click(object sender, EventArgs e)
        {
            mode = 4;
            modeB = 0;

            if (Object_3D != null)
                Object_3D.Clear();

            int n = (int)DetailNum.Value;
            double step = 2 * Math.PI / n;
            int r = 150;
            int h = 300;
            int x0 = 0;
            int z0 = r;
            int x1 = 0;
            int z1 = 0;

            List<Point3D> osnova = new List<Point3D>();
            for (int i = 1; i <= n; i++)
            {
                x1 = (int)Math.Round(Math.Sin(i * step) * r);
                z1 = (int)Math.Round(Math.Cos(i * step) * r);

                osnova.Add(new Point3D(x1, h / 2, z1));

                Object_3D.Add(new List<Point3D>() {
                    new Point3D(x0, h/2, z0),
                    new Point3D(0, -h/2, 0),
                    new Point3D(x1, h/2, z1)
                });
                x0 = x1;
                z0 = z1;
            }
            Object_3D.Add(osnova);

            minZ = -100;
            maxZ = 100;

            IniPaint();
            RightPanel.Invalidate();
            PictureBox.Invalidate();
        }

        private void BezierButton_Click(object sender, EventArgs e)
        {
            mode = 4;
            modeB = 1;
            if (Object_3D != null)
                Object_3D.Clear();

            int w = RightPanel.Width;
            int h = RightPanel.Height;
            int border = 20;

            bezier.Clear();
            int x = w - border - 2;
            int y = border * 5;
            bezier.Add(new Point(x, h / 2 - y));
            bezier.Add(new Point(x - y * 2, h / 2 - y));
            bezier.Add(new Point(x - y * 2, h / 2 + y));
            bezier.Add(new Point(x, h / 2 + y));

            TilingBezier(Object_3D, bezier);
            IniPaint();
            RightPanel.Invalidate();
            PictureBox.Invalidate();
        }

        private void ElipsButton_Click(object sender, EventArgs e)
        {
            mode = 4;
            modeB = 2;
            if (Object_3D != null)
                Object_3D.Clear();

            int w = RightPanel.Width;
            int h = RightPanel.Height;
            int border = 20;

            elips.Clear();
            int x = w - border - 2;
            int y = border * 5;
            elips.Add(new Point(x, h / 2 - y));
            elips.Add(new Point(w - y, h / 2));
            elips.Add(new Point(x, h / 2 + y));

            TilingElips(Object_3D, elips);
            IniPaint();
            RightPanel.Invalidate();
            PictureBox.Invalidate();
        }

        //_________________________________________________________________________________________________________________
        //_________________________________________________________________________________________________________________
        //_________________________________________________________________________________________________________________

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownLocation = e.Location;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    eventmouse = 2;
                    break;
                case MouseButtons.Right:
                    eventmouse = 1;
                    break;
                case MouseButtons.Middle:
                    eventmouse = 3;
                    break;
                case MouseButtons.None:
                default:
                    eventmouse = 0;
                    break;
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouseCurLocation = e.Location;
            int dx = mouseCurLocation.X - mouseDownLocation.X;
            int dy = mouseCurLocation.Y - mouseDownLocation.Y;

            if (eventmouse == 1)
            {
                angel_OX += (double)dx / 100;
                angel_OY += (double)dy / 100;
                PictureBox.Invalidate();
                mouseDownLocation = mouseCurLocation;
            }
            if (eventmouse == 2)
            {
                Point_0.X += dx;
                Point_0.Y += dy;
                PictureBox.Invalidate();
                mouseDownLocation = mouseCurLocation;
            }
            //MouseInfo.Text = "angel_OX: " + angel_OX.ToString() + " angel_OY: " + angel_OY.ToString();
        }

        private void PictureBox_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            scale += (double)e.Delta / 120 / 10;
            scale = Math.Max(scale, 0);
            PictureBox.Invalidate();
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            eventmouse = 0;
        }

        private void RightPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownLocation = e.Location;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    eventmouseB = 1;
                    break;
                default:
                    eventmouseB = 0;
                    break;
            }
        }

        private void RightPanel_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouseDownLocation = e.Location;
            int curNewPointB = 0;
            if (eventmouseB == 0)
            {
                if (modeB == 1)
                {
                    curNewPointB = FindPoint(bezier, mouseDownLocation);
                    if ((curPointB < 0) && (curNewPointB >= 0))
                        RightPanel.Invalidate();
                    if ((curPointB >= 0) && (curNewPointB < 0))
                        RightPanel.Invalidate();
                    curPointB = curNewPointB;
                }
                if (modeB == 2)
                {
                    curNewPointB = FindPoint(elips, mouseDownLocation);
                    if ((curPointB < 0) && (curNewPointB >= 0))
                        RightPanel.Invalidate();
                    if ((curPointB >= 0) && (curNewPointB < 0))
                        RightPanel.Invalidate();
                    curPointB = curNewPointB;
                }
            }
            if (eventmouseB == 1)
            {
                if (modeB == 1)
                    if (curPointB >= 0)
                        bezier[curPointB] = mouseDownLocation;
                if (modeB == 2)
                    if (curPointB == 0)
                        elips[curPointB] = new Point (elips[curPointB].X, mouseDownLocation.Y);
                    if (curPointB == 1)
                        elips[curPointB] = mouseDownLocation;
                    if (curPointB == 2)
                        elips[curPointB] = new Point (elips[curPointB].X, mouseDownLocation.Y);

                RightPanel.Invalidate();
            }
        }

        private void RightPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (curPointB >= 0)
            {
                if (modeB == 1)
                    TilingBezier(Object_3D, bezier);
                if (modeB == 2)
                    TilingElips(Object_3D, elips);
                PictureBox.Invalidate();
            }
            eventmouseB = 0;
        }

        private void TilingBezier(List<List<Point3D>> ob, List<Point> val)
        {
            if (val.Count != 4)
                return;
            int centrY = (int)(val[2].Y + val[0].Y) / 2;

            ob.Clear();
            minZ = 0; maxZ = 0;
            int n = (int)DetailNum.Value;
            double step = 2 * Math.PI / n;
            float[] r = new float[n + 1];
            float[] h = new float[n + 1];
            for (int tt = 0; tt <= n; tt++)
            {
                float t = (float)tt / n;
                float c0 = (1 - t) * (1 - t) * (1 - t);
                float c1 = (1 - t) * (1 - t) * 3 * t;
                float c2 = (1 - t) * t * 3 * t;
                float c3 = t * t * t;
                r[tt] = c0 * val[0].X + c1 * val[1].X + c2 * val[2].X + c3 * val[3].X - val[0].X;
                h[tt] = c0 * val[0].Y + c1 * val[1].Y + c2 * val[2].Y + c3 * val[3].Y - centrY;
            }

            int x0 = 0, x1 = 0, x2 = 0, x3 = 0, z0 = 0, z1 = 0, z2 = 0, z3 = 0;
            for (int t = 1; t <= n; t++)
            {
                z0 = (int)r[t - 1];
                z3 = (int)r[t];
                for (int i = 0; i <= n; i++)
                {
                    x1 = (int)Math.Round(Math.Sin(i * step) * r[t - 1]);
                    z1 = (int)Math.Round(Math.Cos(i * step) * r[t - 1]);
                    x2 = (int)Math.Round(Math.Sin(i * step) * r[t]);
                    z2 = (int)Math.Round(Math.Cos(i * step) * r[t]);

                    if (i > 0)
                        ob.Add(new List<Point3D>() {
                        new Point3D(x0, (int)h[t-1], z0),
                        new Point3D(x1, (int)h[t-1], z1),
                        new Point3D(x2, (int)h[t], z2),
                        new Point3D(x3, (int)h[t], z3)

                    });
                    x0 = x1; z0 = z1; x3 = x2; z3 = z2;
                    minZ = Math.Min(minZ, z2); maxZ = Math.Max(maxZ, z2);
                }
            }
        }

        private void TilingElips(List<List<Point3D>> ob, List<Point> val)
        {
            if (val.Count != 3)
                return;

            int centrY = (int)(val[2].Y + val[0].Y) / 2;
            ob.Clear();

            minZ = 0; maxZ = 0;
            int n = (int)DetailNum.Value;
            float[] r = new float[n + 1];
            float[] h = new float[n + 1];

            float r1 = (int)Len2D(val[0], val[1]);
            float r2 = (int)Len2D(val[0], val[1]);
            float a = (int)(r1 + r2) / 2;
            float c = (int)Len2D(val[0], val[2]) / 2;
            float b = (int)Math.Sqrt(a * a + c * c);

            double step = Math.PI / n;
            for (int tt = 0; tt <= n; tt++)
            {
                int i = (int)(tt + n * 0.5f);
                r[tt] = (int)Math.Round(Math.Cos(i * step) * a);
                h[tt] = (int)Math.Round(Math.Sin(i * step) * b);
                //gr.DrawLine(penGreen, val[0].X + x0, centrY + y0, val[0].X + x1, centrY + y1);
            }

            step = 2 * Math.PI / n;
            int nn = (int)(SectorNum.Value * n / 100);
            int x0 = 0, x1 = 0, x2 = 0, x3 = 0, z0 = 0, z1 = 0, z2 = 0, z3 = 0;
            for (int t = 1; t <= n; t++)
            {
                z0 = (int)r[t - 1];
                z3 = (int)r[t];
                for (int i = 0; i <= nn; i++)
                {
                    x1 = (int)Math.Round(Math.Sin(i * step) * r[t - 1]);
                    z1 = (int)Math.Round(Math.Cos(i * step) * r[t - 1]);
                    x2 = (int)Math.Round(Math.Sin(i * step) * r[t]);
                    z2 = (int)Math.Round(Math.Cos(i * step) * r[t]);

                    if (i > 0)
                        ob.Add(new List<Point3D>() {
                        new Point3D(x0, (int)h[t-1], z0),
                        new Point3D(x1, (int)h[t-1], z1),
                        new Point3D(x2, (int)h[t], z2),
                        new Point3D(x3, (int)h[t], z3)

                    });
                    x0 = x1; z0 = z1; x3 = x2; z3 = z2;
                    minZ = Math.Min(minZ, z2); maxZ = Math.Max(maxZ, z2);
                }
                //for (int i = nn; i <= n; i++)
                //{
                //    x1 = (int)Math.Round(Math.Sin(nn * step) * r[t - 1]);
                //    //z1 = (int)Math.Round(Math.Cos(nn * step) * r[t - 1]);
                //    x2 = (int)Math.Round(Math.Sin(nn * step) * r[t]);
                //    //z2 = (int)Math.Round(Math.Cos(nn * step) * r[t]);

                //    if (i > nn)
                //        ob.Add(new List<Point3D>() {
                //        new Point3D(x0, (int)h[t-1], z0),
                //        new Point3D(x1, (int)h[t-1], z1),
                //        new Point3D(x2, (int)h[t], z2),
                //        new Point3D(x3, (int)h[t], z3)
                //    });
                //    x0 = x1; z0 = z1; x3 = x2; z3 = z2;
                //    minZ = Math.Min(minZ, z2); maxZ = Math.Max(maxZ, z2);
                //}
            }
        }

        private void RightPanel_SizeChanged(object sender, EventArgs e)
        {
            RightPanel.Invalidate();
        }

        private int FindPoint(List<Point> val, Point p)
        {
            int e = 4;
            if (val.Count <= 0)
                return -1;

            // пербираем тайлы
            for (int i = 0; i < val.Count; i++)
            {
                Point p1 = val[i];

                if ((Math.Min(p1.X, p1.X) - e <= p.X && p.X <= Math.Max(p1.X, p1.X) + e) &&
                    (Math.Min(p1.Y, p1.Y) - e <= p.Y && p.Y <= Math.Max(p1.Y, p1.Y) + e))
                    return i;
            }
            return -1;
        }

        private void paintScelet_CheckedChanged(object sender, EventArgs e)
        {
            PictureBox.Invalidate();
        }

    }
}
