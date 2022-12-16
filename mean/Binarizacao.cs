using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

public static class Binarization
{
    public static Bitmap ApplyBin(Bitmap bmp, Bitmap bg, float treshold = 120f)
    {   
        Bitmap returnBmp = new Bitmap(bmp.Width, bmp.Height);

        for (int j = 0; j < bmp.Width; j++)
        {
            for (int i = 0; i < bmp.Height; i++)
            {
                Color pixel = bmp.GetPixel(i,j);
                Color pixel2 = bg.GetPixel(i,j);

                int dr = pixel.R - pixel2.R;
                int dg = pixel.G - pixel2.G;
                int db = pixel.B - pixel2.B;
                float diff = (dr * dr + dg * dg + db * db) / (3f * 255 * 255);

                if (diff <= treshold)
                    returnBmp.SetPixel(i, j, Color.Black);
                else
                    returnBmp.SetPixel(i, j, Color.White);
                    
            }
            
        }
        return returnBmp;       
    }    

    public static Bitmap ApplyBin2(Bitmap bmp, Bitmap bg, float treshold = .05f)
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

        var data_bt = returnBmp.LockBits(
            new Rectangle(0, 0, returnBmp.Width, returnBmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);


        unsafe
        {
            byte* pDataBmp = (byte*)data_bmp.Scan0.ToPointer();

            byte* pDataBg = (byte*)data_bg.Scan0.ToPointer();

            byte* pDataReturn = (byte*)data_bt.Scan0.ToPointer();


            for (int j = 0; j < data_bg.Height; j++)
            {
                byte* l = pDataBmp + j * data_bg.Stride;

                for (int i = 0; i < data_bg.Width; i++, l+=3)
                {
                    byte diffBlue = (byte)(pDataBmp[0] - pDataBg[0]);
                    byte diffGreen = (byte)(pDataBmp[1] - pDataBg[1]);
                    byte diffRed = (byte)(pDataBmp[2] - pDataBg[2]);

                    float diff = ( diffRed * diffRed + diffBlue * diffBlue + diffGreen * diffGreen) / (3f * 255 * 255);

                    if (diff <= treshold)
                    {
                        pDataReturn[0] = 0;
                        pDataReturn[1] = 0;
                        pDataReturn[2] = 0;
                    }
                    else
                    {
                        pDataReturn[0] = 255;
                        pDataReturn[1] = 255;
                        pDataReturn[2] = 255;
                    }
                }
            }
        }

        bmp.UnlockBits(data_bmp);
        bg.UnlockBits(data_bg);
        returnBmp.UnlockBits(data_bt);

        return returnBmp;
    }
}
