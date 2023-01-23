using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public static class Login
{
    public static List<Point> Points = new List<Point>();

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
            Brush FundoBranco = Brushes.White;
            

            //Variaveis para tamanhos e posicionamentos
            int alturabotaoaluno = (int)(0.300 * bmp.Height);
            int largurabotaoaluno = (int)(0.200 * bmp.Width); 

            int alturabotaoprofessor = (int)(0.300 * bmp.Height);
            int largurabotaoprofessor = (int)(0.200 * bmp.Width); 

            int alturamolduralogin = (int)(0.900 * bmp.Height);
            int larguramolduralogin = (int)(0.720 * bmp.Width);

            int alturabotaologin = (int)(0.100 * bmp.Height);
            int largurabotaologin = (int)(0.500 * bmp.Width);

            int alturabotaosenha = (int)(0.100 * bmp.Height);
            int largurabotaosenha = (int)(0.500 * bmp.Width);


            //Molduras
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle RetanguloPretoTela = new Rectangle(0, 0, bmp.Width, bmp.Height);
            Rectangle PreenchimentoRetanguloPretoTela = new Rectangle(5,5, bmp.Width - 10, bmp.Height - 10);


            Rectangle BotaoAluno = new Rectangle(50, 100, largurabotaoaluno, alturabotaoaluno);
            Rectangle BotaoProfessor = new Rectangle(50, ((bmp.Height - 100) - alturabotaoprofessor), largurabotaoprofessor, alturabotaoprofessor);

            Rectangle PreenchimentoBotaoAluno = new Rectangle(55, 105, largurabotaoaluno - 10, alturabotaoaluno - 10);
            Rectangle PreenchimentoBotaoProfessor = new Rectangle(55, ((bmp.Height - 100) - alturabotaoprofessor + 5), largurabotaoprofessor - 10, alturabotaoprofessor - 10);

            Rectangle BotaoLogin = new Rectangle(700, 300, largurabotaologin,alturabotaologin);

            Rectangle BotaoSenha = new Rectangle(700, 600, largurabotaosenha,alturabotaosenha);

            Rectangle MolduraLogin = new Rectangle(500, 50, larguramolduralogin, alturamolduralogin);
            Rectangle PreenchimentoMolduraLogin = new Rectangle(505, 55, larguramolduralogin - 10, alturamolduralogin - 10);

            

            //Layout//
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            g.FillRectangle(FundoCinzaPreenchimento, PreenchimentoRetanguloPretoTela);
            g.FillRectangle(FundoBranco, PreenchimentoMolduraLogin);


            g.DrawRectangle(CanetaPreta, RetanguloPretoTela);
            g.DrawRectangle(CanetaPreta, BotaoAluno);
            g.DrawRectangle(CanetaPreta, BotaoProfessor);
            g.DrawRectangle(CanetaPreta, MolduraLogin);
            g.DrawRectangle(CanetaVermelhaTeste, BotaoLogin);
            g.DrawRectangle(CanetaVermelhaTeste, BotaoSenha);
      
            // g.FillRectangle(FundoVerde, QuadroVerde);

            //preenchimento
            
            g.FillRectangle(FundoBranco, PreenchimentoBotaoProfessor);
            g.FillRectangle(FundoBranco, PreenchimentoBotaoAluno);
           
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //Escritas//
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            SolidBrush drawBrush = new SolidBrush(Color.Red);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            StringFormat formatLoginEsenha = new StringFormat();
            formatLoginEsenha.Alignment = StringAlignment.Near;
            formatLoginEsenha.LineAlignment = StringAlignment.Near;

            String textoALUNO = "SOU ALUNO";
            Font fontAluno = new Font(FontFamily.GetFamilies(g)[59], (int)(0.046 * bmp.Height));
                    
            String textoPROFESSOR = "SOU PROFESSOR";
            Font fontProfessor = new Font(FontFamily.GetFamilies(g)[59], (int)(0.046 * bmp.Height));
    
            String textoLogin = "Login:";
            Font fontLogin = new Font(FontFamily.GetFamilies(g)[59], (int)(0.90 * bmp.Height));
          
            String textoSenha = "Senha:";
            Font fontSenha = new Font(FontFamily.GetFamilies(g)[59], (int)(0.90 * bmp.Height));
            
            // String textoCadastre = "Cadastre-se";
            // Font fontTirarFoto = new Font(FontFamily.GetFamilies(g)[59], alturamolduralogin  - (int)(0.800 * bmp.Height));
            
            
            g.DrawString(textoALUNO, fontAluno, drawBrush, BotaoAluno, format);
            g.DrawString(textoPROFESSOR, fontProfessor, drawBrush, BotaoProfessor, format);
            g.DrawString(textoLogin, fontLogin, drawBrush, BotaoLogin, formatLoginEsenha);
            g.DrawString(textoSenha, fontSenha, drawBrush, BotaoSenha,  formatLoginEsenha);
            // g.DrawString(textoCadastre, fontTirarFoto, drawBrush, 0.700f, 0.700f);            
                
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
        catch
        {
            
        }
    }
}