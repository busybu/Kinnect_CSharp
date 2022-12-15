using System.Drawing;
using System.Windows.Forms;

ApplicationConfiguration.Initialize();

var form = new Form();
form.WindowState = FormWindowState.Maximized;
form.FormBorderStyle = FormBorderStyle.None;

PictureBox pb = new PictureBox();
pb.Dock = DockStyle.Fill;
form.Controls.Add(pb);

Bitmap bg = Image.FromFile("imagem3.jpeg") as Bitmap;

List<Bitmap> imagens = new List<Bitmap>();
imagens.Add(Image.FromFile("imagem2.jpeg") as Bitmap);

imagens.Add(Binarization.ApplyBin(imagens[0], bg))


System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();
tm.Interval = 2500;

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
    pb.Image = imagens[i];
    pb.Refresh();
};



Application.Run(form);
