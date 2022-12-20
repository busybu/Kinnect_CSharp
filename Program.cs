ApplicationConfiguration.Initialize();

var form = new Form();
form.WindowState = FormWindowState.Maximized;
form.FormBorderStyle = FormBorderStyle.None;

PictureBox pb = new PictureBox();
pb.Dock = DockStyle.Fill;
form.Controls.Add(pb);

//string path = @"C:/Users/Aluno/Downloads/shrek.jpg";
string path = "shrek.jpg";

form.KeyDown += (o, e) =>
{
    if (e.KeyCode == Keys.Escape)
        Application.Exit();
};

form.Load += (o, e) =>
{
    Bitmap bmp = (Bitmap)Image.FromFile(path);
    DateTime n = DateTime.Now;
    bmp = Blur.QuickParallelBlur(bmp);
    // throw new System.Exception((DateTime.Now - n).TotalMilliseconds.ToString());

    pb.Image = bmp;
    pb.Refresh();
};

Application.Run(form);
