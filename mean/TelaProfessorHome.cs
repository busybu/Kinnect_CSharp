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

//             //Pinceis
//             Brush PreenchimentoPreto = Brushes.Black; 
//             Brush PreenchimentoCinza = Brushes.Gray;
            

//             //Variaveis para tamanhos e posicionamentos

//             int alturaNavbar = bmp.Height;
//             int larguraNavbar = (int)(0.170 * bmp.Width);

//             int alturaBotoesNavbar = (int)(bmp.Height / 3);
//             int larguraBotoesNavbar = larguraNavbar;

//             int alturaBotaoMaisEmenos = alturaBotoesNavbar / 2;
//             int larguraBotaoMaisEmenos = larguraBotoesNavbar / 2;




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
//             Rectangle Gradient = new Rectangle(5, 5, larguraBotoesNavbar, alturaBotoesNavbar * 3 - 10);
//             Brush GradientBotoes = new LinearGradientBrush(
//                 new Point(0, alturaBotoesNavbar),
//                 new Point(larguraBotoesNavbar, alturaBotoesNavbar),
//                 Color.FromArgb(255,42,72,88),  
//                 Color.FromArgb(255,8,159,143));

//             Brush GradientBotaoBanco = new LinearGradientBrush(
//                 new Point(0, alturaBotoesNavbar),
//                 new Point(larguraBotoesNavbar, alturaBotoesNavbar),
//                 Color.FromArgb(255,110,0,0),  
//                 Color.FromArgb(255,220,0,0));

//             g.FillRectangle(GradientBotoes, Gradient);
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
            
//             //Pintando
//             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//             // g.FillRectangle(PreenchimentoCinza, PreeBotaoBancoQuestoes);
           

            

//             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


//         }
//         catch
//         {
            
//         }
//     }
// }