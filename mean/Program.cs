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

PictureBox pb = new PictureBox();
pb.Dock = DockStyle.Fill;
form.Controls.Add(pb);

Blur.SetBuffer(Image.FromFile("./images/bgHemer.jpeg") as Bitmap);
Bitmap bg = Blur.QuickParallelBlurGray(Image.FromFile("./images/bgHemer.jpeg") as Bitmap);

List<Bitmap> imagens = new List<Bitmap>();
imagens.Add(Blur.QuickParallelBlurGray(Image.FromFile("./images/Hemer.jpeg") as Bitmap));


System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();
tm.Interval = 1000;


form.KeyDown += (o, e) =>
{
    if (e.KeyCode == Keys.Escape)
        Application.Exit();
};
int i = 0;
form.Load += (o, e) =>
{
    var binar = Binarization.ApplyBin(imagens[0], bg, 0.5f);
    imagens.Add(binar);
    tm.Start();
};


tm.Tick += (o, e) =>
{
    i++;
    if (i >= imagens.Count)
        i = 0;
    // imagens[i] = (imagens[i], bg , i * 5);
    pb.Image = imagens[i];
    pb.Refresh();
};


// VideoCaptureDevice videoSource;
// var videoSources = new FilterInfoCollection(FilterCategory.VideoInputDevice);
// if (videoSources != null && videoSources.Count > 0)
// {
//     videoSource = new VideoCaptureDevice(videoSources[0].MonikerString);
//     videoSource.NewFrame += delegate(object sender, AForge.Video.NewFrameEventArgs eventArgs)
//     {
//         var bmp = (Bitmap)eventArgs.Frame.Clone();
//         pb.Image = bmp;
//     };
//     videoSource.Start();
// }

Application.Run(form);
