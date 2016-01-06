namespace ImageTranslition
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.ResultPicture = new System.Windows.Forms.PictureBox();
            this.WorkPicture = new System.Windows.Forms.PictureBox();
            this.SourcePicture = new System.Windows.Forms.PictureBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button5 = new System.Windows.Forms.Button();
            this.FinalPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ResultPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourcePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinalPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(530, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(530, 62);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(544, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Repaint";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(544, 117);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Zoom x2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(544, 146);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Zoom x0.5";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ResultPicture
            // 
            this.ResultPicture.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ResultPicture.Location = new System.Drawing.Point(12, 6);
            this.ResultPicture.Name = "ResultPicture";
            this.ResultPicture.Size = new System.Drawing.Size(512, 565);
            this.ResultPicture.TabIndex = 7;
            this.ResultPicture.TabStop = false;
            // 
            // WorkPicture
            // 
            this.WorkPicture.Location = new System.Drawing.Point(758, 12);
            this.WorkPicture.Name = "WorkPicture";
            this.WorkPicture.Size = new System.Drawing.Size(100, 50);
            this.WorkPicture.TabIndex = 8;
            this.WorkPicture.TabStop = false;
            this.WorkPicture.Visible = false;
            // 
            // SourcePicture
            // 
            this.SourcePicture.Location = new System.Drawing.Point(758, 68);
            this.SourcePicture.Name = "SourcePicture";
            this.SourcePicture.Size = new System.Drawing.Size(100, 50);
            this.SourcePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.SourcePicture.TabIndex = 9;
            this.SourcePicture.TabStop = false;
            this.SourcePicture.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(530, 200);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(286, 20);
            this.textBox3.TabIndex = 10;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(544, 226);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "Browse";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(625, 226);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 12;
            this.button5.Text = "Load";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // FinalPicture
            // 
            this.FinalPicture.Location = new System.Drawing.Point(758, 521);
            this.FinalPicture.Name = "FinalPicture";
            this.FinalPicture.Size = new System.Drawing.Size(100, 50);
            this.FinalPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.FinalPicture.TabIndex = 13;
            this.FinalPicture.TabStop = false;
            this.FinalPicture.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 615);
            this.Controls.Add(this.FinalPicture);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.SourcePicture);
            this.Controls.Add(this.WorkPicture);
            this.Controls.Add(this.ResultPicture);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ResultPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourcePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinalPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox ResultPicture;
        private System.Windows.Forms.PictureBox WorkPicture;
        private System.Windows.Forms.PictureBox SourcePicture;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.PictureBox FinalPicture;
    }
}

