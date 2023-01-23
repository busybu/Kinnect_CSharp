using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;

TextBox tb = null;
TextBox valor = null;
Tela telaAtual = null;

ProfessorHome professorHome = new ProfessorHome();

BancoQuestoes bancoQuestoes = new BancoQuestoes();

VisualizarNotas visualizarNotas = new VisualizarNotas();

/////////////////////////////////////////////
professorHome.QuestionsDatabaseOpen += delegate
{
    telaAtual = bancoQuestoes;
};

professorHome.OnGradePageOpen += delegate
{
    telaAtual = visualizarNotas;
};

/////////////////////////////////////////////
bancoQuestoes.OnClearRequest += delegate
{
    tb.Clear();
};

bancoQuestoes.OnGradePageOpen += delegate
{
    telaAtual = visualizarNotas;
};

bancoQuestoes.HomeOpen += delegate
{
    telaAtual = professorHome;
};
/////////////////////////////////////////////
visualizarNotas.QuestionsDatabaseOpen += delegate
{
    telaAtual = bancoQuestoes;
};

visualizarNotas.HomeOpen += delegate
{
    telaAtual = professorHome;
};


telaAtual = professorHome;


ApplicationConfiguration.Initialize();
Application.EnableVisualStyles();

var form = new Form();
form.WindowState = FormWindowState.Maximized;
form.FormBorderStyle = FormBorderStyle.None;

Bitmap bmp = null; // Tela
Graphics g = null; // Graphics da Tela
Bitmap crr = null; // Frame atual da WebCam
Bitmap bg = null; // Background Salvo
Point cursor = Point.Empty;
bool isDown = false;
VideoCaptureDevice videoSource = null;

tb = new TextBox();
form.Controls.Add(tb);
tb.Location = new Point(-100, -100);

valor = new TextBox();
form.Controls.Add(valor);
valor.Location = new Point(-200, -200);

PictureBox pb = new PictureBox();
pb.Dock = DockStyle.Fill;
form.Controls.Add(pb);

System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();
tm.Interval = 20;

form.KeyDown += (o, e) =>
{
    if (e.KeyCode == Keys.Escape)
    {
        videoSource?.Stop();
        Application.Exit();
    }
};

pb.MouseMove += (o, e) =>
{
    cursor = e.Location;
};

pb.MouseDown += (o, e) =>
{
    isDown = true;
};

pb.MouseUp += (o, e) =>
{
    isDown = false;
};

int i = 0;
form.Load += (o, e) =>
{
    bmp = new Bitmap(pb.Width, pb.Height);
    pb.Image = bmp;
    g = Graphics.FromImage(bmp);
    videoSource?.Start();
    tm.Start();
};

tm.Tick += (o, e) =>
{
    tb.Focus();
    // valor.Focus();
    if (bmp == null)
        return;
    
    telaAtual.Desenhar(crr ?? new Bitmap(640, 480), bmp, g, cursor, isDown, tb.Text, valor.Text);

    pb.Refresh();
};

var videoSources = new FilterInfoCollection(FilterCategory.VideoInputDevice);
if (videoSources != null && videoSources.Count > 0)
{
    videoSource = new VideoCaptureDevice(videoSources[0].MonikerString);
    videoSource.NewFrame += delegate(object sender, AForge.Video.NewFrameEventArgs eventArgs)
    {
        var old = crr;

        crr = (Bitmap)eventArgs.Frame.Clone();

        old.Dispose();
    };
}

form.KeyDown += (o, e) =>
{
    if (e.KeyCode == Keys.Space)
    {
        var img = (Bitmap)crr.Clone();
        Blur.SetBuffer(img);
        bg = Blur.QuickParallelBlur(img, 20);
    }
};


Application.Run(form);
