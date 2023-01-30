using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class BancoQuestoes : Tela
{
    private int question = 1;
    private bool botaoMaisClicked = false;
    private bool botaoMenosClicked = false;
    private bool botaoSairClicked = false;
    public List<Point> Points = new List<Point>();
    public event Action OnClearRequest;
    public event Action OnGradePageOpen;

    public event Action HomeOpen;

    public partial class Questao
    {
        public int NQuestao { get; set; }
        public string? Descricao { get; set; }
        // public int Idmodulo { get; set; }
        public int? Peso { get; set; }
    }

    Questao q = new Questao();

    public static List<Questao> questoes = new List<Questao>();

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

            int alturaBotaoMaisEmenos = alturaBotoesNavbar / 2;
            int larguraBotaoMaisEmenos = larguraBotoesNavbar / 2;


            //Molduras
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle RetanguloPretoTela = new Rectangle(0, 0, bmp.Width, bmp.Height);
            Rectangle MolduraNavbar = new Rectangle(0, 0, larguraNavbar, alturaNavbar);

            Rectangle BotaoBancoQuestoes = new Rectangle(0, 0, larguraBotoesNavbar, alturaBotoesNavbar);
            Rectangle BotaoVisualizarNotas = new Rectangle(0, alturaBotoesNavbar, larguraBotoesNavbar, alturaBotoesNavbar);
            Rectangle BotaoSairX = new Rectangle(0, alturaBotoesNavbar * 2, larguraBotoesNavbar, alturaBotoesNavbar);

            Rectangle BotaoMais = new Rectangle(bmp.Width - larguraBotaoMaisEmenos, 0, larguraBotaoMaisEmenos, alturaBotaoMaisEmenos);
            Rectangle BotaoMenos = new Rectangle(bmp.Width - larguraBotaoMaisEmenos, alturaBotaoMaisEmenos, larguraBotaoMaisEmenos, alturaBotaoMaisEmenos);
            Rectangle BotaoConfirmar = new Rectangle(bmp.Width - larguraBotaoMaisEmenos, alturaBotaoMaisEmenos * 5, larguraBotaoMaisEmenos, alturaBotaoMaisEmenos);

            Rectangle BotaoCima = new Rectangle(bmp.Width - larguraBotaoMaisEmenos, alturaBotaoMaisEmenos * 2 + 90, larguraBotaoMaisEmenos, alturaBotaoMaisEmenos);
            Rectangle BotaoBaixo = new Rectangle(bmp.Width - larguraBotaoMaisEmenos, alturaBotaoMaisEmenos * 3 + 90, larguraBotaoMaisEmenos, alturaBotaoMaisEmenos);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Preenchimentos
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle PreeBotaoBancoQuestoes = new Rectangle(5, 5, larguraBotoesNavbar - 10, alturaBotoesNavbar - 10);
            Rectangle PreeBotaoVisualizarNotas = new Rectangle(5, alturaBotoesNavbar + 5, larguraBotoesNavbar - 10, alturaBotoesNavbar - 10);
            Rectangle PreeBotaoSairX = new Rectangle(5, alturaBotoesNavbar * 2 + 5, larguraBotoesNavbar - 10, alturaBotoesNavbar - 10);

            Rectangle PreeBotaoCima = new Rectangle(bmp.Width - larguraBotaoMaisEmenos + 5, alturaBotaoMaisEmenos * 2 + 95, larguraBotaoMaisEmenos - 10, alturaBotaoMaisEmenos - 10);
            Rectangle PreeBotaoBaixo = new Rectangle(bmp.Width - larguraBotaoMaisEmenos + 5, alturaBotaoMaisEmenos * 3 + 95, larguraBotaoMaisEmenos - 10, alturaBotaoMaisEmenos - 10);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Gradient
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle GradientBotaoEscolhido = new Rectangle(5, 5, larguraBotoesNavbar, alturaBotoesNavbar * 3 - 10);
            Brush GradientBotoes = new LinearGradientBrush(
                new Point(0, alturaBotoesNavbar),
                new Point(larguraBotoesNavbar, alturaBotoesNavbar),
                Color.FromArgb(255,42,72,88),  
                Color.FromArgb(255,8,159,143));

            Rectangle GradientBancoeX = new Rectangle(5, 5, larguraBotoesNavbar, alturaBotoesNavbar - 10);
            Brush GradientBotaoBanco = new LinearGradientBrush(
                new Point(0, alturaBotoesNavbar),
                new Point(larguraBotoesNavbar, alturaBotoesNavbar),
                Color.FromArgb(255,110,0,0),  
                Color.FromArgb(255,220,0,0));



            Rectangle GradientBotaoMais = new Rectangle(bmp.Width - larguraBotaoMaisEmenos + 5, 5, larguraBotaoMaisEmenos - 10, alturaBotaoMaisEmenos - 10);
            Brush GradientBotaoAzul = new LinearGradientBrush(
                new Point(bmp.Width - larguraBotaoMaisEmenos, alturaBotaoMaisEmenos),
                new Point(larguraBotaoMaisEmenos, alturaBotaoMaisEmenos),
                Color.FromArgb(255,42,72,88),  
                Color.FromArgb(255,8,159,143));

            Rectangle GradientBotaoMenos = new Rectangle(bmp.Width - larguraBotaoMaisEmenos + 5, alturaBotaoMaisEmenos + 5, larguraBotaoMaisEmenos - 10, alturaBotaoMaisEmenos - 10);
            Brush GradientBotaoVermelho = new LinearGradientBrush(
                new Point(bmp.Width - larguraBotaoMaisEmenos, alturaBotaoMaisEmenos),
                new Point(larguraBotaoMaisEmenos, alturaBotaoMaisEmenos),
                Color.FromArgb(255,110,0,0),  
                Color.FromArgb(255,220,0,0));

            Rectangle GradientBotaoConfirmar = new Rectangle(bmp.Width - larguraBotaoMaisEmenos + 5, alturaBotaoMaisEmenos * 5 + 5, larguraBotaoMaisEmenos - 10, alturaBotaoMaisEmenos - 10);
            Brush GradientBotaoVerde = new LinearGradientBrush(
                new Point(bmp.Width - larguraBotaoMaisEmenos, alturaBotaoMaisEmenos),
                new Point(larguraBotaoMaisEmenos, alturaBotaoMaisEmenos),
                Color.FromArgb(255,70,150,70),  
                Color.FromArgb(255,100,200,100));


            g.FillRectangle(GradientBotoes, GradientBotaoEscolhido);
            g.FillRectangle(GradientBotaoBanco, GradientBancoeX);

            g.FillRectangle(Brushes.DarkCyan, GradientBotaoMais);
            g.FillRectangle(GradientBotaoVermelho, GradientBotaoMenos);
            g.FillRectangle(Brushes.ForestGreen, GradientBotaoConfirmar);

            g.FillRectangle(Brushes.White, PreeBotaoCima);
            g.FillRectangle(Brushes.White, PreeBotaoBaixo);
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
                if(PreeBotaoBancoQuestoes.Contains(cursor) && isDown)
                    g.FillRectangle(Brushes.DarkRed, PreeBotaoBancoQuestoes);
            }

            //Botão Visualizar Notas
            g.DrawRectangle(CanetaPreta, BotaoVisualizarNotas);
            if (PreeBotaoVisualizarNotas.Contains(cursor))
            {
                g.FillRectangle(GradientBotaoBanco, PreeBotaoVisualizarNotas);
                if(PreeBotaoVisualizarNotas.Contains(cursor) && isDown)
                {
                    g.FillRectangle(Brushes.DarkRed, PreeBotaoVisualizarNotas);
                    OnGradePageOpen();
                }
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

            //Botão Mais
            g.DrawRectangle(CanetaPreta, BotaoMais);
            if (BotaoMais.Contains(cursor))
            {
                g.FillRectangle(GradientBotaoAzul, GradientBotaoMais);
                if(GradientBotaoMais.Contains(cursor) && isDown)
                {
                    botaoMaisClicked = true;
                    g.FillRectangle(Brushes.DarkCyan, GradientBotaoMais);
                }

                if (GradientBotaoMais.Contains(cursor) && !isDown && botaoMaisClicked)
                {
                    botaoMaisClicked = false;
                    
                    q.Descricao = text;
                    q.NQuestao = question;
                    // q.Peso = valor;

                    questoes.Add(q);
                    question++;
                    OnClearRequest();
                }
            }

            //Botão Menos
            g.DrawRectangle(CanetaPreta, BotaoMenos);
            if (BotaoMenos.Contains(cursor))
            {
                g.FillRectangle(Brushes.Red, GradientBotaoMenos);
                if(GradientBotaoMenos.Contains(cursor) && isDown)
                {
                    botaoMenosClicked = true;
                    g.FillRectangle(GradientBotaoVermelho, GradientBotaoMenos);
                }

                if (GradientBotaoMenos.Contains(cursor) && !isDown && botaoMenosClicked)
                {
                    botaoMenosClicked = false;

                    questoes.Remove(q);
                    question--;
            
                    if(question <= 0)
                        question = 1;

                    OnClearRequest();
                }
            }

            //Botão Confirmar
            g.DrawRectangle(CanetaPreta, BotaoConfirmar);
            if (BotaoConfirmar.Contains(cursor))
            {
                g.FillRectangle(GradientBotaoVerde, GradientBotaoConfirmar);
                if(GradientBotaoConfirmar.Contains(cursor) && isDown)
                    g.FillRectangle(Brushes.ForestGreen, GradientBotaoConfirmar);
            }

            //Botão Cima
            g.DrawRectangle(CanetaPreta, BotaoCima);
            if (BotaoCima.Contains(cursor))
            {
                g.FillRectangle(Brushes.LightGray, PreeBotaoCima);
                if(PreeBotaoCima.Contains(cursor) && isDown)
                {
                    g.FillRectangle(Brushes.Gray, PreeBotaoCima);

                    OnClearRequest();
                    // questoes[]
                
                }
            }

            //Botão Baixo
            g.DrawRectangle(CanetaPreta, BotaoBaixo);
            if (BotaoBaixo.Contains(cursor))
            {
                g.FillRectangle(Brushes.LightGray, PreeBotaoBaixo);
                if(PreeBotaoBaixo.Contains(cursor) && isDown)
                    g.FillRectangle(Brushes.Gray, PreeBotaoBaixo);
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
          
            String textoMais = "+";
            Font fontMais = new Font("Arial", (int)(0.100 * bmp.Height));
            
            String textoMenos = "-";
            Font fontMenos = new Font("Arial", (int)(0.100 * bmp.Height));

            String textoConfirmar = "✓";
            Font fontConfirmar = new Font("Arial", (int)(0.040 * bmp.Height));

            String textoCima = "↑";
            Font fontCima = new Font("Arial", (int)(0.100 * bmp.Height));

            String textoBaixo = "↓";
            Font fontBaixo = new Font("Arial", (int)(0.100 * bmp.Height));

            g.DrawString(textoBanco, fontBanco, LetraBranca, BotaoBancoQuestoes, format);
            g.DrawString(textoVisualizar, fontVisualizar, LetraBranca, BotaoVisualizarNotas, format);
            g.DrawString(textoSair, fontSair, LetraBranca, BotaoSairX, format);
            g.DrawString(textoMais, fontMais, LetraBranca, BotaoMais, format);
            g.DrawString(textoMenos, fontMenos, LetraBranca, BotaoMenos, format);
            g.DrawString(textoConfirmar, fontConfirmar, LetraBranca, GradientBotaoConfirmar, format);

            g.DrawString(textoCima, fontCima, LetraPreta, BotaoCima, format);
            g.DrawString(textoBaixo, fontBaixo, LetraPreta, BotaoBaixo, format);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            //Retangulo Referencia
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            int larguraEspacoParaQuestoes = bmp.Width - larguraBotaoMaisEmenos - larguraBotoesNavbar - 20;
            int alturaEspacoParaQuestoes = bmp.Height - 20;

            Rectangle EspacoParaQuestoes = new Rectangle(larguraBotoesNavbar + 10, 10, larguraEspacoParaQuestoes, alturaEspacoParaQuestoes);
            g.DrawRectangle(CanetaBranca, EspacoParaQuestoes);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            //Questões
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            String NumQuestao = $"Questão {question}";
            Font fontNumQuestao = new Font("Arial", (int)(0.050 * bmp.Height));

            String ValorQuestão = "Valor da Questão: ";
            Font fontValor = new Font("Arial", (int)(0.050 * bmp.Height));

            String Questao = text;
            Font fontQuestao = new Font("Arial", (int)(0.050 * bmp.Height));

            g.DrawString(NumQuestao, fontNumQuestao, LetraPreta, EspacoParaQuestoes, formatQuestao);
            g.DrawString(ValorQuestão, fontValor, LetraPreta, EspacoParaQuestoes, formatValor);

            g.DrawString(Questao, fontQuestao, LetraPreta, EspacoParaQuestoes, format);
            



            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




        }
        catch
        {
            
        }
    }
}