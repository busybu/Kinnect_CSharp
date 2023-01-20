// using System.Drawing;
// using System.Windows.Forms;
// using System.Drawing.Drawing2D;

// public static class ProfessorHome
// {
//     public static List<Point> Points = new List<Point>();

//     public static void DesenharHome(Bitmap cam, Bitmap bmp, Graphics g,
//         Point cursor, bool isDown)
//     {
//         try
//         {
//             //Canetas
//             Pen CanetaVermelhaTeste = new Pen(Brushes.Red, 10f);
//             Pen CanetaPreta = new Pen(Brushes.Black, 10f);
//             Pen CanetaBranca = new Pen(Brushes.Transparent, 10f);

//             //Pinceis
//             Brush PreenchimentoPreto = Brushes.Black; 
//             Brush PreenchimentoCinza = Brushes.DarkGray;

//             //Variaveis para tamanhos e posicionamentos

//             int alturaNavbar = bmp.Height;
//             int larguraNavbar = (int)(0.170 * bmp.Width);

//             int alturaBotoesNavbar = (int)(bmp.Height / 3);
//             int larguraBotoesNavbar = larguraNavbar;

//             //Molduras
//             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//             Rectangle RetanguloPretoTela = new Rectangle(0, 0, bmp.Width, bmp.Height);
//             Rectangle MolduraNavbar = new Rectangle(0, 0, larguraNavbar, alturaNavbar);

//             Rectangle BotaoBancoQuestoes = new Rectangle(0, 0, larguraBotoesNavbar, alturaBotoesNavbar);
//             Rectangle BotaoVisualizarNotas = new Rectangle(0, alturaBotoesNavbar, larguraBotoesNavbar, alturaBotoesNavbar);
//             Rectangle BotaoSairX = new Rectangle(0, alturaBotoesNavbar * 2, larguraBotoesNavbar, alturaBotoesNavbar);
//             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//             //Preenchimentos
//             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//             Rectangle PreeBotaoBancoQuestoes = new Rectangle(5, 5, larguraBotoesNavbar - 10, alturaBotoesNavbar - 10);
//             Rectangle PreeBotaoVisualizarNotas = new Rectangle(5, alturaBotoesNavbar + 5, larguraBotoesNavbar - 10, alturaBotoesNavbar - 10);
//             Rectangle PreeBotaoSairX = new Rectangle(5, alturaBotoesNavbar * 2 + 5, larguraBotoesNavbar - 10, alturaBotoesNavbar - 10);

//             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//             //Gradient
//             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//             Rectangle GradientAzul = new Rectangle(5, 5, larguraBotoesNavbar, alturaBotoesNavbar * 3 - 10);
//             Brush GradientBotoes = new LinearGradientBrush(
//                 new Point(0, alturaBotoesNavbar),
//                 new Point(larguraBotoesNavbar, alturaBotoesNavbar),
//                 Color.FromArgb(255,42,72,88),  
//                 Color.FromArgb(255,8,159,143));

//             Rectangle GradientVermelho = new Rectangle(5, 5, larguraBotoesNavbar, alturaBotoesNavbar - 10);
//             Brush GradientBotaoBanco = new LinearGradientBrush(
//                 new Point(0, alturaBotoesNavbar),
//                 new Point(larguraBotoesNavbar, alturaBotoesNavbar),
//                 Color.FromArgb(255,110,0,0),  
//                 Color.FromArgb(255,220,0,0));

//             g.FillRectangle(GradientBotoes, GradientAzul);
//             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//             //Desenhando
//             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//             g.DrawRectangle(CanetaPreta, RetanguloPretoTela);
//             g.DrawRectangle(CanetaPreta, MolduraNavbar);

//             //Botão Banco de Questões
//             g.DrawRectangle(CanetaPreta, BotaoBancoQuestoes);
//             if (PreeBotaoBancoQuestoes.Contains(cursor))
//             {
//                 g.FillRectangle(GradientBotaoBanco, PreeBotaoBancoQuestoes);
//                 if(PreeBotaoBancoQuestoes.Contains(cursor) && isDown == true)
//                     g.FillRectangle(Brushes.DarkRed, PreeBotaoBancoQuestoes);
//             }

//             //Botão Visualizar Notas
//             g.DrawRectangle(CanetaPreta, BotaoVisualizarNotas);
//             if (PreeBotaoVisualizarNotas.Contains(cursor))
//             {
//                 g.FillRectangle(GradientBotaoBanco, PreeBotaoVisualizarNotas);
//                 if(PreeBotaoVisualizarNotas.Contains(cursor) && isDown == true)
//                     g.FillRectangle(Brushes.DarkRed, PreeBotaoVisualizarNotas);
//             }

//             //Botão Sair
//             g.DrawRectangle(CanetaPreta, BotaoSairX);
//             if (BotaoSairX.Contains(cursor))
//             {
//                 g.FillRectangle(GradientBotaoBanco, PreeBotaoSairX);
//                 if(PreeBotaoSairX.Contains(cursor) && isDown == true)
//                     g.FillRectangle(Brushes.DarkRed, PreeBotaoSairX);
//             }
//             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//             //Escritas
//             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//             SolidBrush LetraBranca = new SolidBrush(Color.AliceBlue);

//             StringFormat format = new StringFormat();
//             format.Alignment = StringAlignment.Center;
//             format.LineAlignment = StringAlignment.Center;

//             String textoBanco = "Banco de Questões";
//             Font fontBanco = new Font(FontFamily.GetFamilies(g)[59], (int)(0.048 * bmp.Height));
                    
//             String textoVisualizar = "Visualizar Notas";
//             Font fontVisualizar = new Font(FontFamily.GetFamilies(g)[59], (int)(0.048 * bmp.Height));
    
//             String textoSair = "X";
//             Font fontSair = new Font("Arial", (int)(0.100 * bmp.Height));

//             g.DrawString(textoBanco, fontBanco, LetraBranca, BotaoBancoQuestoes, format);
//             g.DrawString(textoVisualizar, fontVisualizar, LetraBranca, BotaoVisualizarNotas, format);
//             g.DrawString(textoSair, fontSair, LetraBranca, BotaoSairX, format);
//             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//             //Bem Vindo
//             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//             //Retangulo Referencia
//             int larguraRetanguloReferencia = bmp.Width - larguraBotoesNavbar - 20;
//             int alturaRetanguloReferencia = bmp.Height - 20;
//             Rectangle RetanguloReferencia = new Rectangle(larguraBotoesNavbar + 10, 10, larguraRetanguloReferencia, alturaRetanguloReferencia);

//             //Texto Bem Vindo
//             SolidBrush LetraPreta = new SolidBrush(Color.Black);

//             String BemVindo = "Bem vindo professor Trevisan";
//             Font fontQuestao = new Font("Arial", (int)(0.100 * bmp.Height));

//             g.DrawString(BemVindo, fontQuestao, LetraPreta, RetanguloReferencia, format);
//             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


//         }
//         catch
//         {
            
//         }
//     }
// }