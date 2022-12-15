using System.Drawing;
using System.Windows.Forms;

ApplicationConfiguration.Initialize();

var form = new Form();
form.WindowState = FormWindowState.Maximized;
form.FormBorderStyle = FormBorderStyle.None;

PictureBox pb = new PictureBox();
pb.Dock = DockStyle.Fill;
form.Controls.Add(pb);
 
List<Bitmap> imagens = new List<Bitmap>();
imagens.Add(Image.FromFile("imagem.jpeg") as Bitmap);
imagens.Add(Image.FromFile("imagem2.jpeg") as Bitmap);
imagens.Add(Image.FromFile("imagem3.jpeg") as Bitmap);

System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();
tm.Interval = 25;

form.KeyDown += (o,e)=>
{
    if (e.KeyCode==Keys.Escape)
        Application.Exit();
};

form.Load += (o,e) =>
{   
    
    foreach (var item in imagens)
    {
        pb.Image = item;
        pb.Refresh();
    }
};

int i = 0;
tm.Tick += (o,e) =>
{
    i++;
    if(i >= imagens.Count)
        tm.Stop();
}

Application.Run(form);
