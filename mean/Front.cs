using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public static class Front
{
    public static List<Point> Points = new List<Point>();

    public static bool aux1 {get; set;} = false;

    public static void Desenhar(Bitmap cam, Bitmap bmp, Graphics g,
        Point cursor, bool isDown)
    {
        try
        {
            //Canetas
            Pen CanetaVermelhaTeste = new Pen(Brushes.Red, 10f);
            Pen CanetaPreta = new Pen(Brushes.Black, 10f);
            Pen CanetaMarrom = new Pen(Brushes.SaddleBrown, 10f);


            //Pinceis
            Brush FundoVerde = Brushes.DarkGreen;
            Brush FundoPretoReconhecimento = Brushes.Black; 
            Brush FundoCinzaPreenchimento = Brushes.Gray;
            

            //Variaveis para tamanhos e posicionamentos
            int alturaMolduraMarrom = (int)(0.778f * bmp.Height);
            int larguraMolduraMarrom = (int)(0.688f * bmp.Width);  

            int larguraBotao = (int)((larguraMolduraMarrom + 50) / 5);
            int alturaBotao = (int)(0.199f * bmp.Height);

            int larguraMolduraPreta = larguraMolduraMarrom + 20;
            int alturaMolduraPreta = alturaMolduraMarrom + 20;

            int alturaPreenchimentoBotoes = alturaBotao - 10;
            int larguraPreenchimentoBotoes = ((larguraMolduraMarrom) / 5);

            int alturaCamera = (int)(0.380 * bmp.Height);
            int larguraCamera = (bmp.Width - larguraMolduraMarrom);


            //Molduras
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle RetanguloPretoTela = new Rectangle(0, 0, bmp.Width, bmp.Height);
            Rectangle MolduraQuadro = new Rectangle(10, 10, larguraMolduraMarrom, alturaMolduraMarrom);
            Rectangle MolduraPretaMoldura = new Rectangle(0, 0, larguraMolduraMarrom + 20, alturaMolduraMarrom + 20);
            Rectangle Desfazer = new Rectangle(0, alturaMolduraMarrom + 20, larguraBotao, alturaBotao);
            Rectangle Refazer = new Rectangle(larguraBotao, alturaMolduraMarrom + 20, larguraBotao, alturaBotao); 
            Rectangle Resetar = new Rectangle(2 * larguraBotao, alturaMolduraMarrom + 20, larguraBotao, alturaBotao); 
            Rectangle Enviar = new Rectangle(3 * larguraBotao, alturaMolduraMarrom + 20, larguraBotao, alturaBotao); 
            Rectangle TirarFoto = new Rectangle(larguraMolduraPreta, alturaCamera + 10, (larguraCamera - 20), alturaBotao / 2);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //Decoração extra//
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle Gradient = new Rectangle(5, alturaMolduraMarrom + 25, larguraBotao * 4, alturaBotao);
            Brush GradientBotoes = new LinearGradientBrush(
                new Point(5, alturaMolduraMarrom + 25),
                new Point(5 + larguraBotao * 4, alturaMolduraMarrom + 25 + alturaBotao),
                Color.FromArgb(255,42,72,88),  
                Color.FromArgb(255,8,159,143)); 
            Brush GradientBotaoTirarFoto = new LinearGradientBrush(
                new Point(5, alturaMolduraMarrom + 25),
                new Point(5 + larguraBotao * 4, alturaMolduraMarrom + 25 + alturaBotao),
                Color.FromArgb(255,8,159,143),
                Color.FromArgb(255,42,72,88)); 
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //Areas clicaveis dos botões
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle PreenchimentoDesfazer = new Rectangle(5, alturaMolduraMarrom + 25, larguraPreenchimentoBotoes, alturaPreenchimentoBotoes);
            Rectangle PreenchimentoRefazer = new Rectangle(larguraPreenchimentoBotoes + 15, alturaMolduraMarrom + 25, larguraPreenchimentoBotoes, alturaPreenchimentoBotoes);
            Rectangle PreenchimentoResetar = new Rectangle((2 * larguraPreenchimentoBotoes + 25), alturaMolduraMarrom + 25, larguraPreenchimentoBotoes, alturaPreenchimentoBotoes);
            Rectangle PreenchimentoEnviar = new Rectangle((3 * larguraPreenchimentoBotoes + 35), alturaMolduraMarrom + 25, larguraPreenchimentoBotoes, alturaPreenchimentoBotoes);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //Area Clicaveis
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle QuadroVerde = new Rectangle(15, 15, larguraMolduraMarrom - 10, alturaMolduraMarrom - 10);
            Rectangle PreenchimentoTirarFoto = new Rectangle(larguraMolduraPreta + 5, alturaCamera + 15, (larguraCamera - 30), (alturaBotao / 2) - 10);
            Rectangle Reconhecimento = new Rectangle(4 * larguraPreenchimentoBotoes + 45, alturaMolduraMarrom + 25, larguraMolduraPreta - (4 * larguraBotao), alturaPreenchimentoBotoes + 10);
            Rectangle EspaçoCamera = new Rectangle(larguraMolduraPreta, 0, larguraCamera, alturaCamera + 10);
            Rectangle AreaParaDesenhar = new Rectangle(larguraMolduraPreta + 5, alturaCamera + (alturaBotao / 2) + 15, (larguraCamera - 30), bmp.Height - alturaCamera - (alturaBotao / 2) - 20);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            

            //Escritas//
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            SolidBrush drawBrush = new SolidBrush(Color.AliceBlue);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            String textoDesfazer = "←";
            Font fontDesfazer = new Font("Arial", alturaPreenchimentoBotoes - (int)(0.115 * bmp.Height));
                    
            String textoRefazer = "→";
            Font fontRefazer = new Font("Arial", alturaPreenchimentoBotoes - (int)(0.115 * bmp.Height));
    
            String textoResetar = "Resetar";
            Font fontResetar = new Font(FontFamily.GetFamilies(g)[59], alturaPreenchimentoBotoes - (int)(0.160 * bmp.Height));
          
            String textoEnviar = "Enviar";
            Font fontEnviar = new Font(FontFamily.GetFamilies(g)[59], alturaPreenchimentoBotoes - (int)(0.160 * bmp.Height));
            
            String textoTirarFoto = "Tirar Foto";
            Font fontTirarFoto = new Font(FontFamily.GetFamilies(g)[59], (alturaBotao / 2) - (int)(0.055 * bmp.Height));
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            
            //Layout//
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            g.FillRectangle(FundoCinzaPreenchimento, AreaParaDesenhar);

            if (AreaParaDesenhar.Contains(cursor))
            {
                if (isDown)
                {
                    Points.Add(cursor);
                }
                else
                {
                    Points.Clear();
                }
            }

            if (Points.Count > 1)
                g.DrawLines(CanetaPreta, Points.ToArray());

            g.DrawRectangle(CanetaPreta, RetanguloPretoTela);
            g.DrawRectangle(CanetaMarrom, MolduraQuadro);
            g.DrawRectangle(CanetaPreta, MolduraPretaMoldura);
            g.FillRectangle(FundoVerde, QuadroVerde);

            g.FillRectangle(GradientBotoes, Gradient);
            g.FillRectangle(GradientBotaoTirarFoto, PreenchimentoTirarFoto);

            g.DrawRectangle(CanetaPreta, Desfazer);
            if (PreenchimentoDesfazer.Contains(cursor))
            {
                g.FillRectangle(Brushes.Red, PreenchimentoDesfazer);
                if(PreenchimentoDesfazer.Contains(cursor) && isDown == true)
                    g.FillRectangle(Brushes.DarkRed, PreenchimentoDesfazer);
            }

            g.DrawRectangle(CanetaPreta, Refazer);
            if (PreenchimentoRefazer.Contains(cursor))
            {
                g.FillRectangle(Brushes.Red, PreenchimentoRefazer);
                if(PreenchimentoRefazer.Contains(cursor) && isDown == true)
                    g.FillRectangle(Brushes.DarkRed, PreenchimentoRefazer);
            }

            g.DrawRectangle(CanetaPreta, Resetar);
            if (PreenchimentoResetar.Contains(cursor))
            {
                g.FillRectangle(Brushes.Red, PreenchimentoResetar);
                if(PreenchimentoResetar.Contains(cursor) && isDown == true)
                    g.FillRectangle(Brushes.DarkRed, PreenchimentoResetar);
            }

            g.DrawRectangle(CanetaPreta, Enviar);
            if (PreenchimentoEnviar.Contains(cursor))
            {
                g.FillRectangle(Brushes.Red, PreenchimentoEnviar);
                if(PreenchimentoEnviar.Contains(cursor) && isDown == true)
                    g.FillRectangle(Brushes.DarkRed, PreenchimentoEnviar);
            }

            g.DrawRectangle(CanetaPreta, TirarFoto);
            if (PreenchimentoTirarFoto.Contains(cursor))
            {
                g.FillRectangle(Brushes.Red, PreenchimentoTirarFoto);
                if(PreenchimentoTirarFoto.Contains(cursor) && isDown == true)
                {
                    aux1 = true;
                    g.FillRectangle(Brushes.DarkRed, PreenchimentoTirarFoto);

                }
            }

            g.DrawRectangle(CanetaPreta,EspaçoCamera);

            g.FillRectangle(FundoPretoReconhecimento, Reconhecimento);
            

            g.DrawString(textoDesfazer, fontDesfazer, drawBrush, Desfazer, format);
            g.DrawString(textoRefazer, fontRefazer, drawBrush, Refazer, format);
            g.DrawString(textoResetar, fontResetar, drawBrush, Resetar, format);
            g.DrawString(textoEnviar, fontEnviar, drawBrush, Enviar, format);
            g.DrawString(textoTirarFoto, fontTirarFoto, drawBrush, TirarFoto, format);
            
            //Imagem Camera
            RectangleF destRect = new RectangleF((bmp.Width - 5) - larguraCamera + 30, 5, larguraCamera - 30, alturaCamera);    
            RectangleF srcRect = new RectangleF(0.0F, 0.0F, cam.Width, cam.Height);
            GraphicsUnit units = GraphicsUnit.Pixel;      
            g.DrawImage(cam, destRect, srcRect, units);
            
        }
        catch
        {
            
        }
    }
}