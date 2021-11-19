using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;

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
        bool fa = false;
        bool fm = false;

        //List<Surface3D> Object_3D = new List<Surface3D>();
        List<List<Point3D>> Object_3D = new List<List<Point3D>>();

        double scale;
        double angel_OX;// в градусах
        double angel_OY;
        Point Point_0 = new Point(0, 0); // камера
        List<Point> elips = new List<Point>(); // только опорные точки
        //_________________________________________________________________________________________________________________
        //_________________________________________________________________________________________________________________
        //_________________________________________________________________________________________________________________
        // начальные параметры отрисовки
        private void IniPaint()
        {
            Point_0 = new Point(0, 0);
            // установим углы
            angel_OX = 0;
            angel_OY = 120;
            // масштаб
            scale = 60;
        }
        private Point3Df vector(Point3D p1, Point3D p2)
        {
            return new Point3Df(p2.x - p1.x, p2.y - p1.y, p2.z - p1.z);
        }
        private Point3Df vectornoe(List<Point3D> s)
        {
            Point3Df a = vector(s[2], s[1]);
            Point3Df b = vector(s[2], s[0]);
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
        // передача в GL тайлов
        private void Draw(List<List<Point3D>> val)
        {
            // проверка наличия фигуры 3d
            if (val.Count <= 0)
                return;

            if (paintScelet.Checked)
                Gl.glBegin(Gl.GL_LINE_STRIP); // задание примитива
                //Gl.glBegin(Gl.GL_LINE_LOOP);
            else
                Gl.glBegin(Gl.GL_QUADS);

            // пербираем тайлы
            for (int i = 0; i < val.Count; i++)
            {
                List<Point3D> cursurf = val[i];
                Point3Df v = vectornoe(cursurf);
                Point3Df nv = normalize(v);

                //if (intensity >= 0)
                {
                    //Point[] t = new Point[cursurf.Count];
                    for (int j = 0; j < cursurf.Count; j++)
                    {
                        if (paintScelet.Checked)
                            Gl.glVertex3d(cursurf[j].x, cursurf[j].y, cursurf[j].z);
                        //t[j] = convert_3D_in_2D_Point(cursurf[j]);
                    }
                    if (paintScelet.Checked)
                        Gl.glVertex3d(cursurf[0].x, cursurf[0].y, cursurf[0].z);

                    if (!paintScelet.Checked)
                    {
                        Gl.glNormal3d(-nv.x, -nv.y, -nv.z);

                        Gl.glVertex3d(cursurf[0].x, cursurf[0].y, cursurf[0].z);
                        Gl.glVertex3d(cursurf[1].x, cursurf[1].y, cursurf[1].z);
                        Gl.glVertex3d(cursurf[2].x, cursurf[2].y, cursurf[2].z);
                        Gl.glVertex3d(cursurf[3].x, cursurf[3].y, cursurf[3].z);
                    }
                }
            }
            Gl.glEnd();
        }
        // Инвалидация области построения
        private void Draw()
        {
            // очистка буфера цвета и буфера глубины 
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(255, 255, 255, 1);
            // очищение текущей матрицы 
            Gl.glLoadIdentity();
            // установка порта вывода в соответствии с размерами элемента anT 
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            //Gl.glScale();

            // наблюдатель будет  отдалятся или приближатся к объекту наблюдения
            Gl.glTranslated(Point_0.X, Point_0.Y, - scale * 10);
            // 2 поворота (углы angel_OX и angel_OY)
            Gl.glRotated(angel_OY, 1, 0, 0);
            Gl.glRotated(angel_OX, 0, 1, 0);

            Draw(Object_3D);

            // возвращаем сохраненную матрицу
            Gl.glPopMatrix();

            // прорисовка
            Gl.glFlush();

            // обновляем элемент AnT
            AnT.Invalidate();
        }
        private float Len2D(Point A, Point B)
        {
            int x = A.X - B.X;
            int y = A.Y - B.Y;
            return (float)Math.Sqrt(x * x + y * y);
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
            float b = (int)Math.Sqrt(a * a + c * c);
            int centrY = (int)(val[2].Y + val[0].Y) / 2;

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
            AnT.InitializeContexts();
            IniPaint();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            TimeInfo.Text = "";
            MouseInfo.Text = "";

            // инициализация Glut 
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            // отчитка окна 
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода в соответствии с размерами элемента anT 
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // настройка проекции 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45, 1.5, 0.1, 1000);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            // настройка параметров OpenGL для визуализации 
            //Gl.glEnable(Gl.GL_DEPTH_TEST);
            //Gl.glEnable(Gl.GL_LIGHTING);
            //Gl.glEnable(Gl.GL_LIGHT0);

            int hl = 400;
            // Модель освещенности с одним источником цвета
            float[] light_position = { hl, -hl, hl, 0 }; // Координаты источника света
            float[] lghtClr = { 1, 1, 1, 0 }; // Источник излучает белый цвет
            //float[] mtClr = { 0, 0.7f, 0, 1 }; // Материал зеленого цвета
            float[] mtClr = { 0, 0.7f, 0, 1 }; // Материал зеленого цвета
            Gl.glPolygonMode(Gl.GL_FRONT, Gl.GL_FILL); // Заливка полигонов
            Gl.glShadeModel(Gl.GL_SMOOTH); // Вывод с интерполяцией цветов
            Gl.glEnable(Gl.GL_LIGHTING); // Будем рассчитывать освещенность
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light_position);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, lghtClr); // Рассеивание
            Gl.glEnable(Gl.GL_LIGHT0); // Включаем в уравнение освещенности источник GL_LIGHT0
            // Диффузионная компонента цвета материала
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_DIFFUSE, mtClr);
            //GL_FRONT GL_FRONT_AND_BACK
            //GL_AMBIENT GL_DIFFUSE GL_SPECULAR GL_EMISSION GL_AMBIENT_AND_DIFFUSE

            //Gl.glTranslated(0.7f, 0.3f, 0.7f);

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

                //if (modeB == 1)
                //    DrawBezier(bezier, gr);
                if (modeB == 2)
                    DrawElips(elips, gr);
            }
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
                angel_OX += (double)dx ;
                angel_OY += (double)dy;
                Draw();
                mouseDownLocation = mouseCurLocation;
            }
            if (eventmouse == 2)
            {
                Point_0.X += dx;
                Point_0.Y += dy;
                Draw();
                mouseDownLocation = mouseCurLocation;
            }
            //MouseInfo.Text = "angel_OX: " + angel_OX.ToString() + " angel_OY: " + angel_OY.ToString();
        }
        private void PictureBox_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            scale += (double)e.Delta / 12;
            scale = Math.Max(scale, 0);
            Draw();
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
                //if (modeB == 1)
                //{
                //    curNewPointB = FindPoint(bezier, mouseDownLocation);
                //    if ((curPointB < 0) && (curNewPointB >= 0))
                //        RightPanel.Invalidate();
                //    if ((curPointB >= 0) && (curNewPointB < 0))
                //        RightPanel.Invalidate();
                //    curPointB = curNewPointB;
                //}
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
                //if (modeB == 1)
                //    if (curPointB >= 0)
                //        bezier[curPointB] = mouseDownLocation;
                if (modeB == 2)
                    if (curPointB == 0)
                        elips[curPointB] = new Point(elips[curPointB].X, mouseDownLocation.Y);
                if (curPointB == 1)
                    elips[curPointB] = mouseDownLocation;
                if (curPointB == 2)
                    elips[curPointB] = new Point(elips[curPointB].X, mouseDownLocation.Y);

                RightPanel.Invalidate();
            }
        }
        private void RightPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (curPointB >= 0)
            {
                //if (modeB == 1)
                //    TilingBezier(Object_3D, bezier);
                if (modeB == 2)
                    TilingElips(Object_3D, elips);
                Draw();
            }
            eventmouseB = 0;
        }
        private void RightPanel_SizeChanged(object sender, EventArgs e)
        {
            RightPanel.Invalidate();
        }
        private void paintScelet_CheckedChanged(object sender, EventArgs e)
        {
            Draw();
        }
        //_________________________________________________________________________________________________________________
        //_________________________________________________________________________________________________________________
        //_________________________________________________________________________________________________________________
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

            IniPaint();
            TilingElips(Object_3D, elips);
            RightPanel.Invalidate();
            Draw();
        }
        private void TilingElips(List<List<Point3D>> ob, List<Point> val)
        {
            if (val.Count != 3)
                return;

            int centrY = (int)(val[2].Y + val[0].Y) / 2;
            ob.Clear();

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
            }

            step = 2 * Math.PI / n;
            int nn = (int)(SectorNum.Value * n / 100);
            int x0 = 0, x1 = 0, x2 = 0, x3 = 0, z0 = 0, z1 = 0, z2 = 0, z3 = 0;
            //Gl.glBegin(Gl.GL_QUADS);
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

                    if (i > 0) {
                        ob.Add(new List<Point3D>() {
                        new Point3D(x0, (int)h[t-1], z0),
                        new Point3D(x1, (int)h[t-1], z1),
                        new Point3D(x2, (int)h[t], z2),
                        new Point3D(x3, (int)h[t], z3)
                        });
                    }
                    x0 = x1; z0 = z1; x3 = x2; z3 = z2;
                }

            }
            //Draw(ob);
            //Gl.glEnd();
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

        private void button1_Click(object sender, EventArgs e)  {
            if (!fa) {
                fa = true;
                timerOfAnima.Enabled = true;
                return;
            }
            timerOfAnima.Enabled = false;
            fa = false;
            //Point_0 = new Point(0, 0);
        }

        private void timerOfAnima_Tick(object sender, EventArgs e)
        {
            angel_OX += (double)1;
            //angel_OY += (double)15;
            Draw();
        }

        private void button2_Click(object sender, EventArgs e)  {
            if (!fm) {
                Point_0 = new Point(-200, 0);
                fm = true;
                timerOfMove.Enabled = true;
                return;
            }
            timerOfMove.Enabled = false;
            fm = false;
            Point_0 = new Point(0, 0);
            Draw();
        }

        private void timerOfMove_Tick(object sender, EventArgs e) {
            Point_0 = Point_0 + new Size(1, 0);
            Draw();
        }
    }
}
