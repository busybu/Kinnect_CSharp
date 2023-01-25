using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;

ApplicationConfiguration.Initialize();
Application.EnableVisualStyles();

var form = new Form();
form.WindowState = FormWindowState.Maximized;
form.FormBorderStyle = FormBorderStyle.None;

int N = 5;
int M = N;
float treshold = 0.15f;

//Parametros de calibração da identificação do estado da mão
int handthreshold = 2900;
int area = 40;
double handvalue = 0;

float param = 0;
float bgParam = 0;
Bitmap bmp = null; // Tela
Graphics g = null; // Graphics da Tela
Bitmap crr = null; // Frame atual da WebCam
Bitmap bg = null; // Background Salvo
Point cursor = Point.Empty;
bool isDown = false;

VideoCaptureDevice videoSource = null;
HandRecognizer hand = new HandRecognizer();
PointsHandler handler = new PointsHandler();
PictureBox pb = new PictureBox();
pb.Dock = DockStyle.Fill;
System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();
var videoSources = new FilterInfoCollection(FilterCategory.VideoInputDevice);


form.Controls.Add(pb);
tm.Interval = 20;

form.KeyDown += (o, e) =>
{
    if (e.KeyCode == Keys.Escape)
    {
        videoSource.Stop();
        Application.Exit();
    }
};

form.Load += (o, e) =>
{
    bmp = new Bitmap(pb.Width, pb.Height);
    pb.Image = bmp;
    g = Graphics.FromImage(bmp);
    videoSource?.Start();
    tm.Start();
};

PointsHandler ph = new PointsHandler();

tm.Tick += (o, e) =>
{   
    // DrawResize.DrawCursor(bmp, g, cursor, isDown);
    handler.GenerateMnist();
    pb.Refresh();


    if (bmp == null || crr == null)
        return;

    Bitmap bin;
    int[] hist = null;

    if (bg != null)
    {
        var clone = (Bitmap)crr.Clone();
        hist = Abra.Kadabra(clone, bg, bgParam, treshold, N);
        bin = clone;
    }
    else
    {
        bin = crr;
    }

    var center = hand.GetCenterPixel(bin);
    var open = (hand.BetterOpenHand(bin, handthreshold, area)).Item1;

    Front.Desenhar(crr, bmp, g, center, isDown); // estavamos aqui!!!!"  desenha com os pontos da mão se mouse clicado

    // Front.Desenhar(crr, bmp, g, cursor, isDown);

 
    g.DrawString(param.ToString(), SystemFonts.CaptionFont,
        Brushes.White, new PointF(10, 10));

    g.DrawString(bgParam.ToString(), SystemFonts.CaptionFont,
        Brushes.White, new PointF(20, 20));
        
    g.FillRectangle(open ? Brushes.Green : Brushes.Red, center.X * 3 - 5, center.Y * 3 - 5, 10, 10);
    
    ph.RegisterCursor(center, !open, g, (bmp.Height*bmp.Height) + (bmp.Width*bmp.Width));

    pb.Refresh();
};

if (videoSources != null && videoSources.Count > 0)
{
    videoSource = new VideoCaptureDevice(videoSources[0].MonikerString);
    videoSource.NewFrame += delegate(object sender, AForge.Video.NewFrameEventArgs eventArgs)
    {
        var old = crr;

        crr = (Bitmap)eventArgs.Frame.Clone();

        old?.Dispose();
    };
}

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

  
form.KeyDown += (o, e) =>
{
    if (e.KeyCode == Keys.Space)
    {
        N = M;
        var img = (Bitmap)crr.Clone();
        bgParam = Equalization.GetParam(img);
        bg = Blur.QuickParallelBlurGray(img, N);
        N = 0;
    }

     if (e.KeyCode == Keys.O)
    {
        treshold += 0.01f;
    }
    else if (e.KeyCode == Keys.P)
    {
        treshold = treshold > 0 ? treshold -= 0.01f : treshold;
    }
    else if (e.KeyCode == Keys.K)
    {
        N++;
    }
    else if (e.KeyCode == Keys.L)
    {
        if (N > 0)
            {
                N--;
            }
    }
    else if (e.KeyCode == Keys.W)
    {
        handthreshold += 50;
    }
    else if (e.KeyCode == Keys.S)
    {
        if (handthreshold > 0)
        {
            handthreshold -= 50;
        }
    }
    else if (e.KeyCode == Keys.E)
    {
        area++;
    }
    else if (e.KeyCode == Keys.D)
    {
        if (area > 0)
        {
            area--;
        }
    }
    
};

Application.Run(form);

