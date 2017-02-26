namespace Painter
{
    partial class mainForm
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
            this.MainView = new System.Windows.Forms.PictureBox();
            this.ColorBrushDialog = new System.Windows.Forms.ColorDialog();
            this.ColorPenDialog = new System.Windows.Forms.ColorDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnDraw = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.btnPenColor = new System.Windows.Forms.Button();
            this.btnBrushColor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MainView)).BeginInit();
            this.SuspendLayout();
            // 
            // MainView
            // 
            this.MainView.Dock = System.Windows.Forms.DockStyle.Left;
            this.MainView.Location = new System.Drawing.Point(0, 0);
            this.MainView.Name = "MainView";
            this.MainView.Size = new System.Drawing.Size(742, 627);
            this.MainView.TabIndex = 0;
            this.MainView.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(775, 60);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(836, 147);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(812, 195);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(150, 23);
            this.btnDraw.TabIndex = 4;
            this.btnDraw.Text = "Нарисовать";
            this.btnDraw.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(848, 97);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 5;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(968, 86);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 6;
            // 
            // btnPenColor
            // 
            this.btnPenColor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPenColor.Location = new System.Drawing.Point(742, 604);
            this.btnPenColor.Name = "btnPenColor";
            this.btnPenColor.Size = new System.Drawing.Size(330, 23);
            this.btnPenColor.TabIndex = 7;
            this.btnPenColor.Text = "Цвет карандаша";
            this.btnPenColor.UseVisualStyleBackColor = true;
            // 
            // btnBrushColor
            // 
            this.btnBrushColor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnBrushColor.Location = new System.Drawing.Point(742, 581);
            this.btnBrushColor.Name = "btnBrushColor";
            this.btnBrushColor.Size = new System.Drawing.Size(330, 23);
            this.btnBrushColor.TabIndex = 8;
            this.btnBrushColor.Text = "Цвет заливки";
            this.btnBrushColor.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(742, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.label1.Size = new System.Drawing.Size(330, 31);
            this.label1.TabIndex = 10;
            this.label1.Text = "Выберите фигуру";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 627);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrushColor);
            this.Controls.Add(this.btnPenColor);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.MainView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "mainForm";
            this.Text = "Paint";
            ((System.ComponentModel.ISupportInitialize)(this.MainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox MainView;
        private System.Windows.Forms.ColorDialog ColorBrushDialog;
        private System.Windows.Forms.ColorDialog ColorPenDialog;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button btnPenColor;
        private System.Windows.Forms.Button btnBrushColor;
        private System.Windows.Forms.Label label1;
    }
}

