using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class Login : Tela
{
    public List<Point> Points = new List<Point>();

    private bool CampoClicked = false;
    private int selectedField = 0;

    public event Action CadastrarOpen;

    private String login = "";
    private String senha = "";

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

            int alturabotaologin = (int)(0.085 * bmp.Height);
            int largurabotaologin = (int)(0.485 * bmp.Width);

            int alturabotaosenha = (int)(0.085 * bmp.Height);
            int largurabotaosenha = (int)(0.485 * bmp.Width);


            //Molduras
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Rectangle RetanguloPretoTela = new Rectangle(0, 0, bmp.Width, bmp.Height);
            Rectangle PreenchimentoRetanguloPretoTela = new Rectangle(5, 5, bmp.Width - 10, bmp.Height - 10);

            Rectangle BotaoAluno = new Rectangle((int)(0.030 * bmp.Width), (int)(0.100 * bmp.Height), largurabotaoaluno - 5, alturabotaoaluno - 5);
            Rectangle BotaoProfessor = new Rectangle((int)(0.030 * bmp.Width), ((bmp.Height - (int)(0.100 * bmp.Height)) - alturabotaoprofessor), largurabotaoprofessor - 5, alturabotaoprofessor - 5);

            Rectangle PreenchimentoBotaoAluno = new Rectangle((int)(0.034 * bmp.Width), (int)(0.107 * bmp.Height), largurabotaoaluno - 15, alturabotaoaluno - 15);
            Rectangle PreenchimentoBotaoProfessor = new Rectangle((int)(0.034 * bmp.Width), ((bmp.Height - (int)(0.094 * bmp.Height)) - alturabotaoprofessor), largurabotaoprofessor - 15, alturabotaoprofessor - 15);

            Rectangle BotaoLogin = new Rectangle((int)(0.350 * bmp.Width), (int)(0.305 * bmp.Height), largurabotaologin - 5, alturabotaologin - 10);
            Rectangle BotaoSenha = new Rectangle((int)(0.350 * bmp.Width), (int)(0.600 * bmp.Height), largurabotaosenha - 5, alturabotaosenha - 10);

            Rectangle PreenchimentoBotaoLogin = new Rectangle((int)(0.350 * bmp.Width), (int)(0.300 * bmp.Height), largurabotaologin - 10, alturabotaologin - 10);
            Rectangle PreenchimentoBotaoSenha = new Rectangle((int)(0.350 * bmp.Width), (int)(0.600 * bmp.Height), largurabotaosenha - 10, alturabotaosenha - 10);

            Rectangle MolduraLogin = new Rectangle((int)(0.250 * bmp.Width), (int)(0.050 * bmp.Height), larguramolduralogin - 5, alturamolduralogin - 10);
            Rectangle PreenchimentoMolduraLogin = new Rectangle((int)(0.250 * bmp.Width), (int)(0.050 * bmp.Height), larguramolduralogin - 10, alturamolduralogin - 10);

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
                    CampoClicked = true;
                    g.FillRectangle(GradientBotaoVermelho, PreenchimentoBotaoAluno);
                }
            }
            g.DrawRectangle(CanetaPreta, BotaoProfessor);
            if (BotaoProfessor.Contains(cursor))
            {
                g.FillRectangle(GradientBotaoVermelho, PreenchimentoBotaoProfessor);
                if(BotaoProfessor.Contains(cursor) && isDown)
                {
                    CampoClicked = true;
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
            Font fontProfessor = new Font("Arial", (int)(0.049 * bmp.Height));

            float fontSize = 60f;

            String textoLogin = "Login:";
            Font fontLogin = new Font(FontFamily.GetFamilies(g)[59], fontSize);


            String inputLogin = login;
            String inputSenha = senha;

            switch (selectedField)
            {   
                case 0:
                    inputLogin = text;
                    login = text;
                break;

                case 1:
                    inputSenha = text;
                    senha = text;
                break;

                default:
                    text = "";
                break;
            }

            // if(selectedField == 0)
            //     inputLogin = text;
            // else
            // {
            //     login = text;
            //     inputLogin = login;
            //     text = "";
            // }

           
            //  if(selectedField == 1)
            //     inputSenha = text;
            // else
            // {
            //     senha = text;
            //     inputSenha = senha;
            //     text = "";
            // }

            String textoSenha = "Senha:";
            Font fontSenha = new Font(FontFamily.GetFamilies(g)[59], fontSize);

            String textoCadastre = "Cadastre-se";
            Font fontTirarFoto = new Font(FontFamily.GetFamilies(g)[59], fontSize);

            g.DrawString(inputLogin, fontAluno, letraPreta, BotaoLogin, format2);
            g.DrawString(inputSenha, fontAluno, letraPreta, BotaoSenha, format2);

            g.DrawRectangle(CanetaPreta, BotaoLogin);
            if (BotaoLogin.Contains(cursor))
            {
            
                if(BotaoLogin.Contains(cursor) && isDown)
                    CampoClicked = true;

                if (BotaoLogin.Contains(cursor) && !isDown && CampoClicked)
                {
                    CampoClicked = false;
                    selectedField = 0;
                }
            }

            g.DrawRectangle(CanetaPreta, BotaoSenha);
            if (BotaoSenha.Contains(cursor))
            {
            
                if(BotaoSenha.Contains(cursor) && isDown)
                    CampoClicked = true;

                if (BotaoSenha.Contains(cursor) && !isDown && CampoClicked)
                {
                    CampoClicked = false;
                    selectedField = 1;
                }
            }
            

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
                    CampoClicked = true;
                }

                if (retanguloCadastrar.Contains(cursor) && !isDown && CampoClicked)
                {
                    CampoClicked = false;
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