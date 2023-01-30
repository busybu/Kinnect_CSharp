using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class Cadastro : Tela
{
    public List<Point> Points = new List<Point>();

    public override void Desenhar(Bitmap cam, Bitmap bmp, Graphics g,
        Point cursor, bool isDown, string text)
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
            int alturaMolduraMarrom = (int)(0.778f * bmp.Height);
            int larguraMolduraMarrom = (int)(0.688f * bmp.Width); 

            int alturabotaoaluno = (int)(0.200 * bmp.Height);
            int largurabotaoaluno = (int)(0.200 * bmp.Width); 

            int alturabotaoprofessor = (int)(0.200 * bmp.Height);
            int largurabotaoprofessor = (int)(0.200 * bmp.Width); 

            int alturamolduralogin = (int)(0.900 * bmp.Height);
            int larguramolduralogin = (int)(0.720 * bmp.Width);

            int alturabotaologin = (int)(0.100 * bmp.Height);
            int largurabotaologin = (int)(0.500 * bmp.Width);

            int alturabotaosenha = (int)(0.100 * bmp.Height);
            int largurabotaosenha = (int)(0.500 * bmp.Width);

            int alturabotaonome = (int)(0.100 * bmp.Height);
            int largurabotaonome = (int)(0.500 * bmp.Width);


            //Molduras
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle RetanguloPretoTela = new Rectangle(0, 0, bmp.Width, bmp.Height);
            Rectangle PreenchimentoRetanguloPretoTela = new Rectangle(5,5, bmp.Width - 10, bmp.Height - 10);

            Rectangle BotaoAluno = new Rectangle(50, 100, largurabotaoaluno, alturabotaoaluno);
            Rectangle BotaoProfessor = new Rectangle(50, ((bmp.Height - 100) - alturabotaoprofessor), largurabotaoprofessor, alturabotaoprofessor);

            Rectangle PreenchimentoBotaoAluno = new Rectangle(55, 105, largurabotaoaluno - 10, alturabotaoaluno - 10);
            Rectangle PreenchimentoBotaoProfessor = new Rectangle(55, ((bmp.Height - 100) - alturabotaoprofessor + 5), largurabotaoprofessor - 10, alturabotaoprofessor - 10);

            Rectangle BotaoLogin = new Rectangle((int)(0.350 * bmp.Width), (int)(0.500 * bmp.Height), largurabotaologin,alturabotaologin);

            Rectangle BotaoSenha = new Rectangle((int)(0.350 * bmp.Width), (int)(0.700 * bmp.Height), largurabotaosenha,alturabotaosenha);

            Rectangle BotaoNome = new Rectangle((int)(0.350 * bmp.Width), (int)(0.300 * bmp.Height), largurabotaonome,alturabotaonome);

            Rectangle MolduraLogin = new Rectangle((int)(0.250 * bmp.Width), (int)(0.050 * bmp.Height), larguramolduralogin, alturamolduralogin);
            Rectangle PreenchimentoMolduraLogin = new Rectangle((int)(0.250 * bmp.Width), (int)(0.050 * bmp.Height), larguramolduralogin, alturamolduralogin);

            

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
            g.DrawRectangle(CanetaVermelhaTeste, BotaoNome);
      
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

            String textoALUNO = "Sou Aluno";
            Font fontAluno = new Font("Arial", alturabotaoaluno - (int)(0.170 * bmp.Height));
                    
            String textoPROFESSOR = "Sou Professor";
            Font fontProfessor = new Font("Arial", alturabotaoprofessor - (int)(0.170 * bmp.Height));

            float fontSize = 60f;
            float fontmin = 40f;

            String textoTitulo = "CADASTRO";
            Font fontTitulo = new Font(FontFamily.GetFamilies(g)[59], fontSize);
    
            String textoNome = "Nome:";
            Font fontNome = new Font(FontFamily.GetFamilies(g)[59], fontmin);

            String textoEmail = "E-mail:";
            Font fontEmail = new Font(FontFamily.GetFamilies(g)[59], fontmin);
          
            String textoSenha = "Senha:";
            Font fontSenha = new Font(FontFamily.GetFamilies(g)[59],  fontmin);
            
            String textoEntre = "Entrar";
            Font fontEntrar = new Font(FontFamily.GetFamilies(g)[59],  fontSize);
            
            
            g.DrawString(textoALUNO, fontAluno, drawBrush, BotaoAluno, format);
            g.DrawString(textoPROFESSOR, fontProfessor, drawBrush, BotaoProfessor, format);

            g.DrawString(textoTitulo, fontTitulo, drawBrush,
                new Rectangle(MolduraLogin.X + (int)(0.30 * MolduraLogin.Width), MolduraLogin.Y + (int)(0.05 * MolduraLogin.Height), MolduraLogin.Width, 200));

            g.DrawString(textoNome, fontNome, drawBrush, 
                new Rectangle(MolduraLogin.X + (int)(0.14 * MolduraLogin.Width), MolduraLogin.Y + (int)(0.20 * MolduraLogin.Height), MolduraLogin.Width, 200));
            g.DrawString(textoEmail, fontEmail, drawBrush, 
                new Rectangle(MolduraLogin.X + (int)(0.14 * MolduraLogin.Width), MolduraLogin.Y + (int)(0.40 * MolduraLogin.Height), MolduraLogin.Width, 200));
            g.DrawString(textoSenha, fontSenha, drawBrush,
                new Rectangle(MolduraLogin.X + (int)(0.14 * MolduraLogin.Width), MolduraLogin.Y + (int)(0.61 * MolduraLogin.Height), MolduraLogin.Width, 200));
            g.DrawString(textoEntre, fontEntrar, drawBrush, 
                new Rectangle(MolduraLogin.X + (int)(0.38 * MolduraLogin.Width), MolduraLogin.Y + (int)(0.85 * MolduraLogin.Height), MolduraLogin.Width, 200));            
                
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
        catch
        {
            
        }
    }
}