using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Numerics;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public static class Normalization
{
    public static float GetParam(Bitmap bmp)
    {
        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb
        );

        long R = 0;
        long G = 0;
        long B = 0;
        long C = 0;
        
        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();

            Parallel.For(0, bmp.Height, j =>
            {
                long _R = 0;
                long _G = 0;
                long _B = 0;
                long _C = 0;

                byte* line = p + j * data.Stride;

                for (int i = 0; i < data.Width / 10; i++, line += 3)
                {
                    byte b = line[0];
                    byte g = line[1];
                    byte r = line[2];
                    
                    _R += r;
                    _G += g;
                    _B += b;
                    _C++;
                }

                R += _R;
                G += _G;
                B += _B;
                C += _C;
            });
        }

        bmp.UnlockBits(data);

        return (R + G + B) / (float)C;
    }
    public static void Normalize(Bitmap bmp, float param, float bgParam)
    {
        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb
        );

        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();

            float cal = bgParam / param;
            Parallel.For(0, bmp.Height, j =>
            {
                byte* line = p + j * data.Stride;

                for (int i = 0; i < data.Width - 1; i++, line += 3)
                {
                    line[0] = line[0] * cal > 255 ? (byte)255 : (byte)(line[0] * cal);
                    line[1] = line[1] * cal > 255 ? (byte)255 : (byte)(line[1] * cal);
                    line[2] = line[2] * cal > 255 ? (byte)255 : (byte)(line[2] * cal);

                }
            });
        }
        
        bmp.UnlockBits(data);
    }
    
}