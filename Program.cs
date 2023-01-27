using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using RandomFlorestLib;

bool test = false;

ApplicationConfiguration.Initialize();
Application.EnableVisualStyles();

RandomFlorest rfr = RandomFlorest.Load("FalangoFalaBosta.sla");

var form = new Form();
form.WindowState = FormWindowState.Maximized;
form.FormBorderStyle = FormBorderStyle.None;

int N = 5;
int M = N;
float treshold = 0.15f;

//Parametros de calibração da identificação do estado da mão
int handthreshold = 4500;
int area = 30;
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

Point ajdCenter = Point.Empty;
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

    Point center = Point.Empty;
    (bool, double, bool) openfunction = (false, 0, false);
    try
    {
        center = hand.GetCenterPixel(bin);
        openfunction = (hand.TheBestOpenHand(bin, handthreshold, area));
    }
    catch { }

    if (center != Point.Empty && ajdCenter == Point.Empty)
        ajdCenter = center;
    else if (center != Point.Empty)
    {
        ajdCenter = new Point(
            (int)(0.5 * center.X + 0.5 * ajdCenter.X),
            (int)(0.5 * center.Y + 0.5 * ajdCenter.Y)
        );
    }
    center = new Point(ajdCenter.X * 2, ajdCenter.Y * 2);
    center = new Point(bmp.Width - center.X, center.Y);

    handvalue = openfunction.Item2;
    var open = openfunction.Item1;

    Front.Desenhar(crr, bmp, g, Cursor.Position, test); // estavamos aqui!!!!"  desenha com os pontos da mão se mouse clicado

    // Front.Desenhar(crr, bmp, g, cursor, isDown);

 
    g.DrawString(param.ToString(), SystemFonts.CaptionFont,
        Brushes.White, new PointF(10, 10));

    g.DrawString(bgParam.ToString(), SystemFonts.CaptionFont,
        Brushes.White, new PointF(20, 20));

    g.DrawString(handvalue.ToString(), SystemFonts.CaptionFont,
        Brushes.White, new PointF(20, 50));

    g.DrawString(handthreshold.ToString(), SystemFonts.CaptionFont,
        Brushes.White, new PointF(20, 70));
        
    g.FillRectangle(open ? Brushes.Green : Brushes.Red, center.X * 3 - 5, center.Y * 3 - 5, 10, 10);
    
    ph.RegisterCursor(Cursor.Position, !test, g, (bmp.Height*bmp.Height) + (bmp.Width*bmp.Width), rfr);

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
        test = !test;
        return;
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

// RandomFlorest rfr = new RandomFlorest();
// rfr.Fit();
// rfr.Store("FalangoFalaBosta.sla");