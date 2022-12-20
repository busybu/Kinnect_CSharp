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
            Brush FundoPretoReconhecimento = Brushes.Black; // Brush exemplo = new LinearGradientBrush();
            Brush FundoCinzaPreenchimento = Brushes.Gray;

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
        
            //Area Clicaveis
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle QuadroVerde = new Rectangle(15, 15, larguraMolduraMarrom - 10, alturaMolduraMarrom - 10);
            Rectangle PreenchimentoDesfazer = new Rectangle(5, alturaMolduraMarrom + 25, larguraPreenchimentoBotoes, alturaPreenchimentoBotoes);
            Rectangle PreenchimentoRefazer = new Rectangle(larguraPreenchimentoBotoes + 15, alturaMolduraMarrom + 25, larguraPreenchimentoBotoes, alturaPreenchimentoBotoes);
            Rectangle PreenchimentoResetar = new Rectangle((2 * larguraPreenchimentoBotoes + 25), alturaMolduraMarrom + 25, larguraPreenchimentoBotoes, alturaPreenchimentoBotoes);
            Rectangle PreenchimentoEnviar = new Rectangle((3 * larguraPreenchimentoBotoes + 35), alturaMolduraMarrom + 25, larguraPreenchimentoBotoes, alturaPreenchimentoBotoes);
            Rectangle Reconhecimento = new Rectangle(4 * larguraPreenchimentoBotoes + 45, alturaMolduraMarrom + 25, larguraMolduraPreta - (4 * larguraBotao), alturaPreenchimentoBotoes);
            Rectangle EspaçoCamera = new Rectangle(larguraMolduraPreta, 0, larguraCamera, alturaCamera + 10);
            Rectangle AreaParaDesenhar = new Rectangle(larguraMolduraPreta + 5, alturaCamera + 15, (larguraCamera - 30), bmp.Height - alturaCamera - 20);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Layout//
            ////////////////////////////////////////////////////////////////////
            g.DrawRectangle(CanetaPreta, RetanguloPretoTela);
            g.DrawRectangle(CanetaMarrom, MolduraQuadro);
            g.FillRectangle(FundoVerde, QuadroVerde);
            g.DrawRectangle(CanetaPreta, MolduraPretaMoldura);
            g.DrawRectangle(CanetaPreta, Desfazer);
            g.DrawRectangle(CanetaPreta, Refazer);
            g.DrawRectangle(CanetaPreta, Resetar);
            g.DrawRectangle(CanetaPreta, Enviar);
            g.FillRectangle(FundoPretoReconhecimento, Reconhecimento);
            g.FillRectangle(FundoCinzaPreenchimento, PreenchimentoDesfazer);
            g.FillRectangle(FundoCinzaPreenchimento, PreenchimentoRefazer);
            g.FillRectangle(FundoCinzaPreenchimento, PreenchimentoResetar);
            g.FillRectangle(FundoCinzaPreenchimento, PreenchimentoEnviar);
            g.DrawRectangle(CanetaPreta,EspaçoCamera);
            g.FillRectangle(FundoCinzaPreenchimento, AreaParaDesenhar);
            
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