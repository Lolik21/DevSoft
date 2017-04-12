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
            this.ColorPenDialog = new System.Windows.Forms.ColorDialog();
            this.btnDraw = new System.Windows.Forms.Button();
            this.btnPenColor = new System.Windows.Forms.Button();
            this.lvFigures = new System.Windows.Forms.ListView();
            this.ColumHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblPointsCount = new System.Windows.Forms.Label();
            this.lblPointsN = new System.Windows.Forms.Label();
            this.btnClearPoints = new System.Windows.Forms.Button();
            this.btnChangeFig = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pnlBtnPannel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.MainView)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainView
            // 
            this.MainView.BackColor = System.Drawing.SystemColors.Window;
            this.MainView.Dock = System.Windows.Forms.DockStyle.Left;
            this.MainView.Location = new System.Drawing.Point(0, 24);
            this.MainView.Name = "MainView";
            this.MainView.Size = new System.Drawing.Size(830, 603);
            this.MainView.TabIndex = 0;
            this.MainView.TabStop = false;
            this.MainView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseClick);
            this.MainView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.MainView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            this.MainView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseUp);
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(840, 76);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(226, 23);
            this.btnDraw.TabIndex = 4;
            this.btnDraw.Text = "Нарисовать";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // btnPenColor
            // 
            this.btnPenColor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPenColor.Location = new System.Drawing.Point(830, 604);
            this.btnPenColor.Name = "btnPenColor";
            this.btnPenColor.Size = new System.Drawing.Size(242, 23);
            this.btnPenColor.TabIndex = 7;
            this.btnPenColor.Text = "Цвет";
            this.btnPenColor.UseVisualStyleBackColor = true;
            this.btnPenColor.Click += new System.EventHandler(this.btnPenColor_Click);
            // 
            // lvFigures
            // 
            this.lvFigures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumHeader});
            this.lvFigures.GridLines = true;
            this.lvFigures.Location = new System.Drawing.Point(834, 211);
            this.lvFigures.Name = "lvFigures";
            this.lvFigures.Size = new System.Drawing.Size(232, 387);
            this.lvFigures.TabIndex = 15;
            this.lvFigures.UseCompatibleStateImageBehavior = false;
            this.lvFigures.View = System.Windows.Forms.View.Details;
            this.lvFigures.SelectedIndexChanged += new System.EventHandler(this.lvFigures_SelectedIndexChanged);
            // 
            // ColumHeader
            // 
            this.ColumHeader.Text = "Фигуры:";
            this.ColumHeader.Width = 228;
            // 
            // lblPointsCount
            // 
            this.lblPointsCount.AutoSize = true;
            this.lblPointsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPointsCount.Location = new System.Drawing.Point(836, 24);
            this.lblPointsCount.Name = "lblPointsCount";
            this.lblPointsCount.Size = new System.Drawing.Size(156, 20);
            this.lblPointsCount.TabIndex = 24;
            this.lblPointsCount.Text = "Количество точек: ";
            // 
            // lblPointsN
            // 
            this.lblPointsN.AutoSize = true;
            this.lblPointsN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPointsN.Location = new System.Drawing.Point(992, 24);
            this.lblPointsN.Name = "lblPointsN";
            this.lblPointsN.Size = new System.Drawing.Size(0, 20);
            this.lblPointsN.TabIndex = 25;
            // 
            // btnClearPoints
            // 
            this.btnClearPoints.Location = new System.Drawing.Point(840, 47);
            this.btnClearPoints.Name = "btnClearPoints";
            this.btnClearPoints.Size = new System.Drawing.Size(226, 23);
            this.btnClearPoints.TabIndex = 26;
            this.btnClearPoints.Text = "Очистить список точек";
            this.btnClearPoints.UseVisualStyleBackColor = true;
            this.btnClearPoints.Click += new System.EventHandler(this.btnClearPoints_Click);
            // 
            // btnChangeFig
            // 
            this.btnChangeFig.Location = new System.Drawing.Point(840, 105);
            this.btnChangeFig.Name = "btnChangeFig";
            this.btnChangeFig.Size = new System.Drawing.Size(226, 23);
            this.btnChangeFig.TabIndex = 27;
            this.btnChangeFig.Text = "Изменить фигуру";
            this.btnChangeFig.UseVisualStyleBackColor = true;
            this.btnChangeFig.Click += new System.EventHandler(this.btnChangeFig_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1072, 24);
            this.MainMenu.TabIndex = 28;
            this.MainMenu.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.SaveToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.CheckOnClick = true;
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.OpenToolStripMenuItem.Text = "Открыть";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.SaveToolStripMenuItem.Text = "Сохранить";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // pnlBtnPannel
            // 
            this.pnlBtnPannel.Location = new System.Drawing.Point(840, 134);
            this.pnlBtnPannel.Name = "pnlBtnPannel";
            this.pnlBtnPannel.Size = new System.Drawing.Size(226, 71);
            this.pnlBtnPannel.TabIndex = 29;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 627);
            this.Controls.Add(this.pnlBtnPannel);
            this.Controls.Add(this.btnChangeFig);
            this.Controls.Add(this.btnClearPoints);
            this.Controls.Add(this.lblPointsN);
            this.Controls.Add(this.lblPointsCount);
            this.Controls.Add(this.lvFigures);
            this.Controls.Add(this.btnPenColor);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.MainView);
            this.Controls.Add(this.MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "mainForm";
            this.Text = "Paint";
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MainView)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox MainView;
        private System.Windows.Forms.ColorDialog ColorPenDialog;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Button btnPenColor;
        private System.Windows.Forms.ListView lvFigures;
        private System.Windows.Forms.Label lblPointsCount;
        private System.Windows.Forms.Label lblPointsN;
        private System.Windows.Forms.Button btnClearPoints;
        private System.Windows.Forms.ColumnHeader ColumHeader;
        private System.Windows.Forms.Button btnChangeFig;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel pnlBtnPannel;
    }
}

