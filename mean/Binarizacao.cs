using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

public static class Binarization
{

// Função para inverter as cores
    public static Bitmap ApplyBin(Bitmap bmp, Bitmap bg, float treshold = .001f)
    {
        
        var data_bmp = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

        var data_bg = bg.LockBits(
            new Rectangle(0, 0, bg.Width, bg.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

        var returnBmp = new Bitmap(bmp.Width, bmp.Height); 
        var data_rt = returnBmp.LockBits(
            new Rectangle(0, 0, returnBmp.Width, returnBmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

        unsafe
        {
            byte* pDataBmp = (byte*)data_bmp.Scan0.ToPointer();
            byte* pDataBg = (byte*)data_bg.Scan0.ToPointer();
            byte* pDataReturn = (byte*)data_rt.Scan0.ToPointer();

            for (int j = 0; j < data_bg.Height; j++)
            {
                byte* lBmp = pDataBmp + j * data_bmp.Stride;
                byte* lBkg = pDataBg + j * data_bg.Stride;
                byte* lRet = pDataReturn + j * data_rt.Stride;

                for (int i = 0; i < data_bg.Width; i++, lBmp += 3, lBkg += 3, lRet += 3)
                {
                    byte diffBlue = (byte)(lBkg[0] - lBmp[0]);
                    byte diffGreen = (byte)(lBkg[1] - lBmp[1]);
                    byte diffRed = (byte)(lBkg[2] - lBmp[2]);

                    float diff = (diffRed * diffRed + diffBlue * diffBlue + diffGreen * diffGreen) / (3f * 255 * 255);

                    if (diff <= treshold)
                    {
                        lRet[0] = 255;
                        lRet[1] = 255;
                        lRet[2] = 255;
                    }
                    else
                    {
                        lRet[0] = 0;
                        lRet[1] = 0;
                        lRet[2] = 0;
                    }
                }
            }
        }
        bmp.UnlockBits(data_bmp);
        bg.UnlockBits(data_bg);
        returnBmp.UnlockBits(data_rt);

        return returnBmp;
    }
}
