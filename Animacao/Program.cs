using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

ApplicationConfiguration.Initialize();
Application.EnableVisualStyles();

var form = new Form();
form.WindowState = FormWindowState.Maximized;
form.FormBorderStyle = FormBorderStyle.None;

PictureBox pb = new PictureBox();
pb.Dock = DockStyle.Fill;
form.Controls.Add(pb);

Bitmap bg = Blur.ApplyBlur(Image.FromFile("./images/imagem3.jpeg") as Bitmap);

List<Bitmap> imagens = new List<Bitmap>();
imagens.Add(Blur.ApplyBlur(Image.FromFile("./images/imagem2.jpeg") as Bitmap));



var sla = Binarization.ApplyBin(imagens[0], bg);
imagens.Add(sla);




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

Application.Run(form);
