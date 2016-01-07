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
            Zoom(ResultPicture, 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Zoom(ResultPicture, 0.5);
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

        bool validPicture;

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string fle = openFileDialog1.FileName;
            if (fle.Substring(fle.LastIndexOf('.') + 1, fle.Length - fle.LastIndexOf('.') - 1) == "jpg")
            {
                validPicture = true;
                textBox3.Text = openFileDialog1.FileName;
            }
            else
            {
                validPicture = false;
                MessageBox.Show("Invalid FileName!");
            }
        }

        private void DrawResultPicture(PictureBox FromPicture)
        {
            int NewPicWidth = ResultPicture.Width;
            double koef = (double)ResultPicture.Width / (double)FromPicture.Width;
            int NewPicHeight = (int)((double)FromPicture.Height * koef);

            ResultPicture.Width = NewPicWidth;
            ResultPicture.Height = NewPicHeight;

            Bitmap tempBmp = new Bitmap(NewPicWidth, NewPicHeight);
            Graphics grp = Graphics.FromImage(tempBmp);
            Bitmap bmp = new Bitmap(FromPicture.Image);
            Rectangle srcRect = new Rectangle(0, 0, FromPicture.Width, FromPicture.Height);
            Rectangle dstRect = new Rectangle(0, 0, NewPicWidth, NewPicHeight);
            grp.DrawImage(bmp, dstRect, srcRect, GraphicsUnit.Pixel);
            grp.Dispose();

            ResultPicture.Image = tempBmp;
            ResultPicture.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!validPicture)
            {
                MessageBox.Show("Invalid FileName!");
                return;
            }
            WorkPicture.Image = Image.FromFile(textBox3.Text);
            SourcePicture.Image = Image.FromFile(textBox3.Text);
            FinalPicture.Image = Image.FromFile(textBox3.Text);

            DrawResultPicture(SourcePicture);
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
            int AverageRed = (pxl[0, i, j] + pxl[0, i1, j] + pxl[0, i, j1] + pxl[0, i1, j1]) / 4;
            int AverageGreen = (pxl[1, i, j] + pxl[1, i1, j] + pxl[1, i, j1] + pxl[1, i1, j1]) / 4;
            int AverageBlue = (pxl[2, i, j] + pxl[2, i1, j] + pxl[2, i, j1] + pxl[2, i1, j1]) / 4;

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
                    (min(j, GlobW - 1),
                     min(i, GlobH - 1),
                     min(j + scaledw, GlobW - 1),
                     min(i + scaledh, GlobH - 1)));
                    grp.FillRectangle(brush, new Rectangle(min(j, GlobW), min(i, GlobH), min(j + scaledw, GlobW), min(i + scaledh, GlobH)));
                }
                return;
            //var brush = new SolidBrush(Color.Black);
            //grp.FillRectangle(brush, new Rectangle(0, 0, 200, 200));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Interfase thr = new Interfase(Work);
            IAsyncResult res = thr.BeginInvoke(null, null);
            //Work();
        }

        private void Rescale(PictureBox Pic, double scale)
        {

            Bitmap bmp = new Bitmap(Pic.Image);
            int newW = (int)(bmp.Width * scale);
            int newH = (int)(bmp.Height * scale);
            Pic.Width = newW;
            Pic.Height = newH;

            Bitmap tempBmp = new Bitmap(newW, newH);
            Graphics grp = Graphics.FromImage(tempBmp);
            Rectangle srcRect = new Rectangle(0, 0, newW, newH);
            Rectangle dstRect = new Rectangle(0, 0, Pic.Width, Pic.Height);
            grp.DrawImage(bmp, dstRect, srcRect, GraphicsUnit.Pixel);
            grp.Dispose();

            Pic.Image = tempBmp;
            return;
        }

        private void Zoom(PictureBox Pic, double scale)
        {
            Bitmap bmp = new Bitmap(WorkPicture.Image);
            int newW = (int)(Pic.Width * scale);
            int newH = (int)(Pic.Height * scale);

            Bitmap tempBmp = new Bitmap(Pic.Image);
            Graphics grp = Graphics.FromImage(tempBmp);
            Rectangle srcRect = new Rectangle(0, 0, newW, newH);
            Rectangle dstRect = new Rectangle(0, 0, Pic.Width, Pic.Height);
            grp.DrawImage(bmp, dstRect, srcRect, GraphicsUnit.Pixel);
            grp.Dispose();

            Pic.Image = tempBmp;
            return;
        }

        private void Work()
        {
            //MessageBox.Show(textBox1.Text + " " + textBox2.Text);
            //MessageBox.Show(Convert.ToString(int.Parse(textBox2.Text)));
            double h = double.Parse(textBox1.Text);
            double w = double.Parse(textBox2.Text);
            if (h == 0 || w == 0)
                return;
            scale = 1;
            if (h < 1 || w < 1)
                scale = 10;
            if (h < 0.1 || w < 0.1)
                scale = 100;
            if (h < 0.01 || w < 0.01)
                scale = 1000;
            if (h < 0.001 || w < 0.001)
                scale = 10000;

            scaledh = (int)h * scale;
            scaledw = (int)w * scale;

            Bitmap WorkBitmap = new Bitmap(SourcePicture.Image);

            WorkPicture.Image = WorkBitmap;
            Rescale(WorkPicture, scale);

            WorkPicture.Refresh();

            GlobH = WorkPicture.Height;
            GlobW = WorkPicture.Width;

            WorkBitmap = new Bitmap(WorkPicture.Image);

            grp = Graphics.FromImage(WorkBitmap);

            pxl = new byte[3, GlobH, GlobW];
            pxl = BitmapToByteRgb(WorkBitmap);

            for (int i = 0; i < GlobH; i += scaledh)
            {
                /* drw = new Changes(DrawCells);
                IAsyncResult res = drw.BeginInvoke(i, null, null);
                drw.EndInvoke(res);*/


                //new Thread(() => DrawCells(bmp, i)).Start();
                DrawCells(i);
                //func.BeginInvoke(i, null, null);

            }

            WorkPicture.Image = WorkBitmap;
            FinalPicture.Image = WorkBitmap;

            MessageBox.Show("Successful!");
            DrawResultPicture(WorkPicture);

            Action action = () =>
            {
                ResultPicture.Image = bmp;
                ResultPicture.Refresh();
            };
            Invoke(action);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
        }
    }
}
