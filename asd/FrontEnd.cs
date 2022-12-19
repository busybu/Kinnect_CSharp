// using System.Drawing;
// using System.Windows.Forms;
// using System.Drawing.Drawing2D;

// ApplicationConfiguration.Initialize();

// var form = new Form();
// form.WindowState = FormWindowState.Maximized;
// form.FormBorderStyle = FormBorderStyle.None;

// PictureBox pb = new PictureBox();
// pb.Dock = DockStyle.Fill;
// form.Controls.Add(pb);

// Bitmap bmp = null;
// Graphics g = null;

// Timer tm = new Timer();
// tm.Interval = 25;

// form.KeyDown += (o, e) =>
// {
//     if (e.KeyCode == Keys.Escape)
//         Application.Exit();
// };

// form.Load += (o, e) =>
// {
//     bmp = new Bitmap(pb.Width, pb.Height);
//     g = Graphics.FromImage(bmp);
//     pb.Image = bmp;
//     tm.Start();
// };

// tm.Tick += (o, e) =>
// {

    
//     //Canetas
//     Pen CanetaVermelhaTeste = new Pen(Brushes.Red, 10f);
//     Pen CanetaPreta = new Pen(Brushes.Black, 10f);
//     Pen CanetaMarrom = new Pen(Brushes.SaddleBrown, 10f);

//     //Pinceis
//     Brush FundoVerde = Brushes.DarkGreen;
//     Brush FundoPretoReconhecimento = Brushes.Black;                       // Brush exemplo = new LinearGradientBrush();
//     Brush FundoCinzaPreenchimento = Brushes.Gray;

//     //Molduras
//     Rectangle RetanguloPretoTela = new Rectangle(0, 0, bmp.Width, bmp.Height);
//     Rectangle MolduraQuadro = new Rectangle(10, 10, (int)(0.756f * bmp.Width), (int)(0.778f * bmp.Height));
//     Rectangle MolduraPretaMoldura = new Rectangle(0, 0, (int)(0.765f * bmp.Width), (int)(0.793f * bmp.Height));
//     Rectangle Desfazer = new Rectangle(0, 856, (int)(((0.765f * bmp.Width) + 50) / 5), (int)(0.207f * bmp.Height));
//     Rectangle Refazer = new Rectangle((int)(((0.765f * bmp.Width) + 50) / 5), 856, (int)(((0.765f * bmp.Width) + 50) / 5), (int)(0.207f * bmp.Height)); 
//     Rectangle Resetar = new Rectangle((int)(2 * (((0.765f * bmp.Width) + 50) / 5)), 856, (int)(((0.765f * bmp.Width) + 50) / 5), (int)(0.207f * bmp.Height)); 
//     Rectangle Enviar = new Rectangle((int)(3 * (((0.765f * bmp.Width) + 50) / 5)), 856, (int)(((0.765f * bmp.Width) + 50) / 5), (int)(0.207f * bmp.Height)); 
   
//     //Area Clicaveis
//     ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//     Rectangle QuadroVerde = new Rectangle(11, 11, (int)(0.753f * bmp.Width), (int)(0.773f * bmp.Height));
//     Rectangle PreenchimentoDesfazer = new Rectangle(5,861, (int)(((0.739f * bmp.Width) + 50) / 5), (int)(0.199f * bmp.Height));
//     Rectangle PreenchimentoRefazer = new Rectangle((int)(((0.765f * bmp.Width) + 76) / 5), 861, (int)(((0.739f * bmp.Width) + 50) / 5), (int)(0.199f * bmp.Height));
//     Rectangle PreenchimentoResetar = new Rectangle((int)(2 * (((0.765f * bmp.Width) + 63) / 5)), 861, (int)(((0.739f * bmp.Width) + 50) / 5), (int)(0.199f * bmp.Height));
//     Rectangle PreenchimentoEnviar = new Rectangle((int)(3 * (((0.765f * bmp.Width) + 59) / 5)), 861, (int)(((0.739f * bmp.Width) + 50) / 5), (int)(0.199f * bmp.Height));
//     Rectangle Reconhecimento = new Rectangle((int)(4 * (((0.765f * bmp.Width) + 50) / 5)), 856, ((int)(0.765f * bmp.Width) - ((int)(4 * (((0.765f * bmp.Width) + 44) / 5)))), (int)(0.207f * bmp.Height));
//     Rectangle EspaçoCamera = new Rectangle((int)(0.765f * bmp.Width), 0, (bmp.Width - (int)(0.765f * bmp.Width)),(int)(0.600f * bmp.Height));
//     Rectangle AreaParaDesenhar = new Rectangle((int)(0.767f * bmp.Width),(int)(0.605f * bmp.Height),(int)(0.231f * bmp.Width),(int)(0.391f * bmp.Height));
//     ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//     //Layout//
//     ////////////////////////////////////////////////////////////////////
//     g.DrawRectangle(CanetaPreta, RetanguloPretoTela);
//     g.DrawRectangle(CanetaMarrom, MolduraQuadro);
//     g.FillRectangle(FundoVerde, QuadroVerde);
//     g.DrawRectangle(CanetaPreta, MolduraPretaMoldura);
//     g.DrawRectangle(CanetaPreta, Desfazer);
//     g.DrawRectangle(CanetaPreta, Refazer);
//     g.DrawRectangle(CanetaPreta, Resetar);
//     g.DrawRectangle(CanetaPreta, Enviar);
//     g.FillRectangle(FundoPretoReconhecimento, Reconhecimento);
//     g.FillRectangle(FundoCinzaPreenchimento, PreenchimentoDesfazer);
//     g.FillRectangle(FundoCinzaPreenchimento, PreenchimentoRefazer);
//     g.FillRectangle(FundoCinzaPreenchimento, PreenchimentoResetar);
//     g.FillRectangle(FundoCinzaPreenchimento, PreenchimentoEnviar);
//     g.DrawRectangle(CanetaPreta,EspaçoCamera);
//     g.FillRectangle(FundoCinzaPreenchimento, AreaParaDesenhar);

//     ////////////////////////////////////////////////////////////////////

    
//     Image hemer = Image.FromFile("hemer.jpg");
             
//     // Create rectangle for displaying image.
//     RectangleF destRect = new RectangleF(100.0F, 100.0F, 450.0F, 150.0F);
             
//     // Create rectangle for source image.
//     RectangleF srcRect = new RectangleF(50.0F, 50.0F, 150.0F, 150.0F);

//     GraphicsUnit units = GraphicsUnit.Pixel;
             
//     // Draw image to screen.
//     g.DrawImage(hemer, destRect, srcRect, units);


//     pb.Refresh();
// };

// Application.Run(form);