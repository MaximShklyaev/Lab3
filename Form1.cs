using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using MathNet.Numerics.RootFinding;
using MathNet.Numerics.Integration;
using MathNet.Numerics;
using System.Numerics;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {

        double ortho = 10;
        public Form1()
        {
            InitializeComponent();
            glControl1.MouseDown += new MouseEventHandler(mouseDown);
            glControl1.MouseMove += new MouseEventHandler(mouseMove);
            glControl1.MouseUp += new MouseEventHandler(mouseUp);
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            I_point = -1;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            double VpX = (double)20 / (double)glControl1.Width;
            double VpY = (double)20 / (double)glControl1.Height;
            if (I_point != -1)
            {
                points[I_point, 0] = -10 + VpX * (Cursor.Position.X - glControl1.Location.X - Location.X - 8);
                points[I_point, 1] = 10 - VpY * (Cursor.Position.Y - glControl1.Location.Y - Location.Y - Cursor.Size.Height);
                if (e.Button == MouseButtons.Left)
                {
                    label4.Text = e.Location.X.ToString() + "  " + e.Location.Y;
                }
            }

        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            double VpX = (double)20 / (double)glControl1.Width;
            double VpY = (double)20 / (double)glControl1.Height;
            double x = -10 + VpX * (Cursor.Position.X - glControl1.Location.X - Location.X - 8);
            double y = 10 - VpY * (Cursor.Position.Y - glControl1.Location.Y - Location.Y - Cursor.Size.Height);

            
            if (e.Button == MouseButtons.Left)
            {
                double X_p = ortho*(e.Location.X - (double)glControl1.Width / 2) / ((double)glControl1.Width / 2);
                double Y_p = ortho * ((double)glControl1.Height- e.Location.Y - (double)glControl1.Height / 2) / ((double)glControl1.Height / 2);
                label8.Text = X_p.ToString() + "  " + Y_p.ToString();
                points1.Add(new double[] { X_p, Y_p });
               
            }

            //I_point = Find_Point(x, y);
            
        }
        int I_point = -1;

        private int Find_Point(double Xpos, double Ypos)
        {
            double VpX = (double)20 / (double)glControl1.Width;
            double VpY = (double)20 / (double)glControl1.Height;
            int k = points.Length / points.Rank;
            int r = 5;
            for (int i = 0; i < k; i++)
            {
                if (Xpos <= (points[i, 0] + r * VpX) && Xpos >= (points[i, 0] - r * VpX) && Ypos >= (points[i, 1] - r * VpY) && Ypos <= (points[i, 1] + r * VpY)) return i;
            }
            return -1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-ortho, ortho, -ortho, ortho, -1,1);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            
        }
        private void Draw_Triangle()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex2(0, 1);
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex2(1, -1);
            GL.Vertex2(-1, -1);
            GL.End();
            glControl1.SwapBuffers();
            GL.Color3(1.0f, 0.0f, 0.0f);
        }

        private void Draw_Flag()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex2(0, 0);
            GL.Vertex2(3, 0);
            GL.Vertex2(1, 2);
            GL.Color3(1.0f, 1.0f, 0.0f);
            GL.Vertex2(3, 4);
            GL.Vertex2(0, 4);
            GL.Vertex2(0, 0);
            GL.End();

            GL.PointSize(5.0f);
            GL.Begin(PrimitiveType.Points);
            GL.Color3(0.0f, 0.0f, 0.0f);
            GL.Vertex2(0, 0);
            GL.Vertex2(3, 0);
            GL.Vertex2(1, 2);
            GL.Vertex2(3, 4);
            GL.Vertex2(0, 4);
            GL.Vertex2(0, 0);
            GL.End();
            glControl1.SwapBuffers();
            GL.Rotate(50, 0, 0, 1);
        }
        private void Draw_()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex2(0, 1);
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex2(1, -1);
            GL.Vertex2(-1, -1);
            GL.End();
            glControl1.SwapBuffers();
        }
        private void Draw_Pentagram()
        {
            double R = 5;
            double r = R * (Math.Sin(Math.PI / 10) / Math.Sin(3 * Math.PI / 10));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            for (int i = 0; i < 5; i++)
            {
                GL.Begin(PrimitiveType.Triangles);
                GL.Color3(1.0f, 0.0f, 0.0f);
                GL.Vertex2(R * Math.Cos(Math.PI / 2 + i * 2 * Math.PI/5), R * Math.Sin(Math.PI / 2 + i * 2 * Math.PI / 5));
                GL.Vertex2(r * Math.Cos(3 * Math.PI / 10 + i * 2 * Math.PI / 5), r * Math.Sin(3 * Math.PI / 10 + i * 2 * Math.PI / 5));
                GL.Vertex2(r * Math.Cos(7 * Math.PI / 10 + i * 2 * Math.PI / 5), r * Math.Sin(7 * Math.PI / 10 + i * 2 * Math.PI / 5));
                GL.End();
            }
            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(0.0f, 1.0f, 0.0f);
            for (int i = 0; i < 5; i++)
            {
                GL.Vertex2(r * Math.Cos(3 * Math.PI / 10 + i * 2 * Math.PI / 5), r * Math.Sin(3 * Math.PI / 10 + i * 2 * Math.PI / 5));
            }
            GL.End();
            glControl1.SwapBuffers();
        }

        int i = 0;
        int j = 0;
        static double X = 1.5;
        static double Y = 2;
        static double tan = X / Y;
        static double Hv = 1.5;
        static double Av = Hv*tan;
        static double S = Hv * Av;
        

        private void Draw_Sand_Watch()
        {
            double Vp = (double)20/(double)glControl1.Height;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.LineWidth(5);
            GL.Color3(0.0f, 0.0f, 0.0f);
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(X, Y);
            GL.Vertex2(-X, Y);
            GL.Vertex2(X, -Y);
            GL.Vertex2(-X, -Y);
            GL.Vertex2(X, Y);
            GL.End();

            double h = Hv - Vp * i;
            double a = (Hv - Vp * i) * tan;
            double Sn = S - (Hv - Vp * i) * (Hv - Vp * i) * tan;
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex2(a, h);
            GL.Vertex2(0, 0);
            GL.Vertex2(-a, h);
            GL.End();

            double Hn = Math.Sqrt(Sn / tan);
            double An = Math.Sqrt(Sn * tan);
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex2(An, -Y);
            GL.Vertex2(0, -Y + Hn);
            GL.Vertex2(-An, -Y);
            GL.End();

            if (h <= 0)
            {
                GL.Rotate(4, 0, 0, 1);
                if( j != 45)
                {
                    j++;
                }
                else
                {
                    i = 0;
                    j = 0;
                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadIdentity();
                    GL.Ortho(-10, 10, -10, 10, -1, 1);
                    GL.MatrixMode(MatrixMode.Modelview);
                    GL.LoadIdentity();
                    day = !day;
                }
            }
            else
            {
                Change_Time();
                i++;
            }
            glControl1.SwapBuffers();
        }
        bool day = true;
        private void Change_Time()
        {
            if(day)
            {
                double r = 1.5;
                double x = 5;
                double y = 5;
                GL.Begin(PrimitiveType.Polygon);
                GL.Color3(1.0f, 1.0f, 0.0f);
                for (int i = 0; i < 30; i++)
                {
                    GL.Vertex2(r * Math.Cos(3 * Math.PI / 10 + i * 2 * Math.PI / 30) + x, r * Math.Sin(3 * Math.PI / 10 + i * 2 * Math.PI / 30) + y);
                }
                GL.End();
            }
            else 
            {
                double r = 0.5;
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(1.0f, 1.0f, 0.0f);
                for (int i = 0; i < 6; i++)
                {
                    double x = 7 * Math.Cos(Math.PI / 4 + i * Math.PI / 10);
                    double y = 7 * Math.Sin(Math.PI / 4 + i * Math.PI / 10);
                    GL.Vertex2(r * Math.Cos(0 ) + x, r * Math.Sin(0) + y);
                    GL.Vertex2(r * Math.Cos(0 + 2 * Math.PI / 2) + x, r * Math.Sin(0 + 2 * Math.PI / 2) + y);
                    GL.Vertex2(r * Math.Cos(Math.PI / 2) + x, r * Math.Sin( Math.PI / 2) + y);
                    GL.Vertex2(r * Math.Cos(Math.PI / 2 + 2 * Math.PI / 2) + x, r * Math.Sin(Math.PI / 2 + 2 * Math.PI / 2) + y);
                    GL.Vertex2(r * Math.Cos(Math.PI / 4) + x, r * Math.Sin(Math.PI / 4) + y);
                    GL.Vertex2(r * Math.Cos(Math.PI / 4 + 2 * Math.PI / 2) + x, r * Math.Sin(Math.PI / 4 + 2 * Math.PI / 2) + y);
                    GL.Vertex2(r * Math.Cos(3*Math.PI / 4) + x, r * Math.Sin(3*Math.PI / 4) + y);
                    GL.Vertex2(r * Math.Cos(3*Math.PI / 4 + 2 * Math.PI / 2) + x, r * Math.Sin(3*Math.PI / 4 + 2 * Math.PI / 2) + y);
                }
                GL.End();
            }
        }
        private double f1(double t)
        {
            return Math.Cos(t);
        }
        private double f2(double t)
        {
            return Math.Sin(t);
        }
        private double foo2(double t, double r)
        {
            return Math.Sqrt(r*r -(t)*(t));
        }
        private double foo1(double t)
        {
            return t;
        }

        //private void Draw_Circle(double r)
        //{
        //    double t_min = 0;
        //    double t_max = 2 * Math.PI;
        //    int grid = trackBar1.Value;
        //    double h = (t_max-t_min) / (double)grid;
        //    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        //    GL.Begin(PrimitiveType.LineStrip);
        //    GL.Color3(1.0f, 0.0f, 0.0f);
        //    for (int i = 0; i < grid; i++)
        //    {
        //        GL.Vertex2(f1(t_min + i * h), f2(t_min + i * h, r));
        //    }
        //    GL.End();
        //    glControl1.SwapBuffers();
        //}

        private void Draw_Circle2(double r)
        {
            double t_min = 0;
            double t_max = 2 * Math.PI;
            int grid = trackBar1.Value;
            double h = (t_max - t_min) / (double)grid;
            double h1 = 2 * r / (double)grid;
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(1.0f, 0.0f, 0.0f);
            for (int i = 0; i < grid; i++)
            {
                GL.Vertex2(r*f1(t_min + i * h), r*f2(t_min + i * h));
            }
            GL.End();
            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(0.0f, 0.0f, 0.0f);
            for (int i = 0; i < grid; i++)
            {
                GL.Vertex2(foo1(-r + i * h1), foo2(-r + i * h1, r));
            }
            GL.End();
            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(0.0f, 0.0f, 0.0f);
            for (int i = 0; i < grid; i++)
            {
                GL.Vertex2(foo1(-r + i * h1), -foo2(-r + i * h1, r));
            }
            GL.End();
            glControl1.SwapBuffers();
        }
        int dzeta = 2;
        private void Draw_Circle3(double r)
        {
            double t_min = 0;
            double t_max = 2 * Math.PI;
            int grid = trackBar1.Value;
            double h = (t_max - t_min) / (double)grid;
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(1.0f, 0.0f, 0.0f);
            for (int i = 0; i < grid; i++)
            {
                GL.Vertex2(r * f1(t_min + i * h), r * f2(t_min + i * h));
            }
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(0.0f, 0.0f, 0.0f);
            for (int i = 0; i < grid; i++)
            {
                int i1 = (dzeta * i) % grid;
                GL.Vertex2(r * f1(t_min + i * h), r * f2(t_min + i * h));
                GL.Vertex2(r * f1(t_min + i1 * h), r * f2(t_min + i1 * h));
            }
            GL.End();
            glControl1.SwapBuffers();
        }

        private static int Factorial(int number)
        {
            int n = 1;

            for (int i = 1; i < number; i++)
            {
                n *= i;
            }

            return n;
        }

        private static int C(int n, int m)
        {          
            return Factorial(n)/(Factorial(m)* Factorial(n-m));
        }
        private void Draw_b(double [,] P)
        {
            int k = P.Length/P.Rank;
            double t_min = 0;
            double t_max = 1;
            double t1_max = Math.PI;
            int grid = trackBar1.Value;
            double h = (t_max - t_min) / (double)grid;
            double h1 = (t1_max - t_min) / (double)grid;
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(0.0f, 0.0f, 0.0f);
            for (int i = 0; i <= grid; i++)
            {
                double t = t_min + i * h;
                double x = 0;
                double y = 0;
                for (int j = 0; j < k; j++)
                {
                    x = x + C(k - 1, j) * Math.Pow((1 - t), (k - 1 - j)) * Math.Pow((t), (j)) * P[j,0];
                    y = y + C(k - 1, j) * Math.Pow((1 - t), (k - 1 - j)) * Math.Pow((t), (j)) * P[j, 1];
                }
                GL.Vertex2(x, y);
            }           
            GL.End();

            GL.PointSize(5);
            GL.Begin(PrimitiveType.Points);
            for (int j = 0; j < k; j++)
            {
                GL.Vertex2(P[j,0], P[j, 1]);
            }

            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(0.0f, 0.0f, 0.0f);
            GL.Vertex2(points[0,0], points[0,1]);
            GL.Vertex2(points[1, 0], points[1, 1]);
            GL.Vertex2(points[k-2, 0], points[k-2, 1]);
            GL.Vertex2(points[k-1, 0], points[k-1, 1]);
            GL.End();
            int r = 3;
            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(1.0f, 0.0f, 0.0f);
            for (int i = 0; i <= grid; i++)
            {
                GL.Vertex2(r * f1(t_min + i * h1), r * f2(t_min + i * h1));
            }
            GL.End();
            glControl1.SwapBuffers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Draw_Pentagram();
            timer1.Start();
        }

        double[,] points = new double[5,2] { { -3, 0 }, { -3, 1 }, { -2, 4 }, { 1, 2 },{ 3, 0 }};

        private double r1_x(double t)
        {
            return  4*(Math.Cos(t) + Math.Sin(t));
        }
        private double r1_y(double t)
        {
            return  3*Math.Sin(t);
        }
        private double r2_x(double t)
        {
            return 1.5 * Math.Cos(2*t);
        }
        private double r2_y(double t)
        {
            return 2 * (Math.Sin(2*t) - 3*Math.Cos(t));
        }

        private double r3_x(double t)
        {
            return 0.5 *Math.Cos(3*t);
        }
        private double r3_y(double t)
        {
            return Math.Sin(3*t);
        }

        int ch = 0;
        double delta_t = 0.01;
        List<double[]> P_end = new List<double[]>();

        private void Draw_f()
        {
            double t_min = 0;
            double x1 = r1_x(t_min + ch * delta_t);
            double y1 = r1_y(t_min + ch * delta_t);
            double x2 = r2_x(t_min + ch * delta_t);
            double y2 = r2_y(t_min + ch * delta_t);
            double x3 = r3_x(t_min + ch * delta_t);
            double y3 = r3_y(t_min + ch * delta_t);

            double[] tmp = new double[2];
            tmp[0] = x1 + x2 + x3;
            tmp[1] = y1 + y2 + y3;
            P_end.Add(tmp);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(0.0f, 0.0f, 0.0f);
            GL.Vertex2(0, 0);
            GL.Vertex2(x1, y1);
            GL.Vertex2(x1, y1);
            GL.Vertex2(x1 + x2, y1 + y2);
            GL.Vertex2(x1 + x2, y1 + y2);
            GL.Vertex2(x1 + x2 + x3, y1 + y2 + y3);
            GL.End();
            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(1.0f, 0.0f, 0.0f);
            for (int i = 0; i < P_end.Count; i++)
            {
                GL.Vertex2(P_end[i][0], P_end[i][1]);
            }
            GL.End();
            glControl1.SwapBuffers();
            ch++;
        }
        double a11 = 1;
        double a12 = 1;
        private double f_dif1(double x, double y)
        {
            return (x - (x * y) / (1 + a11 * x ) - a12 * x * x);
            //return (a11+ a12 * y)*x;
            //return  a11 * x + a12* y;

        }
        double a21 = 1;
        double a22 = 1;
        private double f_dif2(double x, double y)
        {
            return (-y + (x * y) / (1 + a11 * x ) - a22 * y * y);
            //return (a21 + a22*x)*y;
            //return a21 * x + a22 * y;
        }

        private double f_dif3(double t, double C1, double C2)
        {
            return C1 * Math.Cos(t) + C2 * Math.Sin(t);
        }

        private double f_dif4(double t, double C1, double C2)
        {
            return C1 * Math.Sin(t) + C2 * Math.Cos(t);
        }

        const int n_uzlov = 6;

        double[,] points0 = new double[,] {  };
        List<double[]> points1 = new List<double[]>();
        double[,] K = new double[7, 3] { { 0, 0 , 1}, { 0, 1, 1 }, { 0, 2, 1 }, { 0, 3, 1 }, { 0, 4, 1 }, { 0, 5, 1 }, { 0, 6, 1 } };
        List<double[,]> list_points0 = new List<double[,]>();
        double x0_global = 0;
        double y0_global = 0;

        private void fill_list_points0( int n)
        {

        }

        private void Draw_difur_system(List<double[]> P)
        {
            double t_min = 0;
            double t_max = trackBar2.Value;
            int N = trackBar1.Value;
            label2.Text = "T";
            //label3.Text = "N";
            label4.Text = N.ToString();
            double h = (t_max - t_min) / (double)N;

            double[,] r_p = resting_points(a11, a12, a22);
            type_resting_points(a11, a12, a22);
            String str = "";
            str += r_p[0, 0].ToString() + "  " + r_p[0, 1].ToString() + "\n";
            str += r_p[1, 0].ToString() + "  " + r_p[1, 1].ToString() + "\n";
            str += r_p[2, 0].ToString() + "  " + r_p[2, 1].ToString();
            label3.Text = str;


            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(0.0f, 0.0f, 0.0f);
            GL.Vertex2(0, -ortho);
            GL.Vertex2(0, ortho);
            GL.Vertex2(-ortho, 0);
            GL.Vertex2(ortho, 0);
            GL.End();

            
            GL.PointSize(5);
            GL.Begin(PrimitiveType.Points);
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex2(r_p[0, 0], r_p[0, 1]);
            GL.Vertex2(r_p[1, 0], r_p[1, 1]);
            GL.Vertex2(r_p[2, 0], r_p[2, 1]);
            GL.End();

            for (int j = 0; j < (P.Count); j++)
            {
                double x0 = P[j][ 0];
                double y0 = P[j][1];
                GL.Begin(PrimitiveType.LineStrip);
                GL.Color3(0.0f, 0.0f, 0.0f);
                GL.Vertex2(x0, y0);
                for (int i = 0; i <= N; i++)
                {
                    double x1 = x0 + h * f_dif1(x0, y0);
                    double y1 = y0 + h * f_dif2(x0, y0);
                    GL.Vertex2(x1, y1);
                    x0 = x1;
                    y0 = y1;
                }
                GL.End();
                x0 = P[j][ 0];
                y0 = P[j][ 1];
                GL.Begin(PrimitiveType.LineStrip);
                GL.Color3(0.0f, 0.0f, 0.0f);
                GL.Vertex2(x0, y0);
                for (int i = 0; i <= N; i++)
                {
                    double x1 = x0 - h * f_dif1(x0, y0);
                    double y1 = y0 - h * f_dif2(x0, y0);
                    GL.Vertex2(x1, y1);
                    x0 = x1;
                    y0 = y1;
                }
                GL.End();


            }
            glControl1.SwapBuffers();
        }

        private double[,] resting_points(double a, double e, double b)
        {
            double[,] r_p = { {0,0 },{ 0, 0}, {0,0} };
            double k1 = -(1 + b) / (b * a * a * e);
            double k2 = -(2 * a * b - e * b - 1 + a) / (b * a * a * e);
            double k3 = -b * (a * a - 2 * e * a) / (b * a * a * e);
            var roots = Cubic.RealRoots(k1, k2, k3);
            double root1 = roots.Item1;
            double root2 = roots.Item2;
            double root3 = roots.Item3;
            if (!double.IsNaN(root1))
            {
                r_p[0, 0] = root1;
                r_p[0, 1] = (1+a* root1)*(1-e* root1);
            }
            if (!double.IsNaN(root2))
            {
                r_p[1, 0] = root2;
                r_p[1, 1] = (1 + a * root2) * (1 - e * root2);
            }
            if (!double.IsNaN(root3))
            {
                r_p[2, 0] = root3;
                r_p[2, 1] = (1 + a * root3) * (1 - e * root3);
            }
            return r_p;
        }
        private void type_resting_points(double a, double e, double b)
        {
            double[,] r_p = resting_points(a, e, b);
            String tmp = "";
            for(int i = 0; i < 3; i++)
            {
                double k_a = 1- 2*e*r_p[i, 0]-(r_p[i,1]*(1+a* r_p[i, 1]) - r_p[i, 1]* r_p[i, 0]*a)/((1+ a * r_p[i, 0]) *(1 + a * r_p[i, 0]));
                double k_b = -r_p[i, 0] / (1 + a * r_p[i, 0]);
                double k_c = (r_p[i, 1] * (1 + a * r_p[i, 0]) - r_p[i, 0] * r_p[i, 1] * a) / ((1 + a * r_p[i, 0]) * (1 + a * r_p[i, 0]));
                double k_d = -1 - 2 * b * r_p[i, 1] + r_p[i, 0] / (1 + a * r_p[i, 0]);
                var roots = FindRoots.Quadratic(k_a * k_d - k_b * k_c, k_d + k_a, 1);
                Complex root1 = roots.Item1;
                Complex root2 = roots.Item2;
                tmp += root1.ToString() + " " + root2.ToString() + "\n";
            }
            label5.Text = tmp;
        }

        private void Draw_vektor_field(double[,] P, double [,] K)
        {
            
            int size = P.Length / P.Rank;
            label3.Text = size.ToString();
            double[,] vektors = new double[size, 3];
            for (int i = 0; i < size; i++)
            {
                vektors[i, 0] = f_dif1(P[i, 0], P[i, 1]);
                vektors[i, 1] = f_dif2(P[i, 0], P[i, 1]);
                vektors[i, 2] = Math.Sqrt(vektors[i, 0] * vektors[i, 0] + vektors[i, 1] * vektors[i, 1]);
            }
            double min = vektors[0, 2];
            double max = vektors[0, 2];
            for (int i = 1; i < size; i++)
            {
                if (vektors[i, 2] > max)
                {
                    max = vektors[i, 2];
                }
                if (vektors[i, 2] < min)
                {
                    min = vektors[i, 2];
                }
            }
            label5.Text = min.ToString();
            label8.Text = max.ToString();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);           
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(0.0f, 0.0f, 0.0f);
            GL.Vertex2(0, -10);
            GL.Vertex2(0, 10);
            GL.Vertex2(-10, 0);
            GL.Vertex2(10, 0);            
            GL.End();
            GL.LineWidth(4);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex2(9.5, 8);
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex2(9.5, 9.5);
            GL.End();
            GL.LineWidth(1);
            GL.Color3(0.8f, 0.8f, 0.8f);
            for (int i = 1; i < 10; i++)
            {
                GL.Begin(PrimitiveType.Lines);
                GL.Vertex2(-10, i);
                GL.Vertex2(10, i);
                GL.Vertex2(-10, -i);
                GL.Vertex2(10, -i);
                GL.Vertex2(-i, 10);
                GL.Vertex2(-i, -10);
                GL.Vertex2(i, 10);
                GL.Vertex2(i, -10);
            }

            for (int j = 0; j < size; j++)
            {
                double x0 = P[j, 0];
                double y0 = P[j, 1];
                double x1 = P[j, 0] + (double)vektors[j, 0] / vektors[j, 2];
                double y1 = P[j, 1] + (double)vektors[j, 1] / vektors[j, 2];
                double color = (vektors[j, 2] - min) / (max - min);
                GL.Color3(0.0f, 0.0f, 0.0f);
                GL.End();
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(0.0f*(1-color) + 1.0f*color, 1.0f * (1 - color) + 0.0f * color, 0.0f * (1 - color) + 0.0f * color);
                GL.Vertex2(x0, y0);
                GL.Vertex2(x1, y1);
                GL.End();

            }
            double t_min = 0;
            double t_max = trackBar2.Value;
            int N = trackBar1.Value;
            label2.Text = "T";
            label3.Text = "N";
            label4.Text = N.ToString();
            double h = (t_max - t_min) / (double)N;
            GL.Color3(0.0f, 0.0f, 0.0f);
            GL.PointSize(1);
            GL.Begin(PrimitiveType.Points);
            for (int j = 0; j < K.Length/3; j++)
            {
                double x1 = K[j, 0] + h  * K[j, 2] * f_dif1(K[j,0], K[j, 1]);
                double y1 = K[j, 1] + h  * K[j, 2] * f_dif2(K[j, 0], K[j, 1]);
                GL.Vertex2(x1, y1);
                K[j, 0] = x1;
                K[j, 1] = y1;
            }
            GL.End();
            glControl1.SwapBuffers();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            a11 = Convert.ToDouble(textBox1.Text);
            a12 = Convert.ToDouble(textBox2.Text);
            a21 = Convert.ToDouble(textBox3.Text);
            a22 = Convert.ToDouble(textBox4.Text);
        }

        private void read_from_trackbar()
        {
            a11 = trackBar3.Value / (double)100;
            a12 = trackBar4.Value / (double)100;
            a21 = trackBar5.Value / (double)100;
            a22 = trackBar6.Value / (double)1000;
            label6.Text = a11.ToString();
            label7.Text = a12.ToString();
            label10.Text = a22.ToString();
            ortho = trackBar7.Value;
            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-ortho, ortho, -ortho, ortho, -1, 1);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Draw_Sand_Watch();

            //double r = trackBar2.Value;
            //label3.Text = trackBar1.Value.ToString();
            dzeta = trackBar3.Value;
            //Draw_Circle3(r);
            //Draw_b(points);
            //Draw_f();
            read_from_trackbar();
            Draw_difur_system(points1);
            //Draw_vektor_field(points0, K);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            points1.Clear();
            int k = 0;
            int k1 = 0;
            int N = 200;
            Random rnd = new Random();
            points0 = new double[19*19, 2];
            K = new double[N * N, 3];
            for (int i = -9; i<=9; i++)
                for (int j = -9; j <= 9; j++)
                {
                    
                    points0[k, 0] = i;
                    points0[k, 1] = j;
                    k++;
                }
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    K[k1, 0] = -10 +i*((double)20 /N);
                    K[k1, 1] = -10 + j * ((double)20 / N);
                    K[k1, 2] = rnd.NextDouble();
                    //K[k1, 2] = 1;
                    k1++;
                }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
