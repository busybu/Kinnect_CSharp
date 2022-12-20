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

Bitmap bmp = null; // Tela
Graphics g = null; // Graphics da Tela
Bitmap crr = null; // Frame atual da WebCam
Bitmap bg = null; // Background Salvo
VideoCaptureDevice videoSource = null;

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
        Blur.QuickParallelBlur((Bitmap)crr.Clone(), 40),
        bg, .2f) :
        crr;
    
    Front.Desenhar(bin, bmp, g);
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
        bg = Blur.QuickParallelBlur(img, 20);
    }
};

Application.Run(form);