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
            this.tbY1 = new System.Windows.Forms.TextBox();
            this.tbX2 = new System.Windows.Forms.TextBox();
            this.btnDraw = new System.Windows.Forms.Button();
            this.tbX1 = new System.Windows.Forms.TextBox();
            this.tbY2 = new System.Windows.Forms.TextBox();
            this.btnPenColor = new System.Windows.Forms.Button();
            this.btnBrushColor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbChousePoints = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.tbPointsCount = new System.Windows.Forms.TextBox();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnEllipse = new System.Windows.Forms.Button();
            this.btnCurveLine = new System.Windows.Forms.Button();
            this.btnPoligon = new System.Windows.Forms.Button();
            this.lblChouse = new System.Windows.Forms.Label();
            this.lblChousenFig = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MainView)).BeginInit();
            this.SuspendLayout();
            // 
            // MainView
            // 
            this.MainView.BackColor = System.Drawing.SystemColors.Window;
            this.MainView.Dock = System.Windows.Forms.DockStyle.Left;
            this.MainView.Location = new System.Drawing.Point(0, 0);
            this.MainView.Name = "MainView";
            this.MainView.Size = new System.Drawing.Size(830, 627);
            this.MainView.TabIndex = 0;
            this.MainView.TabStop = false;
            // 
            // tbY1
            // 
            this.tbY1.Location = new System.Drawing.Point(960, 105);
            this.tbY1.Name = "tbY1";
            this.tbY1.Size = new System.Drawing.Size(106, 20);
            this.tbY1.TabIndex = 2;
            // 
            // tbX2
            // 
            this.tbX2.Location = new System.Drawing.Point(834, 131);
            this.tbX2.Name = "tbX2";
            this.tbX2.Size = new System.Drawing.Size(100, 20);
            this.tbX2.TabIndex = 3;
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(876, 236);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(150, 23);
            this.btnDraw.TabIndex = 4;
            this.btnDraw.Text = "Нарисовать";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // tbX1
            // 
            this.tbX1.Location = new System.Drawing.Point(834, 105);
            this.tbX1.Name = "tbX1";
            this.tbX1.Size = new System.Drawing.Size(99, 20);
            this.tbX1.TabIndex = 5;
            // 
            // tbY2
            // 
            this.tbY2.Location = new System.Drawing.Point(960, 131);
            this.tbY2.Name = "tbY2";
            this.tbY2.Size = new System.Drawing.Size(106, 20);
            this.tbY2.TabIndex = 6;
            // 
            // btnPenColor
            // 
            this.btnPenColor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPenColor.Location = new System.Drawing.Point(830, 604);
            this.btnPenColor.Name = "btnPenColor";
            this.btnPenColor.Size = new System.Drawing.Size(242, 23);
            this.btnPenColor.TabIndex = 7;
            this.btnPenColor.Text = "Цвет карандаша";
            this.btnPenColor.UseVisualStyleBackColor = true;
            // 
            // btnBrushColor
            // 
            this.btnBrushColor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnBrushColor.Location = new System.Drawing.Point(830, 581);
            this.btnBrushColor.Name = "btnBrushColor";
            this.btnBrushColor.Size = new System.Drawing.Size(242, 23);
            this.btnBrushColor.TabIndex = 8;
            this.btnBrushColor.Text = "Цвет заливки";
            this.btnBrushColor.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(830, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.label1.Size = new System.Drawing.Size(242, 31);
            this.label1.TabIndex = 10;
            this.label1.Text = "Выберите фигуру";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbChousePoints
            // 
            this.cbChousePoints.FormattingEnabled = true;
            this.cbChousePoints.Location = new System.Drawing.Point(834, 183);
            this.cbChousePoints.Name = "cbChousePoints";
            this.cbChousePoints.Size = new System.Drawing.Size(232, 21);
            this.cbChousePoints.TabIndex = 12;
            this.cbChousePoints.Text = "Точки";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(834, 210);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 13;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(964, 210);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(102, 20);
            this.textBox2.TabIndex = 14;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(834, 265);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(232, 310);
            this.listView1.TabIndex = 15;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // tbPointsCount
            // 
            this.tbPointsCount.Location = new System.Drawing.Point(898, 157);
            this.tbPointsCount.Name = "tbPointsCount";
            this.tbPointsCount.Size = new System.Drawing.Size(100, 20);
            this.tbPointsCount.TabIndex = 16;
            this.tbPointsCount.MouseLeave += new System.EventHandler(this.tbPointsCount_MouseLeave);
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(834, 34);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(43, 37);
            this.btnLine.TabIndex = 17;
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.Location = new System.Drawing.Point(883, 34);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(40, 37);
            this.btnRectangle.TabIndex = 18;
            this.btnRectangle.UseVisualStyleBackColor = true;
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnEllipse
            // 
            this.btnEllipse.Location = new System.Drawing.Point(929, 34);
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.Size = new System.Drawing.Size(43, 37);
            this.btnEllipse.TabIndex = 19;
            this.btnEllipse.UseVisualStyleBackColor = true;
            this.btnEllipse.Click += new System.EventHandler(this.btnEllipse_Click);
            // 
            // btnCurveLine
            // 
            this.btnCurveLine.Location = new System.Drawing.Point(978, 34);
            this.btnCurveLine.Name = "btnCurveLine";
            this.btnCurveLine.Size = new System.Drawing.Size(44, 37);
            this.btnCurveLine.TabIndex = 20;
            this.btnCurveLine.UseVisualStyleBackColor = true;
            this.btnCurveLine.Click += new System.EventHandler(this.btnCurveLine_Click);
            // 
            // btnPoligon
            // 
            this.btnPoligon.Location = new System.Drawing.Point(1028, 34);
            this.btnPoligon.Name = "btnPoligon";
            this.btnPoligon.Size = new System.Drawing.Size(38, 37);
            this.btnPoligon.TabIndex = 21;
            this.btnPoligon.UseVisualStyleBackColor = true;
            this.btnPoligon.Click += new System.EventHandler(this.btnPoligon_Click);
            // 
            // lblChouse
            // 
            this.lblChouse.AutoSize = true;
            this.lblChouse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblChouse.Location = new System.Drawing.Point(836, 74);
            this.lblChouse.Name = "lblChouse";
            this.lblChouse.Size = new System.Drawing.Size(75, 20);
            this.lblChouse.TabIndex = 22;
            this.lblChouse.Text = "Выбран: ";
            // 
            // lblChousenFig
            // 
            this.lblChousenFig.AutoSize = true;
            this.lblChousenFig.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblChousenFig.Location = new System.Drawing.Point(907, 74);
            this.lblChousenFig.Name = "lblChousenFig";
            this.lblChousenFig.Size = new System.Drawing.Size(0, 20);
            this.lblChousenFig.TabIndex = 23;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 627);
            this.Controls.Add(this.lblChousenFig);
            this.Controls.Add(this.lblChouse);
            this.Controls.Add(this.btnPoligon);
            this.Controls.Add(this.btnCurveLine);
            this.Controls.Add(this.btnEllipse);
            this.Controls.Add(this.btnRectangle);
            this.Controls.Add(this.btnLine);
            this.Controls.Add(this.tbPointsCount);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cbChousePoints);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrushColor);
            this.Controls.Add(this.btnPenColor);
            this.Controls.Add(this.tbY2);
            this.Controls.Add(this.tbX1);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.tbX2);
            this.Controls.Add(this.tbY1);
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
        private System.Windows.Forms.TextBox tbY1;
        private System.Windows.Forms.TextBox tbX2;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.TextBox tbX1;
        private System.Windows.Forms.TextBox tbY2;
        private System.Windows.Forms.Button btnPenColor;
        private System.Windows.Forms.Button btnBrushColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbChousePoints;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox tbPointsCount;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnRectangle;
        private System.Windows.Forms.Button btnEllipse;
        private System.Windows.Forms.Button btnCurveLine;
        private System.Windows.Forms.Button btnPoligon;
        private System.Windows.Forms.Label lblChouse;
        private System.Windows.Forms.Label lblChousenFig;
    }
}

