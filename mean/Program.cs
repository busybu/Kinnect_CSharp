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

int N = 15;
float treshold = 0.15f;

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
form.Controls.Add(pb);

System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();
tm.Interval = 20;

form.KeyDown += (o, e) =>
{
    if (e.KeyCode == Keys.Escape)
    {
        videoSource.Stop();
        Application.Exit();
    }
};


int i = 0;
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

    var bin = bg != null ? 
        Binarization.ApplyBin(
            Blur.QuickParallelBlurGray((Bitmap)crr.Clone(), N),
            bg, treshold) 
        : crr;
    var center = hand.GetCenterPixel(bin);

    Pen pen = new Pen(Color.Red, 2);
    // g.DrawImage(crr, new Rectangle(0, 0, 1600, 1200),
    //     new Rectangle(0, 0, 1600, 1200), GraphicsUnit.Pixel);

    Front.Desenhar(bin, bmp, g, cursor, isDown); 
    g.FillRectangle(Brushes.Red, center.X -5, center.Y - 5, 10, 10);
    
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
        bg = Blur.QuickParallelBlurGray(img, N);
    }
    else if (e.KeyCode == Keys.O)
    {
        treshold += 0.01f;
    }
    else if (e.KeyCode == Keys.P)
    {
        treshold -= 0.01f;
    }
    else if (e.KeyCode == Keys.K)
    {
        N++;
    }
    else if (e.KeyCode == Keys.L)
    {
        N--;
    }
};

Application.Run(form);
