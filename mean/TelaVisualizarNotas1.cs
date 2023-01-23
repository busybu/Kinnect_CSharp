using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class VisualizarNotas : Tela
{
    public List<Point> Points = new List<Point>();

    public event Action QuestionsDatabaseOpen;

    public event Action HomeOpen;

    public override void Desenhar(Bitmap cam, Bitmap bmp, Graphics g,
        Point cursor, bool isDown, string text, string valor)
    {
        g.Clear(Color.Transparent);
        try
        {
            //Canetas
            Pen CanetaVermelhaTeste = new Pen(Brushes.Red, 10f);
            Pen CanetaPreta = new Pen(Brushes.Black, 10f);
            Pen CanetaBranca = new Pen(Brushes.Transparent, 10f);


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

            //Referencia
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            int larguraEspacoParaQuestoes = bmp.Width - larguraBotoesNavbar - 20;
            int alturaEspacoParaQuestoes = bmp.Height - 20;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle EspacoParaQuestoes = new Rectangle(larguraBotoesNavbar + 10, 10, larguraEspacoParaQuestoes, alturaEspacoParaQuestoes);
            g.DrawRectangle(CanetaBranca, EspacoParaQuestoes);

            //Molduras
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle RetanguloPretoTela = new Rectangle(0, 0, bmp.Width, bmp.Height);
            Rectangle MolduraNavbar = new Rectangle(0, 0, larguraNavbar, alturaNavbar);

            Rectangle BotaoBancoQuestoes = new Rectangle(0, 0, larguraBotoesNavbar, alturaBotoesNavbar);
            Rectangle BotaoVisualizarNotas = new Rectangle(0, alturaBotoesNavbar, larguraBotoesNavbar, alturaBotoesNavbar);
            Rectangle BotaoSairX = new Rectangle(0, alturaBotoesNavbar * 2, larguraBotoesNavbar, alturaBotoesNavbar);

            int larguraTurmas = larguraEspacoParaQuestoes / 2 - (int)(0.150 * bmp.Width);
            int alturaTurmas = alturaEspacoParaQuestoes / 2 - (int)(0.100 * bmp.Width);

            Rectangle Turma = new Rectangle(larguraBotoesNavbar + (int)(0.100 * bmp.Width), (int)(0.150 * bmp.Height), larguraTurmas, alturaTurmas);

            Rectangle Turma2 = new Rectangle(larguraBotoesNavbar + larguraTurmas + (int)(0.200 * bmp.Width), (int)(0.150 * bmp.Height), larguraTurmas, alturaTurmas);

            Rectangle Turma3 = new Rectangle(larguraBotoesNavbar + (int)(0.100 * bmp.Width), alturaTurmas + (int)(0.250 * bmp.Height), larguraTurmas, alturaTurmas);

            Rectangle Turma4 = new Rectangle(larguraBotoesNavbar + larguraTurmas + (int)(0.200 * bmp.Width),alturaTurmas + (int)(0.250 * bmp.Height), larguraTurmas, alturaTurmas);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Preenchimentos
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle PreeBotaoBancoQuestoes = new Rectangle(5, 5, larguraBotoesNavbar - 10, alturaBotoesNavbar - 10);
            Rectangle PreeBotaoVisualizarNotas = new Rectangle(5, alturaBotoesNavbar + 5, larguraBotoesNavbar - 10, alturaBotoesNavbar - 10);
            Rectangle PreeBotaoSairX = new Rectangle(5, alturaBotoesNavbar * 2 + 5, larguraBotoesNavbar - 10, alturaBotoesNavbar - 10);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Gradient
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle GradientAzul = new Rectangle(5, 5, larguraBotoesNavbar, alturaBotoesNavbar * 3 - 10);
            Brush GradientCorAzul = new LinearGradientBrush(
                new Point(0, alturaBotoesNavbar),
                new Point(larguraBotoesNavbar, alturaBotoesNavbar),
                Color.FromArgb(255,42,72,88),  
                Color.FromArgb(255,8,159,143));

            Rectangle GradientVermelho = new Rectangle(5, 5 + alturaBotoesNavbar, larguraBotoesNavbar, alturaBotoesNavbar - 10);
            Brush GradientCorVermelha = new LinearGradientBrush(
                new Point(0, alturaBotoesNavbar),
                new Point(larguraBotoesNavbar, alturaBotoesNavbar),
                Color.FromArgb(255,110,0,0),  
                Color.FromArgb(255,220,0,0));

            g.FillRectangle(GradientCorAzul, GradientAzul);
            g.FillRectangle(GradientCorVermelha, GradientVermelho);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Pintando
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

           
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Desenhando
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            g.DrawRectangle(CanetaPreta, RetanguloPretoTela);
            g.DrawRectangle(CanetaPreta, MolduraNavbar);

            //Botão Banco de Questões
            g.DrawRectangle(CanetaPreta, BotaoBancoQuestoes);
            if (PreeBotaoBancoQuestoes.Contains(cursor))
            {
                g.FillRectangle(GradientCorVermelha, PreeBotaoBancoQuestoes);
                if(PreeBotaoBancoQuestoes.Contains(cursor) && isDown)
                {
                    g.FillRectangle(Brushes.DarkRed, PreeBotaoBancoQuestoes);
                    QuestionsDatabaseOpen();
                }
            }

            //Botão Visualizar Notas
            g.DrawRectangle(CanetaPreta, BotaoVisualizarNotas);
            if (PreeBotaoVisualizarNotas.Contains(cursor))
            {
                if(PreeBotaoVisualizarNotas.Contains(cursor) && isDown)
                    g.FillRectangle(Brushes.DarkRed, PreeBotaoVisualizarNotas);
            }

            //Botão Sair
            g.DrawRectangle(CanetaPreta, BotaoSairX);
            if (BotaoSairX.Contains(cursor))
            {
                g.FillRectangle(GradientCorVermelha, PreeBotaoSairX);
                if(PreeBotaoSairX.Contains(cursor) && isDown)
                {
                    g.FillRectangle(Brushes.DarkRed, PreeBotaoSairX);
                    HomeOpen();
                }
            }

            g.DrawRectangle(CanetaPreta, Turma);
            g.DrawRectangle(CanetaPreta, Turma2);
            g.DrawRectangle(CanetaPreta, Turma3);
            g.DrawRectangle(CanetaPreta, Turma4);


            //Escritas
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            SolidBrush LetraBranca = new SolidBrush(Color.AliceBlue);
            SolidBrush LetraPreta = new SolidBrush(Color.Black);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            StringFormat formatQuestao = new StringFormat();
            formatQuestao.Alignment = StringAlignment.Center;
            formatQuestao.LineAlignment = StringAlignment.Near;

            StringFormat formatValor = new StringFormat();
            formatValor.Alignment = StringAlignment.Center;
            formatValor.LineAlignment = StringAlignment.Far;

            String textoBanco = "Banco de Questões";
            Font fontBanco = new Font(FontFamily.GetFamilies(g)[59], (int)(0.048 * bmp.Height));
                    
            String textoVisualizar = "Visualizar Notas";
            Font fontVisualizar = new Font(FontFamily.GetFamilies(g)[59], (int)(0.048 * bmp.Height));
    
            String textoSair = "←";
            Font fontSair = new Font("Arial", (int)(0.100 * bmp.Height));
          

            g.DrawString(textoBanco, fontBanco, LetraBranca, BotaoBancoQuestoes, format);
            g.DrawString(textoVisualizar, fontVisualizar, LetraBranca, BotaoVisualizarNotas, format);
            g.DrawString(textoSair, fontSair, LetraBranca, BotaoSairX, format);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            





        }
        catch
        {
            
        }
    }
}