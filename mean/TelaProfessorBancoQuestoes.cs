using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public static class BancoQuestoes
{
    public static List<Point> Points = new List<Point>();

    public static void DesenharBanco(Bitmap cam, Bitmap bmp, Graphics g,
        Point cursor, bool isDown)
    {
        try
        {
            //Canetas
            Pen CanetaVermelhaTeste = new Pen(Brushes.Red, 10f);
            Pen CanetaPreta = new Pen(Brushes.Black, 10f);

            //Pinceis
            Brush PreenchimentoPreto = Brushes.Black; 
            Brush PreenchimentoCinza = Brushes.DarkGray;
            Brush PreenchimentoAzul = Brushes.LightBlue;
            Brush PreenchimentoVermelho = Brushes.IndianRed;
            Brush PreenchimentoVerde = Brushes.LightGreen;
            

            //Variaveis para tamanhos e posicionamentos

            int alturaNavbar = bmp.Height;
            int larguraNavbar = (int)(0.170 * bmp.Width);

            int alturaBotoesNavbar = (int)(bmp.Height / 3);
            int larguraBotoesNavbar = larguraNavbar;

            int alturaBotaoMaisEmenos = alturaBotoesNavbar / 2;
            int larguraBotaoMaisEmenos = larguraBotoesNavbar / 2;




            //Molduras
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle RetanguloPretoTela = new Rectangle(0, 0, bmp.Width, bmp.Height);
            Rectangle MolduraNavbar = new Rectangle(0, 0, larguraNavbar, alturaNavbar);
            Rectangle BotaoBancoQuestoes = new Rectangle(0, 0, larguraBotoesNavbar, alturaBotoesNavbar);
            Rectangle BotaoVisualizarNotas = new Rectangle(0, alturaBotoesNavbar, larguraBotoesNavbar, alturaBotoesNavbar);
            Rectangle BotaoSairX = new Rectangle(0, alturaBotoesNavbar * 2, larguraBotoesNavbar, alturaBotoesNavbar);
            Rectangle BotaoMais = new Rectangle(larguraBotoesNavbar, 0, larguraBotaoMaisEmenos, alturaBotaoMaisEmenos);
            Rectangle BotaoMenos = new Rectangle(larguraBotoesNavbar, alturaBotaoMaisEmenos, larguraBotaoMaisEmenos, alturaBotaoMaisEmenos);
            Rectangle BotaoConfirmar = new Rectangle(larguraBotoesNavbar, alturaBotaoMaisEmenos * 5, larguraBotaoMaisEmenos, alturaBotaoMaisEmenos);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Preenchimentos
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle PreeBotaoBancoQuestoes = new Rectangle(5, 5, larguraBotoesNavbar - 10, alturaBotoesNavbar - 10);
            Rectangle PreeBotaoVisualizarNotas = new Rectangle(5, alturaBotoesNavbar + 5, larguraBotoesNavbar - 10, alturaBotoesNavbar - 10);
            Rectangle PreeBotaoSairX = new Rectangle(5, alturaBotoesNavbar * 2 + 5, larguraBotoesNavbar - 10, alturaBotoesNavbar - 10);

            Rectangle PreeBotaoMais = new Rectangle(larguraBotoesNavbar + 5, 5, larguraBotaoMaisEmenos - 10, alturaBotaoMaisEmenos - 10);
            Rectangle PreeBotaoMenos = new Rectangle(larguraBotoesNavbar + 5, alturaBotaoMaisEmenos + 5, larguraBotaoMaisEmenos - 10, alturaBotaoMaisEmenos - 10);
            Rectangle PreeBotaoConfirmar = new Rectangle(larguraBotoesNavbar + 5, alturaBotaoMaisEmenos * 5 + 5, larguraBotaoMaisEmenos - 10, alturaBotaoMaisEmenos - 10);


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Gradient
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle Gradient = new Rectangle(5, 5, larguraBotoesNavbar, alturaBotoesNavbar * 3 - 10);
            Brush GradientBotoes = new LinearGradientBrush(
                new Point(0, alturaBotoesNavbar),
                new Point(larguraBotoesNavbar, alturaBotoesNavbar),
                Color.FromArgb(255,42,72,88),  
                Color.FromArgb(255,8,159,143));

            Rectangle GradientBanco = new Rectangle(5, 5, larguraBotoesNavbar, alturaBotoesNavbar - 10);
            Brush GradientBotaoBanco = new LinearGradientBrush(
                new Point(0, alturaBotoesNavbar),
                new Point(larguraBotoesNavbar, alturaBotoesNavbar),
                Color.FromArgb(255,110,0,0),  
                Color.FromArgb(255,220,0,0));

                // Color.FromArgb(255,30,200,100),  
                // Color.FromArgb(255,185,200,30));


            g.FillRectangle(GradientBotoes, Gradient);
            g.FillRectangle(GradientBotaoBanco, GradientBanco);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Pintando
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            g.FillRectangle(PreenchimentoAzul, PreeBotaoMais);
            g.FillRectangle(PreenchimentoVermelho, PreeBotaoMenos);
            g.FillRectangle(PreenchimentoVerde, PreeBotaoConfirmar);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Desenhando
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            g.DrawRectangle(CanetaPreta, RetanguloPretoTela);
            g.DrawRectangle(CanetaPreta, MolduraNavbar);

            //Botão Banco de Questões
            g.DrawRectangle(CanetaPreta, BotaoBancoQuestoes);
            if (PreeBotaoBancoQuestoes.Contains(cursor))
            {
                if(PreeBotaoBancoQuestoes.Contains(cursor) && isDown == true)
                    g.FillRectangle(Brushes.DarkRed, PreeBotaoBancoQuestoes);
            }

            //Botão Visualizar Notas
            g.DrawRectangle(CanetaPreta, BotaoVisualizarNotas);
            if (PreeBotaoVisualizarNotas.Contains(cursor))
            {
                g.FillRectangle(GradientBotaoBanco, PreeBotaoVisualizarNotas);
                if(PreeBotaoVisualizarNotas.Contains(cursor) && isDown == true)
                    g.FillRectangle(Brushes.DarkRed, PreeBotaoVisualizarNotas);
            }

            //Botão Sair
            g.DrawRectangle(CanetaPreta, BotaoSairX);
            if (BotaoSairX.Contains(cursor))
            {
                g.FillRectangle(GradientBotaoBanco, PreeBotaoSairX);
                if(PreeBotaoSairX.Contains(cursor) && isDown == true)
                    g.FillRectangle(Brushes.DarkRed, PreeBotaoSairX);
            }

            //Botão Mais
            g.DrawRectangle(CanetaPreta, BotaoMais);
            if (BotaoMais.Contains(cursor))
            {
                g.FillRectangle(Brushes.Gray, PreeBotaoMais);
                if(PreeBotaoMais.Contains(cursor) && isDown == true)
                    g.FillRectangle(Brushes.DimGray, PreeBotaoMais);
            }

            //Botão Menos
            g.DrawRectangle(CanetaPreta, BotaoMenos);
            if (BotaoMenos.Contains(cursor))
            {
                g.FillRectangle(Brushes.Gray, PreeBotaoMenos);
                if(PreeBotaoMenos.Contains(cursor) && isDown == true)
                    g.FillRectangle(Brushes.DimGray, PreeBotaoMenos);
            }

            //Botão Confirmar
            g.DrawRectangle(CanetaPreta, BotaoConfirmar);
            if (BotaoConfirmar.Contains(cursor))
            {
                g.FillRectangle(Brushes.Gray, PreeBotaoConfirmar);
                if(PreeBotaoConfirmar.Contains(cursor) && isDown == true)
                    g.FillRectangle(Brushes.DimGray, PreeBotaoConfirmar);
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Escritas
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            SolidBrush LetraPreta = new SolidBrush(Color.Black);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            String textoBanco = "Banco de Questões";
            Font fontBanco = new Font(FontFamily.GetFamilies(g)[59], alturaBotoesNavbar - (int)(0.200 * bmp.Height));
                    
            String textoVisualizar = "Visualizar Notas";
            Font fontVisualizar = new Font(FontFamily.GetFamilies(g)[59], alturaBotoesNavbar - (int)(0.200 * bmp.Height));
    
            String textoSair = "X";
            Font fontSair = new Font("Arial", alturaBotoesNavbar - (int)(0.200 * bmp.Height));
          
            String textoMais = "+";
            Font fontMais = new Font("Arial", alturaBotaoMaisEmenos - (int)(0.250 * bmp.Height));
            
            String textoMenos = "-";
            Font fontMenos = new Font("Arial", alturaBotaoMaisEmenos - (int)(0.250 * bmp.Height));

            String textoConfirmar = "Ok";
            Font fontConfirmar = new Font("Arial", 500f);

            g.DrawString(textoBanco, fontBanco, LetraPreta, BotaoBancoQuestoes, format);
            g.DrawString(textoVisualizar, fontVisualizar, LetraPreta, BotaoVisualizarNotas, format);
            g.DrawString(textoSair, fontSair, LetraPreta, BotaoSairX, format);
            g.DrawString(textoMais, fontMais, LetraPreta, BotaoMais, format);
            g.DrawString(textoMenos, fontMenos, LetraPreta, BotaoMenos, format);
            g.DrawString(textoConfirmar, fontConfirmar, LetraPreta, BotaoConfirmar, format);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        }
        catch
        {
            
        }
    }
}