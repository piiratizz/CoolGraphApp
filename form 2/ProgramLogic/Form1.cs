using CoolGraphicsApp.ProgramLogic;
using CoolGraphicsApp.ProgramLogic.Save;
using System;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace CoolGraphicsApp
{
    public partial class Graphics : Form
    {
        private GraphPane graphPane;
        private LineItem _lineItem;
        private IDataFormatter _formatter;
        private string Path = Environment.CurrentDirectory + "\\config\\";

        public Graphics()
        {
            InitializeComponent();
            _formatter = new BinaryDataFormatter();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAsync();
        }

        private async void LoadAsync()
        {
            ShowLoadingScreen();
            IStarter userLogic = null;
            CreateInstanceOfUserEntryClass(ref userLogic);

            await Task.Run(() =>
            {
                userLogic.Start();
            });

            ConfigurateCurve();
            LoadUserSettings();
            HideLoadingScreen();
            UpdateFrame();
        }


        private void ShowLoadingScreen()
        {
            panel1.Visible = true;
            label10.Visible = true;
        }

        private void HideLoadingScreen()
        {
            panel1.Visible = false ;
            label10.Visible = false;
        }


        private void LoadUserSettings()
        {
            Config cfg = _formatter.Load<Config>(Path);
            if (cfg == null) return;

            textBox3.Text = cfg.Title;
            textBox4.Text = cfg.LineName;
            numericUpDown1.Value = cfg.LineWidth;
            textBox1.Text = cfg.XAxisName;
            textBox2.Text = cfg.YAxisName;
        }

        private void SaveUserSettings()
        {
            Config cfg = new Config();

            ConfigBuilder configBuilder = new ConfigBuilder(cfg);

            configBuilder.SetTitle(textBox3.Text)
                         .SetLineName(textBox4.Text)
                         .SetLineWidth(numericUpDown1.Value)
                         .SetXAxisName(textBox1.Text)
                         .SetYAxisName(textBox2.Text);

            _formatter.Save<Config>(Path, configBuilder.GetConfig());
        }


        private void CreateInstanceOfUserEntryClass(ref IStarter starter)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (typeof(IStarter).IsAssignableFrom(type) && !type.IsInterface)
                {
                    starter = (IStarter)assembly.CreateInstance(type.FullName);
                }
            }

            if (starter == null)
            {
                throw new Exception("You dont have class that implement IStarter interface");
            }
        }

        private void ConfigurateCurve()
        {
            graphPane = zedGraphControl1.GraphPane;
            graphPane.XAxis.Scale.IsUseTenPower = false;
            graphPane.YAxis.Scale.IsUseTenPower = false;
            zedGraphControl1.IsAntiAlias = true;
            graphPane.YAxis.Scale.MagAuto = false;
            graphPane.XAxis.Scale.MagAuto = false;
            graphPane.YAxis.ScaleFormatEvent += new ZedGraph.Axis.ScaleFormatHandler(Axis_ScaleFormatEvent);
            graphPane.XAxis.ScaleFormatEvent += new ZedGraph.Axis.ScaleFormatHandler(Axis_ScaleFormatEvent);
            graphPane.XAxis.MajorGrid.IsVisible = true;
            graphPane.XAxis.MajorGrid.DashOn = 10;
            graphPane.XAxis.MajorGrid.DashOff = 5;
            graphPane.YAxis.MajorGrid.IsVisible = true;
            graphPane.YAxis.MajorGrid.DashOn = 10;
            graphPane.YAxis.MajorGrid.DashOff = 5;
            graphPane.YAxis.MinorGrid.IsVisible = true;
            graphPane.YAxis.MinorGrid.DashOn = 1;
            graphPane.YAxis.MinorGrid.DashOff = 2;
            graphPane.XAxis.MinorGrid.IsVisible = true;
            graphPane.XAxis.MinorGrid.DashOn = 1;
            graphPane.XAxis.MinorGrid.DashOff = 2;

            _lineItem = graphPane.AddCurve("", PointsManager.GetPairs(), Color.Red, SymbolType.None);
            label7.Text = PointsManager.SwapCount.ToString();
            label8.Text = PointsManager.CompareCount.ToString();
        }
       

        private string Axis_ScaleFormatEvent(GraphPane pane, ZedGraph.Axis axis, double val, int index)
        {
            return val.ToString();
        }

        private void UpdateFrame()
        {
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void GraphicsNameChanged(object sender, EventArgs e)
        {
            graphPane.Title.Text = textBox3.Text;
            UpdateFrame();
        }

        private void LineNameChanged(object sender, EventArgs e)
        {
            graphPane.CurveList[0].Label.Text = textBox4.Text;
            UpdateFrame();
        }

        private void AxisXNameChanged(object sender, EventArgs e)
        {
            graphPane.XAxis.Title.Text = textBox1.Text;
            UpdateFrame();
        }

        private void AxisYNameChanged(object sender, EventArgs e)
        {
            graphPane.YAxis.Title.Text = textBox2.Text;
            UpdateFrame();
        }

        private void WidthChanged(object sender, EventArgs e)
        {
            _lineItem.Line.Width = (float)numericUpDown1.Value;
            UpdateFrame();
        }

        private void ColorWindowButton(object sender, EventArgs e)
        {
            var res = colorDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                graphPane.CurveList[0].Color = colorDialog1.Color;
            }
            UpdateFrame();
        }

        private void BackgroundColorButton(object sender, EventArgs e)
        {
            var res = colorDialog2.ShowDialog();
            if (res == DialogResult.OK)
            {
                zedGraphControl1.GraphPane.Fill.Color = colorDialog2.Color;
            }
            UpdateFrame();
        }

        private void label7_MouseClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(label7.Text);
            MessageBox.Show("Скопировано");
        }

        private void label8_MouseClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(label8.Text);
            MessageBox.Show("Скопировано");
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            SaveUserSettings();
        }


    }
}
