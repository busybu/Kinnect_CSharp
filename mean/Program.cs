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
float treshold = 0.15f;

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
    videoSource.Start();
    tm.Start();
};

tm.Tick += (o, e) =>
{
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
    var open = hand.IsOpenHand(bin);

    // Pen pen = new Pen(Color.Red, 2);
    g.Clear(Color.White);
    if (bg != null)
        g.DrawImage(bg, new Rectangle(0, 0, bg.Width, bg.Height));
    if (crr != null)
        g.DrawImage(crr, new Rectangle(bg?.Width ?? 0, 0, crr.Width, crr.Height));
    if (bin != null)
        g.DrawImage(bin, new Rectangle((bg?.Width ?? 0) + (crr?.Width ?? 0), 0, bin.Width, bin.Height));
    if (hist != null)
    {
        var histImg = drawHist(hist);
        g.DrawImage(histImg, new Rectangle(0, (bg?.Height ?? 0), bin.Width, bin.Height));
    }

    // Front.Desenhar(bg, bmp, g, cursor, isDown);

    g.DrawString(param.ToString(), SystemFonts.CaptionFont,
        Brushes.White, new PointF(10, 10));

    g.DrawString(bgParam.ToString(), SystemFonts.CaptionFont,
        Brushes.White, new PointF(20, 20));
    g.FillRectangle(open ? Brushes.Green : Brushes.Red, center.X -5, center.Y - 5, 10, 10);
    
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
        var img = (Bitmap)crr.Clone();
        bgParam = Equalization.GetParam(img);
        bg = Blur.QuickParallelBlurGray(img, N);
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
        N = N > 0 ? N-- : N;
    }
};

Application.Run(form);

Image drawHist(int[] hist)
{
    var bmp = new Bitmap(512, 256);
    var g = Graphics.FromImage(bmp);
    float margin = 16;

    int max = hist.Max();
    float barlen = (bmp.Width - 2 * margin) / hist.Length;
    float r = (bmp.Height - 2 * margin) / max;

    for (int i = 0; i < hist.Length; i++)
    {
        float bar = hist[i] * r;
        g.FillRectangle(Brushes.Black,
            margin + i * barlen,
            bmp.Height - margin - bar,
            barlen,
            bar);
        g.DrawRectangle(Pens.DarkBlue,
            margin + i * barlen,
            bmp.Height - margin - bar,
            barlen,
            bar);
    }

    return bmp;
}