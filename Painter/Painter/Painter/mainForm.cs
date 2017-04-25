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
        private const int MARGIN = 10;
        private const int BTNSIZE = 50;
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
        private List<AppDomain> Domains = new List<AppDomain>();
        private AppDomainSetup setup = new AppDomainSetup();
        private string path = "";
        private List<Button> Buttons = new List<Button>();

        public mainForm()
        {
            InitializeComponent();
        }

        private void CreateFabric()
        {            
            Button tmpBtn = new Button();
            try
            {
                Type type = null;
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    var bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    Assembly assembly = AppDomain.CurrentDomain.Load(bytes);
                    type = assembly.GetExportedTypes()[0];
                }              
                AbstractFabric fab = Activator.CreateInstance(type) as AbstractFabric;
                Creators.Add(fab);           
                tmpBtn.Location = new Point(btnPosX, 0);
                tmpBtn.Size = new Size(BTNSIZE, BTNSIZE);
                try
                {
                    tmpBtn.BackgroundImage = Image.FromFile(Environment.CurrentDirectory +
                                                                    "\\Images\\" + fab.GetImgName());
                }
                catch
                {
                    MessageBox.Show("Не удалось найти картинку для кнопки, кнопка будет пустая!");
                }
                
                tmpBtn.BackgroundImageLayout = ImageLayout.Stretch;
                tmpBtn.Click += btn_Clicked;
                btnPosX += tmpBtn.Width + MARGIN;
                btnCount++;
                this.pnlBtnPannel.Invoke((MethodInvoker)(() => 
                pnlBtnPannel.Controls.Add(tmpBtn)));
                Types.Add(fab.GetFType());
                Buttons.Add(tmpBtn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }         
        }
        private void btn_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            SelectedFabric = Buttons.IndexOf(btn);
        }
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            CreateDomain(e.FullPath);
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

        private void RedrawButtons(List<Button> Buttons, int DeletedButtonID)
        {
            btnPosX = btnPosX - BTNSIZE - MARGIN;
            btnCount = btnCount - 1;
            this.pnlBtnPannel.Invoke((MethodInvoker)(() =>
                this.pnlBtnPannel.Controls.RemoveAt(DeletedButtonID)));
            
            for (int i = DeletedButtonID; i<Buttons.Count; i++)
            {
                 this.Buttons[i].Invoke((MethodInvoker)(() => 
                Buttons[i].Location = new Point(Buttons[i].Location.X - BTNSIZE - MARGIN,
                    Buttons[i].Location.Y)));                           
            }
        }

        public delegate void DeleteDel(string Name);

        private void Delete_Fab(object sender, FileSystemEventArgs e)
        {
            int TypeID = 0;
            for (int i=0; i<Types.Count; i++)
            {
                if (Types[i].Name == e.Name.Split('.')[0])
                {
                    TypeID = i;
                }
            }
            string Name = FigList.DeleteFig(Types[TypeID]);
            DeleteDel Del = new DeleteDel(DeleteFromList);
            if (Name != null) lvFigures.Invoke(Del, Name);                     
            SelectedFig = null;
            SelectedFabric = 0;
            Types.RemoveAt(TypeID);
            Creators.RemoveAt(TypeID);
            Buttons.RemoveAt(TypeID);                     
            RedrawButtons(Buttons, TypeID);
            Domains.RemoveAt(TypeID);
            MainView.Image = Canvas;
        }

        private void DeleteFromList(string Name)
        {
            
            lvFigures.SelectedIndices.Clear();       
            int i = 0;
            while (i < lvFigures.Items.Count)
            {
                if (lvFigures.Items[i].Text == Name)
                {
                    lvFigures.Items.RemoveAt(i);
                }
                else i++;
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

        private void CreateDomain(string path)
        {
            AppDomain FabDomain = AppDomain.CreateDomain("Fabric Domain "
                    + Convert.ToString(btnCount), null, setup);
            this.path = path;
            FabDomain.DoCallBack(CreateFabric);
            Domains.Add(FabDomain);
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
            fileWatcher.Deleted += new FileSystemEventHandler(Delete_Fab);
            pnlBtnPannel.AutoScroll = true;
            fileWatcher.EnableRaisingEvents = true;
            Creators = new List<AbstractFabric>();

            setup.ApplicationBase = Environment.CurrentDirectory;
            string[] files = Directory.GetFiles(Environment.CurrentDirectory + "\\Modules\\");
            foreach (string file in files)
            {
                CreateDomain(file);
            }

        }
    }
}
