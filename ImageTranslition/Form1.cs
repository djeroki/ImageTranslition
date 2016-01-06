using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;

namespace ImageTranslition
{


    public delegate void Changes(int i);
    public delegate void Interfase();

    public partial class Form1 : Form
    {


        public int scale, scaledw, scaledh, GlobW, GlobH;
        public Bitmap bmp, pattern;
        public Graphics grp;
        public double koefh, koefw;
        public byte[,,] pxl;


        public Form1()
        {
            InitializeComponent();
        }

        int max(int x, int y)
        {
            return (x > y ? x : y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Zoom(pictureBox1, 0.5);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Zoom(pictureBox1, 1);
        }

        int min(int x, int y)
        {
            return (x < y ? x : y);
        }

        public unsafe static byte[,,] BitmapToByteRgb(Bitmap bmp)
        {
            int width = bmp.Width,
                height = bmp.Height;
            byte[,,] res = new byte[3, height, width];

            BitmapData bd =
                bmp.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);

            try
            {
                byte* curpos;
                for (int h = 0; h < height; h++)
                {
                    curpos = ((byte*)bd.Scan0) + h * bd.Stride;
                    for (int w = 0; w < width; w++)
                    {
                        res[2, h, w] = *(curpos++);
                        res[1, h, w] = *(curpos++);
                        res[0, h, w] = *(curpos++);
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(bd);
            }


            return res;
        }

        public Color GetAverageColor(int j, int i, int j1, int i1)
        {
            /* Color c1, c2, c3, c4;
             c1 = pattern.GetPixel(j, i);
             c2 = pattern.GetPixel(j, i1);
             c3 = pattern.GetPixel(j1, i);
             c4 = pattern.GetPixel(j1, i1);

             int AverageRed = (c1.R + c2.R + c3.R + c4.R) / 4;
             int AverageGreen = (c1.G + c2.G + c3.G + c4.G) / 4;
             int AverageBlue = (c1.B + c2.B + c3.B + c4.B) / 4;
             return Color.FromArgb(AverageRed, AverageGreen, AverageBlue);*/
            int AverageRed   = (pxl[0, i, j] + pxl[0, i1, j] + pxl[0, i, j1] + pxl[0, i1, j1]) / 4;
            int AverageGreen = (pxl[1, i, j] + pxl[1, i1, j] + pxl[1, i, j1] + pxl[1, i1, j1]) / 4;
            int AverageBlue  = (pxl[2, i, j] + pxl[2, i1, j] + pxl[2, i, j1] + pxl[2, i1, j1]) / 4;

            return Color.FromArgb(AverageRed, AverageGreen, AverageBlue);

        }

        static object locker = new object();

        public void DrawCells(int i)
        {
            //var pen = new Pen(Color.Black, 0.00001f);

                for (int j = 0; j < GlobW; j += scaledw)
                {
                    var brush = new SolidBrush
                    (GetAverageColor
                    (min(j + 1, GlobW - 1),
                     min(i + 1, GlobH - 1),
                     min(j + scaledw - 1, GlobW - 1),
                     min(i + scaledh - 1, GlobH - 1)));
                    grp.FillRectangle(brush, new Rectangle(j, i, scaledw, scaledh));
                }
                return;
            //var brush = new SolidBrush(Color.Black);
            //grp.FillRectangle(brush, new Rectangle(0, 0, 200, 200));
        }

        public void Zoom(PictureBox pb1, double x)
        {
            Graphics grp = Graphics.FromHwnd(pb1.Handle);
            Bitmap bmp = new Bitmap(pb1.Image);
            int newW = (int)(bmp.Width * x);
            int newH = (int)(bmp.Height * x);
            Rectangle srcRect = new Rectangle(0, 0, newW, newH);
            Rectangle dstRect = new Rectangle(0, 0, pb1.Width, pb1.Height);
            grp.DrawImage(bmp, dstRect, srcRect, GraphicsUnit.Pixel);
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Interfase thr = new Interfase(Work);
            IAsyncResult res = thr.BeginInvoke(null, null);
        }

        private void Work()
        {
            //MessageBox.Show(textBox1.Text + " " + textBox2.Text);
            //MessageBox.Show(Convert.ToString(int.Parse(textBox2.Text)));
            double h = double.Parse(textBox1.Text);
            double w = double.Parse(textBox2.Text);
            scale = 1;
            if (h < 1 || w < 1)
                scale = 10;
            if (h < 0.1 || w < 0.1)
                scale = 100;
            scaledh = (int)(h * scale);
            scaledw = (int)(w * scale);
            pattern = new Bitmap(pictureBox1.Height, pictureBox1.Width);
            pattern = (Bitmap)pictureBox2.Image;

            koefh = (double)pictureBox1.Height / (double)pictureBox1.Image.Height;
            koefw = (double)pictureBox1.Width / (double)pictureBox1.Image.Width;

            scaledh = (int)(scaledh * 1 / koefh);
            scaledw = (int)(scaledw * 1 / koefw);

            bmp = new Bitmap(pictureBox1.Height, pictureBox1.Width);
            bmp = (Bitmap)pictureBox1.Image;

            bmp.SetResolution(pictureBox1.Image.VerticalResolution, pictureBox1.Image.HorizontalResolution);
            grp = Graphics.FromImage(bmp);

            GlobH = bmp.Height;
            GlobW = bmp.Width;

            pxl = new byte[3, GlobH, GlobW];
            pxl = BitmapToByteRgb(pattern);

            for (int i = 0; i < GlobH; i += scaledh)
            {
                /* drw = new Changes(DrawCells);
                IAsyncResult res = drw.BeginInvoke(i, null, null);
                drw.EndInvoke(res);*/


                //new Thread(() => DrawCells(bmp, i)).Start();
                DrawCells(i);
                //func.BeginInvoke(i, null, null);

            }

            Action action = () =>
            {
                pictureBox1.Image = bmp;
                pictureBox1.Refresh();
            };
            Invoke(action);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
        }
    }
}
