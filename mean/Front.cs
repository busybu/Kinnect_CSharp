using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public static class Front
{
    public static void Desenhar(Bitmap cam, Bitmap bmp, Graphics g)
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
            Brush FundoVermelhoTirarFoto = new SolidBrush(Color.FromArgb(255,8,115,127));
            

            int alturaMolduraMarrom = (int)(0.778f * bmp.Height);
            int larguraMolduraMarrom = (int)(0.688f * bmp.Width);  

            int larguraBotao = (int)((larguraMolduraMarrom + 50) / 5);
            int alturaBotao = (int)(0.199f * bmp.Height);

            int larguraMolduraPreta = larguraMolduraMarrom + 20;
            int alturaMolduraPreta = alturaMolduraMarrom + 20;

            int alturaPreenchimentoBotoes = alturaBotao - 4;
            int larguraPreenchimentoBotoes = ((larguraMolduraMarrom) / 5);

            int alturaCamera = (int)(0.380 * bmp.Height);
            int larguraCamera = (bmp.Width - larguraMolduraMarrom);


            //Molduras
            Rectangle RetanguloPretoTela = new Rectangle(0, 0, bmp.Width, bmp.Height);
            Rectangle MolduraQuadro = new Rectangle(10, 10, larguraMolduraMarrom, alturaMolduraMarrom);
            Rectangle MolduraPretaMoldura = new Rectangle(0, 0, larguraMolduraMarrom + 20, alturaMolduraMarrom + 20);
            Rectangle Desfazer = new Rectangle(0, alturaMolduraMarrom + 20, larguraBotao, alturaBotao);
            Rectangle Refazer = new Rectangle(larguraBotao, alturaMolduraMarrom + 20, larguraBotao, alturaBotao); 
            Rectangle Resetar = new Rectangle(2 * larguraBotao, alturaMolduraMarrom + 20, larguraBotao, alturaBotao); 
            Rectangle Enviar = new Rectangle(3 * larguraBotao, alturaMolduraMarrom + 20, larguraBotao, alturaBotao); 
            Rectangle TirarFoto = new Rectangle(larguraMolduraPreta, alturaCamera + 10, (larguraCamera - 20), alturaBotao / 2);

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
                Color.FromArgb(255,42,72,88)  
                ); 
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
          
          
            //Area Clicaveis
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle QuadroVerde = new Rectangle(15, 15, larguraMolduraMarrom - 10, alturaMolduraMarrom - 10);
            Rectangle PreenchimentoDesfazer = new Rectangle(5, alturaMolduraMarrom + 25, larguraPreenchimentoBotoes, alturaPreenchimentoBotoes);
            Rectangle PreenchimentoRefazer = new Rectangle(larguraPreenchimentoBotoes + 15, alturaMolduraMarrom + 25, larguraPreenchimentoBotoes, alturaPreenchimentoBotoes);
            Rectangle PreenchimentoResetar = new Rectangle((2 * larguraPreenchimentoBotoes + 25), alturaMolduraMarrom + 25, larguraPreenchimentoBotoes, alturaPreenchimentoBotoes);
            Rectangle PreenchimentoEnviar = new Rectangle((3 * larguraPreenchimentoBotoes + 35), alturaMolduraMarrom + 25, larguraPreenchimentoBotoes, alturaPreenchimentoBotoes);
            Rectangle PreenchimentoTirarFoto = new Rectangle(larguraMolduraPreta + 5, alturaCamera + 15, (larguraCamera - 30), (alturaBotao / 2) - 10);
            Rectangle Reconhecimento = new Rectangle(4 * larguraPreenchimentoBotoes + 45, alturaMolduraMarrom + 25, larguraMolduraPreta - (4 * larguraBotao), alturaPreenchimentoBotoes);
            Rectangle EspaçoCamera = new Rectangle(larguraMolduraPreta, 0, larguraCamera, alturaCamera + 10);
            Rectangle AreaParaDesenhar = new Rectangle(larguraMolduraPreta + 5, alturaCamera + (alturaBotao / 2) + 15, (larguraCamera - 30), bmp.Height - alturaCamera - (alturaBotao / 2) - 20);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            

            //Escritas//
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            SolidBrush drawBrush = new SolidBrush(Color.AliceBlue);

            String textoDesfazer = "←";
            Font fontDesfazer = new Font("Arial", 90);
            PointF posicionamentoDesfazer = new PointF(50, alturaMolduraMarrom + 50);
                    
            String textoRefazer = "→";
            Font fontRefazer = new Font("Arial", 90);
            PointF posicionamentoRefazer = new PointF((larguraPreenchimentoBotoes + 60), alturaMolduraMarrom + 50); 

            String textoResetar = "Resetar";
            Font fontResetar = new Font("Arial", 40);
            PointF posicionamentoResetar = new PointF((2 * larguraPreenchimentoBotoes + 70), alturaMolduraMarrom + 100);

            String textoEnviar = "Enviar";
            Font fontEnviar = new Font("Arial", 40);
            PointF posicionamentoEnviar = new PointF((3 * larguraPreenchimentoBotoes + 80), alturaMolduraMarrom + 100);
            
                    
            
            
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Layout//
            ////////////////////////////////////////////////////////////////////
            g.DrawRectangle(CanetaPreta, RetanguloPretoTela);
            g.DrawRectangle(CanetaMarrom, MolduraQuadro);
            g.FillRectangle(FundoVerde, QuadroVerde);
            g.DrawRectangle(CanetaPreta, MolduraPretaMoldura);
            g.FillRectangle(GradientBotoes, Gradient);
            g.DrawRectangle(CanetaPreta, Desfazer);
            g.DrawRectangle(CanetaPreta, Refazer);
            g.DrawRectangle(CanetaPreta, Resetar);
            g.DrawRectangle(CanetaPreta, Enviar);
            g.DrawRectangle(CanetaPreta, TirarFoto);
            g.FillRectangle(FundoPretoReconhecimento, Reconhecimento);
            // g.FillRectangle(FundoCinzaPreenchimento, PreenchimentoDesfazer);
            // g.FillRectangle(FundoCinzaPreenchimento, PreenchimentoRefazer);
            // g.FillRectangle(FundoCinzaPreenchimento, PreenchimentoResetar);
            // g.FillRectangle(FundoCinzaPreenchimento, PreenchimentoEnviar);
            g.DrawRectangle(CanetaPreta,EspaçoCamera);
            g.FillRectangle(FundoCinzaPreenchimento, AreaParaDesenhar);
            g.FillRectangle(GradientBotaoTirarFoto, PreenchimentoTirarFoto);

            g.DrawString(textoDesfazer, fontDesfazer, drawBrush, posicionamentoDesfazer);
            g.DrawString(textoRefazer, fontRefazer, drawBrush, posicionamentoRefazer);
            g.DrawString(textoResetar, fontResetar, drawBrush, posicionamentoResetar);
            g.DrawString(textoEnviar, fontEnviar, drawBrush, posicionamentoEnviar);
            
            // Create rectangle for displaying image.
            RectangleF destRect = new RectangleF((bmp.Width - 5) - larguraCamera + 30, 5, larguraCamera - 30, alturaCamera);    
            // Create rectangle for source image.
            RectangleF srcRect = new RectangleF(0.0F, 0.0F, cam.Width, cam.Height);
            GraphicsUnit units = GraphicsUnit.Pixel;      
            // Draw image to screen.
            g.DrawImage(cam, destRect, srcRect, units);
        }
        catch
        {
            
        }
    }
}