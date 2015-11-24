﻿using System;
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

namespace ImageTranslition
{
    public partial class Form1 : Form
    {
        public int scale, scaledw, scaledh, GlobW, GlobH;
        public Bitmap bmp, pattern;
        public Graphics grp;

        public Form1()
        {
            InitializeComponent();
        }

        int max(int x, int y)
        {
            return (x > y ? x : y);
        }
        int min(int x, int y)
        {
            return (x < y ? x : y);
        }

        public Color GetAverageColor(Bitmap bmp, int j, int i, int j1, int i1)
        {
            Color c1, c2, c3, c4;
            c1 = bmp.GetPixel(j, i);
            c2 = bmp.GetPixel(j, i1);
            c3 = bmp.GetPixel(j1, i);
            c4 = bmp.GetPixel(j1, i1);

            int AverageRed = (c1.R + c2.R + c3.R + c4.R) / 4;
            int AverageGreen = (c1.G + c2.G + c3.G + c4.G) / 4;
            int AverageBlue = (c1.B + c2.B + c3.B + c4.B) / 4;
            return Color.FromArgb(AverageRed, AverageGreen, AverageBlue);
        }

        public void DrawCells(Bitmap pattern, Bitmap bmp, int i)
        {
            //var pen = new Pen(Color.Black, 0.00001f);

            lock(grp)
            {
                for (int j = 0; j < GlobW; j += scaledw)
                {
                    var brush = new HatchBrush(HatchStyle.Vertical, Color.Transparent, GetAverageColor(pattern, min(j, GlobW - 1), min(i, GlobH - 1), min(j + scaledw, GlobW - 1), min(i + scaledh, GlobH - 1)));
                    //grp.DrawRectangle(pen, new Rectangle(j, i, j + scaledw, i + scaledh));
                    grp.FillRectangle(brush, new Rectangle(j, i, j + scaledw, i + scaledh));
                }
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(textBox1.Text + " " + textBox2.Text);
            //MessageBox.Show(Convert.ToString(int.Parse(textBox2.Text)));
            double h = double.Parse(textBox1.Text);
            double w = double.Parse(textBox2.Text);
            scale = 10;
            scaledh = (int)(h * scale);
            scaledw = (int)(w * scale);
            Bitmap pattern = new Bitmap(pictureBox1.Height, pictureBox1.Width);
            pattern = (Bitmap)pictureBox1.Image;


            bmp = new Bitmap(pictureBox1.Height, pictureBox1.Width);
            bmp = (Bitmap)pictureBox1.Image;
            bmp.SetResolution(pictureBox1.Image.VerticalResolution, pictureBox1.Image.HorizontalResolution);
            grp = Graphics.FromImage(bmp);

            GlobH = bmp.Height;
            GlobW = bmp.Width;

            for (int i = 0; i < GlobH; i += scaledh)
            {
                //new Thread(() => DrawCells(bmp, i)).Start();
                DrawCells(pattern, bmp, i);
                //func.BeginInvoke(i, null, null);
                pictureBox1.Image = bmp;
                pictureBox1.Refresh();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
