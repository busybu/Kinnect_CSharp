using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class VisualizarNotas : Tela
{
    public List<Point> Points = new List<Point>();

    private bool botaoSairClicked = false;

    public event Action QuestionsDatabaseOpen;

    public event Action HomeOpen;

    public override void Desenhar(Bitmap cam, Bitmap bmp, Graphics g,
        Point cursor, bool isDown, string text)
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

            Rectangle referenciaCards = new Rectangle(larguraBotoesNavbar,0,bmp.Width - larguraBotoesNavbar,bmp.Height);
            Rectangle margemEsquerda = new Rectangle(larguraBotoesNavbar, 0, (int)(0.100 * bmp.Width), bmp.Height);
            Rectangle margemDireita = new Rectangle(bmp.Width - (int)(0.100 * bmp.Width), 0, (int)(0.100 * bmp.Width), bmp.Height);
            Rectangle margemCima = new Rectangle(larguraBotoesNavbar, 0, bmp.Width - larguraBotoesNavbar, (int)(0.075 * bmp.Width));
            Rectangle margemBaixo = new Rectangle(larguraBotoesNavbar, bmp.Height - (int)(0.075 * bmp.Width), bmp.Width - larguraBotoesNavbar, (int)(0.075 * bmp.Width));

            int finalMargemEsquerda =  larguraBotoesNavbar + (int)(0.100 * bmp.Width);
            int finalMargemDireita =  bmp.Width - (int)(0.100 * bmp.Width);
            int finalMargemCima =  (int)(0.075 * bmp.Width);
            int finalMargemBaixo = bmp.Height - (int)(0.075 * bmp.Width);

            Rectangle Turma = new Rectangle(finalMargemEsquerda, finalMargemCima, larguraTurmas, alturaTurmas);

            Rectangle Turma2 = new Rectangle(finalMargemDireita - larguraTurmas, finalMargemCima, larguraTurmas, alturaTurmas);

            Rectangle Turma3 = new Rectangle(finalMargemEsquerda, finalMargemBaixo - alturaTurmas, larguraTurmas, alturaTurmas);

            Rectangle Turma4 = new Rectangle(finalMargemDireita - larguraTurmas, finalMargemBaixo - alturaTurmas, larguraTurmas, alturaTurmas);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Preenchimentos
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle PreeBotaoBancoQuestoes = new Rectangle(5, 5, larguraBotoesNavbar - 10, alturaBotoesNavbar - 10);
            Rectangle PreeBotaoVisualizarNotas = new Rectangle(5, alturaBotoesNavbar + 5, larguraBotoesNavbar - 10, alturaBotoesNavbar - 10);
            Rectangle PreeBotaoSairX = new Rectangle(5, alturaBotoesNavbar * 2 + 5, larguraBotoesNavbar - 10, alturaBotoesNavbar - 10);

            Rectangle PreeTurmaA = new Rectangle(finalMargemEsquerda + 5, finalMargemCima + 5, larguraTurmas - 10, alturaTurmas - 10);
            Rectangle PreeTurmaB = new Rectangle(finalMargemDireita - larguraTurmas + 5, finalMargemCima + 5, larguraTurmas - 10, alturaTurmas - 10);
            Rectangle PreeTurmaC = new Rectangle(finalMargemEsquerda + 5, finalMargemBaixo - alturaTurmas + 5, larguraTurmas - 10, alturaTurmas - 10);
            Rectangle PreeTurmaD = new Rectangle(finalMargemDireita - larguraTurmas + 5, finalMargemBaixo - alturaTurmas + 5, larguraTurmas - 10, alturaTurmas - 10);

            Brush GradientAzulTurmas = new LinearGradientBrush(
                new Point(finalMargemEsquerda * 2 + finalMargemDireita, alturaTurmas),
                new Point(larguraTurmas, alturaTurmas),
                Color.FromArgb(255,42,72,88),  
                Color.FromArgb(255,8,159,143));
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
             Brush GradientBotaoBanco = new LinearGradientBrush(
                new Point(0, alturaBotoesNavbar),
                new Point(larguraBotoesNavbar, alturaBotoesNavbar),
                Color.FromArgb(255,110,0,0),  
                Color.FromArgb(255,220,0,0));

            g.FillRectangle(GradientCorAzul, GradientAzul);
            g.FillRectangle(GradientBotaoBanco, GradientVermelho);

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
                g.FillRectangle(GradientBotaoBanco, PreeBotaoBancoQuestoes);
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
                g.FillRectangle(GradientBotaoBanco, PreeBotaoSairX);
                if(PreeBotaoSairX.Contains(cursor) && isDown)
                {
                    botaoSairClicked = true;
                    g.FillRectangle(Brushes.DarkRed, PreeBotaoSairX);
                }

                if (PreeBotaoSairX.Contains(cursor) && !isDown && botaoSairClicked)
                {
                    botaoSairClicked = false;
                    HomeOpen();
                }
            }


            // g.DrawRectangle(CanetaVermelhaTeste, margemEsquerda);
            // g.DrawRectangle(CanetaVermelhaTeste, margemDireita);
            // g.DrawRectangle(CanetaVermelhaTeste, margemCima);
            // g.DrawRectangle(CanetaVermelhaTeste, margemBaixo);


            g.DrawRectangle(CanetaPreta, Turma);
            g.DrawRectangle(CanetaPreta, Turma2);
            g.DrawRectangle(CanetaPreta, Turma3);
            g.DrawRectangle(CanetaPreta, Turma4);

            g.FillRectangle(GradientAzulTurmas, PreeTurmaA);
            g.FillRectangle(GradientAzulTurmas, PreeTurmaB);
            g.FillRectangle(GradientAzulTurmas, PreeTurmaC);
            g.FillRectangle(GradientAzulTurmas, PreeTurmaD);


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



            Font fontTurmas = new Font(FontFamily.GetFamilies(g)[59], (int)(0.075 * bmp.Height));
        
            String TurmaA = "Turma A";

            String TurmaB = "Turma B";
            
            String TurmaC = "Turma C";

            String TurmaD = "Turma D";
           
            g.DrawString(TurmaA, fontTurmas, LetraBranca, Turma, format);
            g.DrawString(TurmaB, fontTurmas, LetraBranca, Turma2, format);
            g.DrawString(TurmaC, fontTurmas, LetraBranca, Turma3, format);
            g.DrawString(TurmaD, fontTurmas, LetraBranca, Turma4, format);

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