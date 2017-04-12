using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MainFigCreater;
using System.IO;
using System.Reflection;

namespace Painter
{
    public partial class mainForm : Form
    {
        private Bitmap Canvas;
        private Graphics Painter;
        private List<AbstractFabric> Creators { get; set; }
        private int SelectedFabric { get; set; }
        private AbstractFigure SelectedFig = null;
        private Pen PenColor = new Pen(Color.Black,3);
        private List<Point> points = new List<Point>();
        private FigList FigList = new FigList();
        private bool MouseIsDown = false;
        private Point PrevPoint = new Point();
        private Point CurrPoint = new Point();
        private FigSerialization FigSerializator = new FigSerialization();
        private int btnPosX = 0;
        private int btnCount = 0;
        public List<Type> Types = new List<Type>();

        public mainForm()
        {
            InitializeComponent();
        }
     
        private void CreateFabric(string path)
        {
            int MARGIN = 10;
            int BTNSIZE = 50;
            Button tmpBtn = new Button();
            try
            {
                Type type = Assembly.LoadFile(path).GetExportedTypes()[0];
                AbstractFabric fab = Activator.CreateInstance(type) as AbstractFabric;
                Creators.Add(fab);           
                tmpBtn.Location = new Point(btnPosX, 0);
                tmpBtn.Size = new Size(BTNSIZE, BTNSIZE);
                tmpBtn.TabIndex = btnCount;
            
                tmpBtn.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + 
                                                "\\Images\\"+ fab.GetImgName());
                tmpBtn.BackgroundImageLayout = ImageLayout.Stretch;
                tmpBtn.Click += btn_Clicked;
                btnPosX += tmpBtn.Width + MARGIN;
                btnCount++;
                this.pnlBtnPannel.BeginInvoke((MethodInvoker)(() => 
                pnlBtnPannel.Controls.Add(tmpBtn)));
                Types.Add(fab.GetFType());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }                                
        }
        private void btn_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            SelectedFabric = btn.TabIndex;
        }
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            CreateFabric(e.FullPath);
        }
        private void btnDraw_Click(object sender, EventArgs e)
        {
            int i = Creators[SelectedFabric].CheckPoints(ref points);
            if (i > 0)
            {
                Creators[SelectedFabric].Painter = Painter;
                AbstractFigure Fig;
                Pen Pen = new Pen(PenColor.Color, PenColor.Width);
                Fig = Creators[SelectedFabric].GetFigure(Pen, points);
                Fig.Draw();
                lvFigures.Items.Add(Fig.GetName());
                FigList.AddToList(Fig);
            }
            else
            {
                MessageBox.Show("Неверно заданы точки!!");
            }
            for (int j = 0; j < i; j++)
            {
                points.RemoveAt(0);
            }
            lblPointsN.Text = points.Count.ToString();
            MainView.Image = Canvas;
        }        
        private void btnPenColor_Click(object sender, EventArgs e)
        {
            if (this.ColorPenDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PenColor.Color = this.ColorPenDialog.Color;
            }
            
        }
        private void btnClearPoints_Click(object sender, EventArgs e)
        {
            points.Clear();
            lblPointsN.Text = points.Count.ToString();
        }
        private void MainView_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = new Point();
            p.X = e.X;
            p.Y = e.Y;
            points.Add(p);            
        }
        private void lvFigures_SelectedIndexChanged(object sender, EventArgs e)
        {

            ReadrawSelected();
            if (lvFigures.SelectedIndices.Count > 0)
            {
                int ind = 0;
                ind = lvFigures.SelectedIndices[0];
                SelectedFig = FigList.GetItem(ind);
                MarkFigure(SelectedFig);
            }                     
        }
        private void btnChangeFig_Click(object sender, EventArgs e)
        {
            if (SelectedFig != null)
            {
                SelectedFig.ChangeColor(PenColor.Color);
                SelectedFig.Draw();
                MainView.Image = Canvas;
                SelectedFig = null;
                lvFigures.SelectedIndices.Clear();
            }
        }

        private void MarkFigure(AbstractFigure SelectedFig)
        {                        
            if (SelectedFig is IEditable)
            {
                SelectedFig.Mark();
                MainView.Image = Canvas;
            }
        }
        private void ReadrawSelected()
        {
            if (SelectedFig != null)
            {
                FigList.ReadrawFigures();
                MainView.Image = Canvas;
            }
        }

        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            lvFigures.SelectedIndices.Clear();
            ReadrawSelected();
            Point p = new Point();
            p.X = e.X;
            p.Y = e.Y;
            SelectedFig = FigList.FindPoint(p);
            if (SelectedFig != null)
            {
                MouseIsDown = true;
                PrevPoint = p;
                MarkFigure(SelectedFig);
            }
        }
        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            if (MouseIsDown) points.RemoveAt(points.Count - 1);
            MouseIsDown = false;
            lblPointsN.Text = points.Count.ToString();
            FigList.ReadrawFigures();
        }
        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseIsDown)
            {
                CurrPoint.X = e.X;
                CurrPoint.Y = e.Y;
                int dx =  CurrPoint.X - PrevPoint.X;
                int dy =  CurrPoint.Y - PrevPoint.Y;
                SelectedFig.Move(dx, dy);
                FigList.ReadrawFigures();
                PrevPoint = CurrPoint;
                MainView.Image = Canvas;
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.Filter = "Dat Files |*.json";
                openFileDialog.Title = "Открыть фигуры";
                openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    FigList.Clear();
                    lvFigures.Items.Clear();                 
                    FigSerializator.Deserialize(openFileDialog.FileName,ref FigList,Types);
                    Canvas = new Bitmap(MainView.Width, MainView.Height);
                    Painter = Graphics.FromImage(Canvas);
                    FigList.ResetColors();
                    FigList.SetPainter(Painter);
                    for (int i = 0; i< FigList.Count(); i++)
                    {
                        lvFigures.Items.Add(FigList.GetItem(i).GetName());
                    }
                    
                    FigList.ReadrawFigures();
                    
                    MainView.Image = Canvas;

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog.Filter = "Dat Files |*.json";
                saveFileDialog.Title = "Сохранить фигуры";
                saveFileDialog.ShowDialog();
                if (saveFileDialog.FileName != "")
                {
                    FigSerializator.Serialize(saveFileDialog.FileName, FigList, Types);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            Canvas = new Bitmap(MainView.Width, MainView.Height);
            Painter = Graphics.FromImage(Canvas);
            FileSystemWatcher fileWatcher = new FileSystemWatcher(AppDomain.CurrentDomain.BaseDirectory + "Modules\\", "*.dll");
            fileWatcher.Created += new FileSystemEventHandler(OnChanged);
            pnlBtnPannel.AutoScroll = true;
            fileWatcher.EnableRaisingEvents = true;
            Creators = new List<AbstractFabric>();

            string[] files = Directory.GetFiles(Environment.CurrentDirectory + "\\Modules\\");
            foreach (string file in files)
            {
                CreateFabric(file);
            }

        }
    }
}
