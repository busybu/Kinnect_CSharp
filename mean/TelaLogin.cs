using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class Login : Tela
{
    public List<Point> Points = new List<Point>();

    private bool CampoLoginClicked = false;

    public event Action CadastrarOpen;

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
            Rectangle PreenchimentoRetanguloPretoTela = new Rectangle(5, 5, bmp.Width - 10, bmp.Height - 10);

            Rectangle BotaoAluno = new Rectangle(50, 100, largurabotaoaluno, alturabotaoaluno);
            Rectangle BotaoProfessor = new Rectangle(50, ((bmp.Height - 100) - alturabotaoprofessor), largurabotaoprofessor, alturabotaoprofessor);

            Rectangle PreenchimentoBotaoAluno = new Rectangle(55, 105, largurabotaoaluno - 10, alturabotaoaluno - 10);
            Rectangle PreenchimentoBotaoProfessor = new Rectangle(55, ((bmp.Height - 100) - alturabotaoprofessor + 5), largurabotaoprofessor - 10, alturabotaoprofessor - 10);

            Rectangle BotaoLogin = new Rectangle(700, 300, largurabotaologin, alturabotaologin);
            Rectangle BotaoSenha = new Rectangle(700, 600, largurabotaosenha, alturabotaosenha);

            Rectangle PreenchimentoBotaoLogin = new Rectangle(705, 305, largurabotaologin - 10, alturabotaologin - 10);
            Rectangle PreenchimentoBotaoSenha = new Rectangle(705, 605, largurabotaosenha - 10, alturabotaosenha - 10);

            Rectangle MolduraLogin = new Rectangle(500, 50, larguramolduralogin, alturamolduralogin);
            Rectangle PreenchimentoMolduraLogin = new Rectangle(505, 55, larguramolduralogin - 10, alturamolduralogin - 10);

            //Gradient
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Brush GradientBotaoAzul = new LinearGradientBrush
            (
                new Point(0, alturabotaoprofessor),
                new Point(largurabotaoprofessor + 50, alturabotaoprofessor),
                Color.FromArgb(255, 42, 72, 88),
                Color.FromArgb(255, 8, 159, 143)
            );

            Brush GradientBotaoVermelho = new LinearGradientBrush(
                new Point(0 , alturabotaoprofessor),
                new Point(largurabotaoprofessor + 50, alturabotaoprofessor),
                Color.FromArgb(255,110,0,0),  
                Color.FromArgb(255,220,0,0));

            //Layout//
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            g.FillRectangle(FundoBranco, PreenchimentoRetanguloPretoTela);
            g.FillRectangle(FundoCinzaPreenchimento, PreenchimentoMolduraLogin);

            g.FillRectangle(GradientBotaoAzul, PreenchimentoBotaoAluno);
            g.FillRectangle(GradientBotaoAzul, PreenchimentoBotaoProfessor);


            g.DrawRectangle(CanetaPreta, RetanguloPretoTela);
            g.DrawRectangle(CanetaPreta, MolduraLogin);

            g.DrawRectangle(CanetaPreta, BotaoAluno);
            if (BotaoAluno.Contains(cursor))
            {
                g.FillRectangle(GradientBotaoVermelho, PreenchimentoBotaoAluno);
                if(BotaoAluno.Contains(cursor) && isDown)
                {
                    CampoLoginClicked = true;
                    g.FillRectangle(GradientBotaoVermelho, PreenchimentoBotaoAluno);
                }
            }
            g.DrawRectangle(CanetaPreta, BotaoProfessor);
            if (BotaoProfessor.Contains(cursor))
            {
                g.FillRectangle(GradientBotaoVermelho, PreenchimentoBotaoProfessor);
                if(BotaoProfessor.Contains(cursor) && isDown)
                {
                    CampoLoginClicked = true;
                    g.FillRectangle(GradientBotaoVermelho, PreenchimentoBotaoProfessor);
                }
            }
            

            //preenchimento

            g.FillRectangle(FundoBranco, PreenchimentoBotaoLogin);
            g.FillRectangle(FundoBranco, PreenchimentoBotaoSenha);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //Escritas//
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            SolidBrush drawBrush = new SolidBrush(Color.AliceBlue);
            SolidBrush letraPreta = new SolidBrush(Color.Black);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            StringFormat format2 = new StringFormat();
            format2.Alignment = StringAlignment.Near;
            format2.LineAlignment = StringAlignment.Center;

            String textoALUNO = "Sou           Aluno";
            Font fontAluno = new Font("Arial", (int)(0.050 * bmp.Height));

            String textoPROFESSOR = "Sou Professor";
            Font fontProfessor = new Font("Arial", (int)(0.050 * bmp.Height));

            float fontSize = 60f;

            String textoLogin = "Login:";
            Font fontLogin = new Font(FontFamily.GetFamilies(g)[59], fontSize);

            String inputLogin = text;

            String inputSenha = text;

            String textoSenha = "Senha:";
            Font fontSenha = new Font(FontFamily.GetFamilies(g)[59], fontSize);

            String textoCadastre = "Cadastre-se";
            Font fontTirarFoto = new Font(FontFamily.GetFamilies(g)[59], fontSize);

            g.DrawString(inputLogin, fontAluno, letraPreta, BotaoLogin, format2);
            g.DrawString(inputSenha, fontAluno, letraPreta, BotaoSenha, format2);

            g.DrawRectangle(CanetaPreta, BotaoLogin);

            g.DrawRectangle(CanetaPreta, BotaoSenha);
            

            g.DrawString(textoALUNO, fontAluno, drawBrush, BotaoAluno, format);
            g.DrawString(textoPROFESSOR, fontProfessor, drawBrush, BotaoProfessor, format);
            g.DrawString(textoLogin, fontLogin, drawBrush, new Rectangle(MolduraLogin.X + (int)(0.13 * MolduraLogin.Width), MolduraLogin.Y + (int)(0.15 * MolduraLogin.Height), MolduraLogin.Width, 200));

            g.DrawString(textoSenha, fontSenha, drawBrush, new Rectangle(MolduraLogin.X + (int)(0.13 * MolduraLogin.Width), MolduraLogin.Y + (int)(0.47 * MolduraLogin.Height), MolduraLogin.Width, 200));

            Rectangle retanguloCadastrar = new Rectangle(MolduraLogin.X + (int)(0.30 * MolduraLogin.Width), MolduraLogin.Y + (int)(0.85 * MolduraLogin.Height), MolduraLogin.Width, 200);
            g.DrawString(textoCadastre, fontTirarFoto, drawBrush, retanguloCadastrar);
            if (retanguloCadastrar.Contains(cursor))
            {
            
                if(retanguloCadastrar.Contains(cursor) && isDown)
                {
                    CampoLoginClicked = true;
                }

                if (retanguloCadastrar.Contains(cursor) && !isDown && CampoLoginClicked)
                {
                    CampoLoginClicked = false;
                    CadastrarOpen();
                }
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
        catch
        {

        }
    }
}