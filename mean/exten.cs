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

public static class exten
{



    public static Bitmap decompress(byte[] file)
    {
        if (file[0] != (byte)'P' ||
            file[1] != (byte)'U' ||
            file[2] != (byte)'D' ||
            file[3] != (byte)'I' ||
            file[4] != (byte)'M')
            throw new Exception("Arquivo .pudim inválido");

        int colors = file[5];
        int hei = file[6 + 3 * 255 + 0] * 256 +
            file[6 + 3 * 255 + 1];
        int wid = file[6 + 3 * 255 + 2] * 256 +
            file[6 + 3 * 255 + 3];
        Bitmap bmp = new Bitmap(wid, hei);
        byte[] pixels = new byte[3 * wid * hei];
        int index = 6 + 3 * 255 + 4;

        for (int i = 0; i < wid * hei; i++)
        {
            int k = file[index + i];
            pixels[3 * i + 0] = file[6 + 3 * k + 0];
            pixels[3 * i + 1] = file[6 + 3 * k + 1];
            pixels[3 * i + 2] = file[6 + 3 * k + 2];
        }

        img(bmp, pixels);

        return bmp;
    }

    public static byte[] compress((Bitmap bmp, byte[] img) org)
    {
        int wid = org.bmp.Width;
        int hei = org.bmp.Height;
        byte[] img = org.img;
        byte[] file = new byte[
            5 + 1 + 3 * 255 + 4 + wid * hei
        ];
        Random.Shared.NextBytes(file);

        file[0] = (byte)'P';
        file[1] = (byte)'U';
        file[2] = (byte)'D';
        file[3] = (byte)'I';
        file[4] = (byte)'M';
        file[5] = 255;

        long[] R = new long[255];
        long[] G = new long[255];
        long[] B = new long[255];
        long[] Q = new long[255];

        for (int epoch = 0; epoch < 10; epoch++)
        {
            for (int k = 0; k < 255; k++)
                R[k] = G[k] = B[k] = Q[k] = 0;

            Parallel.For(0, hei, j =>
            {
                for (int i = 0; i < wid;
                    i += Random.Shared.Next(2, 11))
                {
                    int index = 3 * (i + j * wid);
                    byte or = img[index + 0];
                    byte og = img[index + 1];
                    byte ob = img[index + 2];

                    int bestK = 0;
                    int bestDist = int.MaxValue;
                    for (int k = 0; k < 255; k++)
                    {
                        index = 6 + 3 * k;
                        byte r = file[index + 0];
                        byte g = file[index + 1];
                        byte b = file[index + 2];

                        int dr = r - or;
                        int dg = g - og;
                        int db = b - ob;
                        int dist = dr * dr + dg * dg + db * db;

                        if (dist < bestDist)
                        {
                            bestK = k;
                            bestDist = dist;
                        }
                    }

                    R[bestK] += or;
                    G[bestK] += og;
                    B[bestK] += ob;
                    Q[bestK]++;
                }
            });

            for (int k = 0; k < 255; k++)
            {
                if (Q[k] == 0)
                    continue;

                int index = 6 + 3 * k;
                file[index + 0] = (byte)(R[k] / Q[k]);
                file[index + 1] = (byte)(G[k] / Q[k]);
                file[index + 2] = (byte)(B[k] / Q[k]);
            }
        }

        file[6 + 3 * 255 + 0] = (byte)(hei / 256);
        file[6 + 3 * 255 + 1] = (byte)(hei % 256);
        file[6 + 3 * 255 + 2] = (byte)(wid / 256);
        file[6 + 3 * 255 + 3] = (byte)(wid % 256);

        for (int j = 0; j < hei; j++)
        {
            for (int i = 0; i < wid; i++)
            {
                int index = 3 * (i + j * wid);
                byte or = img[index + 0];
                byte og = img[index + 1];
                byte ob = img[index + 2];

                int bestK = 0;
                int bestDist = int.MaxValue;
                for (int k = 0; k < 255; k++)
                {
                    index = 6 + 3 * k;
                    byte r = file[index + 0];
                    byte g = file[index + 1];
                    byte b = file[index + 2];

                    int dr = r - or;
                    int dg = g - og;
                    int db = b - ob;
                    int dist = dr * dr + dg * dg + db * db;

                    if (dist < bestDist)
                    {
                        bestK = k;
                        bestDist = dist;
                    }
                }

                file[5 + 1 + 3 * 255 + 4 + i + j * wid]
                    = (byte)bestK;
            }
        }

        return file;
    }

    public static (Bitmap bmp, float[] img) hough((Bitmap bmp, float[] img) org)
    {
        int wid = org.bmp.Width;
        int hei = org.bmp.Height;
        float[] tImg = new float[1000 * 1000];
        Bitmap bmpTranformation = new Bitmap(1000, 1000);
        var oImg = org.img;

        for (int j = 0; j < hei; j++)
        {
            for (int i = 0; i < wid; i++)
            {
                int index = i + j * wid;
                if (oImg[index] == 0f)
                    continue;
                // y = ax + b
                // b = -xa + y
                float a = -i;
                float b = j;

                for (int x = 0; x < 1000; x++)
                {
                    float y = a * (x / 500f - 1f) + b;
                    y = (y + 5000f) / 10f;
                    if (y < 0 || y >= 1000)
                        continue;

                    int iy = (int)y;
                    tImg[x + iy * 1000] += 0.0001f;
                    if (tImg[x + iy * 1000] > 1f)
                        tImg[x + iy * 1000] = 1f;
                }
            }
        }

        var tBytes = discretGray(tImg);
        var image = img(bmpTranformation, tBytes);
        return (image as Bitmap, tImg);
    }

    public static (Bitmap bmp, float[] img) bilinear((Bitmap bmp, float[] img) org)
    {
        float[] orgImg = org.img;
        float[] newImg = new float[orgImg.Length];
        int hei = org.bmp.Height;
        int wid = org.bmp.Width;

        for (int j = 0; j < hei; j++)
        {
            for (int i = 0; i < wid; i++)
            {
                int index = i + j * wid;
                if (orgImg[index] > 0f ||
                    i == 0 || j == 0 ||
                    i == wid - 1 || j == hei - 1)
                {
                    newImg[index] = orgImg[index];
                    continue;
                }

                var c1Index = i - 1 + (j - 1) * wid;
                var c2Index = i + 1 + (j - 1) * wid;
                float a = (orgImg[c1Index] + orgImg[c2Index]) / 2;

                var c3Index = i - 1 + (j + 1) * wid;
                var c4Index = i + 1 + (j + 1) * wid;
                float b = (orgImg[c3Index] + orgImg[c4Index]) / 2;

                newImg[index] = (a + b) / 2;
            }
        }

        var imgBytes = discretGray(newImg);
        img(org.bmp, imgBytes);

        return (org.bmp, newImg);
    }

    public static (Bitmap bmp, float[] img) resize((Bitmap bmp, float[] img) org,
        float scaleX, float scaleY)
    {
        int wid = org.bmp.Width;
        int hei = org.bmp.Height;
        Bitmap newBmp = new Bitmap(
            (int)(scaleX * wid),
            (int)(scaleY * hei)
        );
        int sWid = newBmp.Width;
        int sHei = newBmp.Height;
        float[] orgImg = org.img;
        float[] newImg = new float[sWid * sHei];
        for (int j = 0; j < hei; j++)
        {
            for (int i = 0; i < wid; i++)
            {
                newImg[i + j * sWid]
                    = orgImg[i + j * wid];
            }
        }
        var imgBytes = discretGray(newImg);
        var bmp = img(newBmp, imgBytes) as Bitmap;
        var image = (bmp, newImg);

        image = affine(image,
            scale(scaleX, scaleY));

        return image;
    }

    public static Matrix4x4 mat(params float[] arr)
    {
        return new Matrix4x4(
            arr[0], arr[1], arr[2], 0,
            arr[3], arr[4], arr[5], 0,
            arr[6], arr[7], arr[8], 0,
                 0, 0, 0, 1
        );
    }

    public static Matrix4x4 rotation(float degree)
    {
        float radian = degree / 180 * MathF.PI;
        float cos = MathF.Cos(radian);
        float sin = MathF.Sin(radian);
        return mat(
            cos, -sin, 0,
            sin, cos, 0,
              0, 0, 1
        );
    }

    public static Matrix4x4 translate(float dx, float dy)
    {
        return mat(
            1, 0, dx,
            0, 1, dy,
            0, 0, 1
        );
    }

    public static Matrix4x4 translateFromSize(float dx, float dy,
        (Bitmap bmp, float[] img) t)
    {
        return mat(
            1, 0, dx * t.bmp.Width,
            0, 1, dy * t.bmp.Height,
            0, 0, 1
        );
    }

    public static Matrix4x4 scale(float dx, float dy)
    {
        return mat(
            dx, 0, 0,
            0, dy, 0,
            0, 0, 1
        );
    }

    public static Matrix4x4 shear(float cx, float cy)
    {
        return mat(
            1, cx, 0,
            cy, 1, 0,
            0, 0, 1
        );
    }

    public static (Bitmap bmp, float[] img) affine((Bitmap bmp, float[] img) t,
        Matrix4x4 mat)
    {
        float[] p = new float[]
        {
        mat.M11, mat.M12, mat.M13,
        mat.M21, mat.M22, mat.M23,
        mat.M31, mat.M32, mat.M33,
        };
        var _img = t.img;
        float[] newImg = new float[_img.Length];
        int wid = t.bmp.Width;
        int hei = t.bmp.Height;
        int x = 0;
        int y = 0;
        int index = 0;

        for (int i = 0; i < wid; i++)
        {
            for (int j = 0; j < hei; j++)
            {
                x = (int)(p[0] * i + p[1] * j + p[2]);
                y = (int)(p[3] * i + p[4] * j + p[5]);

                if (x < 0 || x >= wid || y < 0 || y >= hei)
                    continue;
                else
                {
                    index = (int)(x + y * wid);
                    newImg[index] = _img[i + j * wid];
                }
            }
        }

        var Imgbytes = discretGray(newImg);
        img(t.bmp, Imgbytes);

        return (t.bmp, newImg);
    }

    public static (Bitmap bmp, float[] img) sobel((Bitmap bmp, float[] img) t,
        bool dir = true)
    {
        var im = t.img;
        float[] tempo = new float[im.Length];
        float[] final = new float[im.Length];
        int wid = t.bmp.Width;
        int hei = t.bmp.Height;

        for (int i = 1; i < wid - 1; i++)
        {
            float sum =
                im[i + 0 * wid] +
                im[i + 1 * wid] +
                im[i + 2 * wid];
            for (int j = 1; j < hei - 1; j++)
            {
                int index = i + j * wid;
                tempo[index] = im[index] + sum;

                sum -= im[index - 1];
                sum += im[index + 1];
            }
        }

        for (int j = 1; j < hei - 1; j++)
        {
            float seq =
                im[0 + j * wid] +
                im[1 + j * wid];
            for (int i = 1; i < wid - 1; i++)
            {
                float nextSeq =
                    im[i + j * wid] +
                    im[i + 1 + j * wid];

                int index = i + j * wid;
                float value = dir ? seq - nextSeq : nextSeq - seq;
                if (value > 1f)
                    value = 1f;
                else if (value < 0f)
                    value = 0f;
                final[index] = value;

                seq = nextSeq;
            }
        }

        var Imgbytes = discretGray(final);
        img(t.bmp, Imgbytes);

        return (t.bmp, final);
    }

    public static Bitmap conv(
        (Bitmap bmp, float[] img) t, params float[] kernel)
    {
        var N = (int)Math.Sqrt(kernel.Length);
        var wid = t.bmp.Width;
        var hei = t.bmp.Height;
        var _img = t.img;
        float[] result = new float[_img.Length];

        for (int j = N / 2; j < hei - N / 2; j++)
        {
            for (int i = N / 2; i < wid - N / 2; i++)
            {
                float sum = 0;

                for (int k = 0; k < N; k++)
                {
                    for (int l = 0; l < N; l++)
                    {
                        sum += _img[i + k - (N / 2) + (j + l - (N / 2)) * wid] *
                            kernel[k + l * N];
                    }
                }

                if (sum > 1f)
                    sum = 1f;

                else if (sum < 0f)
                    sum = 0f;

                result[i + j * wid] = sum;
            }
        }

        var Imgbytes = discretGray(result);
        img(t.bmp, Imgbytes);

        return t.bmp;
    }

    public static (Bitmap bmp, float[] img) morfology((Bitmap bmp, float[] img) t, float[] kernel, bool erosion)
    {
        bool match = false;
        int wid = t.bmp.Width;
        int hei = t.bmp.Height;

        float[] imgor = t.img;
        float[] newImg = new float[imgor.Length];
        var tamKernel = (int)Math.Sqrt(kernel.Length);

        for (int i = 0; i < imgor.Length; i++)
        {
            match = erosion;
            int x = i % wid,
                y = i / wid;

            for (int j = 0; j < kernel.Length; j++)
            {
                if (kernel[j] == 0f)
                    continue;

                int kx = j % tamKernel,
                    ky = j / tamKernel;

                int tx = x + kx - tamKernel / 2;
                int ty = y + ky - tamKernel / 2;

                if (tx < 0 || ty < 0 || tx >= wid || ty >= hei)
                    continue;

                int index = tx + ty * wid;

                if (imgor[index] == 1f)
                {
                    if (!erosion)
                    {
                        match = true;
                        break;
                    }
                }
                else
                {
                    if (erosion)
                    {
                        match = false;
                        break;
                    }
                }
            }

            if (match)
                newImg[i] = 1f;
        }

        var Imgbytes = discretGray(newImg);
        img(t.bmp, Imgbytes);

        return (t.bmp, newImg);
    }

    public static List<Rectangle> segmentation((Bitmap bmp, float[] img) t)
    {
        var rects = segmentationT(t, 0);
        var areas = rects.Select(r => r.Width * r.Height);
        var average = areas.Average();

        return rects
            .Where(r => r.Width * r.Height > average)
            .ToList();
    }

    public static List<Rectangle> segmentationT((Bitmap bmp, float[] img) t, int threshold)
    {
        List<Rectangle> list = new List<Rectangle>();
        Stack<int> stack = new Stack<int>();

        float[] img = t.img;
        int wid = t.bmp.Width;
        float crr = 0.01f;

        int minx, maxx, miny, maxy;
        int count = 0;

        for (int i = 0; i < img.Length; i++)
        {
            if (img[i] > 0f)
                continue;

            minx = int.MaxValue;
            miny = int.MaxValue;
            maxx = int.MinValue;
            maxy = int.MinValue;
            count = 0;
            stack.Push(i);

            while (stack.Count > 0)
            {
                int j = stack.Pop();

                if (j < 0 || j >= img.Length)
                    continue;

                if (img[j] > 0f)
                    continue;

                int x = j % wid,
                    y = j / wid;

                if (x < minx)
                    minx = x;
                if (x > maxx)
                    maxx = x;

                if (y < miny)
                    miny = y;
                if (y > maxy)
                    maxy = y;

                img[j] = crr;
                count++;

                stack.Push(j - 1);
                stack.Push(j + 1);
                stack.Push(j + wid);
                stack.Push(j - wid);
            }

            crr += 0.01f;
            if (count < threshold)
                continue;

            Rectangle rect = new Rectangle(
                minx, miny, maxx - minx, maxy - miny
            );
            list.Add(rect);
        }

        return list;
    }

    public static void otsu((Bitmap bmp, float[] img) t, float db = 0.05f)
    {
        var histogram = hist(t.img, db);
        int threshold = 0;

        float Ex0 = 0;
        float Ex1 = t.img.Average();
        float Dx0 = 0;
        float Dx1 = t.img.Sum(x => x * x);
        int N0 = 0;
        int N1 = t.img.Length;

        float minstddev = float.PositiveInfinity;

        for (int i = 0; i < histogram.Length; i++)
        {
            float value = db * (2 * i + 1) / 2;
            float s = histogram[i] * value;

            if (N0 == 0 && histogram[i] == 0)
                continue;

            Ex0 = (Ex0 * N0 + s) / (N0 + histogram[i]);
            Ex1 = (Ex1 * N1 - s) / (N1 - histogram[i]);

            N0 += histogram[i];
            N1 -= histogram[i];

            Dx0 += value * value * histogram[i];
            Dx1 -= value * value * histogram[i];

            float stddev =
                Dx0 - N0 * Ex0 * Ex0 +
                Dx1 - N1 * Ex1 * Ex1;

            if (float.IsInfinity(stddev) ||
                float.IsNaN(stddev))
                continue;

            if (stddev < minstddev)
            {
                minstddev = stddev;
                threshold = i;
            }
        }
        float bestTreshold = db * (2 * threshold + 1) / 2;

        tresh(t, bestTreshold);
    }

    public static void tresh((Bitmap bmp, float[] img) t,
        float threshold = 0.5f)
    {
        for (int i = 0; i < t.img.Length; i++)
            t.img[i] = t.img[i] > threshold ? 1f : 0f;
    }

    public static float[] equalization(
        (Bitmap bmp, float[] img) t,
        float threshold = 0f,
        float db = 0.05f)
    {
        int[] histogram = hist(t.img, db);

        int dropCount = (int)(t.img.Length * threshold);

        float min = 0;
        int droped = 0;
        for (int i = 0; i < histogram.Length; i++)
        {
            droped += histogram[i];
            if (droped > dropCount)
            {
                min = i * db;
                break;
            }
        }

        float max = 0;
        droped = 0;
        for (int i = histogram.Length - 1; i > -1; i--)
        {
            droped += histogram[i];
            if (droped > dropCount)
            {
                max = i * db;
                break;
            }
        }

        var r = 1 / (max - min);

        for (int i = 0; i < t.img.Length; i++)
        {
            float newValue = (t.img[i] - min) * r;
            if (newValue > 1f)
                newValue = 1f;
            else if (newValue < 0f)
                newValue = 0f;
            t.img[i] = newValue;
        }

        return t.img;
    }

    public static void showHist((Bitmap bmp, float[] img) t, float db = 0.05f)
    {
        var histogram = hist(t.img, db);
        var histImg = drawHist(histogram);
        showBmp(histImg);
    }

    public static (Bitmap bmp, float[] img) open(Bitmap bmp)
    {
        var byteArray = bytes(bmp);
        var dataCont = continuous(byteArray);
        var gray = grayScale(dataCont);
        return (bmp, gray);
    }

    public static void inverse((Bitmap bmp, float[] img) t)
    {
        for (int i = 0; i < t.img.Length; i++)
            t.img[i] = 1f - t.img[i];
    }

    public static Image drawHist(int[] hist)
    {
        var bmp = new Bitmap(512, 256);
        var g = Graphics.FromImage(bmp);
        float margin = 16;

        int max = hist.Max();
        float barlen = (bmp.Width - 2 * margin) / hist.Length;
        float r = (bmp.Height - 2 * margin) / max;

        for (int i = 0; i < hist.Length; i++)
        {
            float bar = hist[i] * r;
            g.FillRectangle(Brushes.Black,
                margin + i * barlen,
                bmp.Height - margin - bar,
                barlen,
                bar);
            g.DrawRectangle(Pens.DarkBlue,
                margin + i * barlen,
                bmp.Height - margin - bar,
                barlen,
                bar);
        }

        return bmp;
    }

    public static void show((Bitmap bmp, float[] gray) t)
    {
        var bytes = discretGray(t.gray);
        var image = img(t.bmp, bytes);
        showBmp(image);
    }

    public static int[] hist(float[] img, float db = 0.05f)
    {
        int histogramLen = (int)(1 / db) + 1;
        int[] histogram = new int[histogramLen];

        foreach (var pixel in img)
            histogram[(int)(pixel / db)]++;

        return histogram;
    }

    public static float[] grayScale(float[] img)
    {
        float[] result = new float[img.Length / 3];

        for (int i = 0, j = 0; i < img.Length; i += 3, j++)
        {
            result[j] = 0.1f * img[i] +
                0.59f * img[i + 1] +
                0.3f * img[i + 2];
        }

        return result;
    }

    public static float[] continuous(byte[] img)
    {
        var result = new float[img.Length];

        for (int i = 0; i < img.Length; i++)
            result[i] = img[i] / 255f;

        return result;
    }

    public static byte[] discret(float[] img)
    {
        var result = new byte[img.Length];

        for (int i = 0; i < img.Length; i++)
            result[i] = (byte)(255 * img[i]);

        return result;
    }

    public static byte[] discretGray(float[] img)
    {
        var result = new byte[3 * img.Length];

        for (int i = 0; i < img.Length; i++)
        {
            var value = (byte)(255 * img[i]);
            result[3 * i] = value;
            result[3 * i + 1] = value;
            result[3 * i + 2] = value;
        }

        return result;
    }

    public static byte[] bytes(Image img)
    {
        var bmp = img as Bitmap;
        var data = bmp.LockBits(
            new Rectangle(0, 0, img.Width, img.Height),
            ImageLockMode.ReadOnly,
            PixelFormat.Format24bppRgb);

        byte[] byteArray = new byte[3 * data.Width * data.Height];

        byte[] temp = new byte[data.Stride * data.Height];
        Marshal.Copy(data.Scan0, temp, 0, temp.Length);

        for (int j = 0; j < data.Height; j++)
        {
            for (int i = 0; i < 3 * data.Width; i++)
            {
                byteArray[i + j * 3 * data.Width] =
                    temp[i + j * data.Stride];
            }
        }

        bmp.UnlockBits(data);

        return byteArray;
    }

    public static Image img(Image img, byte[] bytes)
    {
        var bmp = img as Bitmap;
        var data = bmp.LockBits(
            new Rectangle(0, 0, img.Width, img.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

        byte[] temp = new byte[data.Stride * data.Height];

        for (int j = 0; j < data.Height; j++)
        {
            for (int i = 0; i < 3 * data.Width; i++)
            {
                temp[i + j * data.Stride] =
                    bytes[i + j * 3 * data.Width];
            }
        }

        Marshal.Copy(temp, 0, data.Scan0, temp.Length);

        bmp.UnlockBits(data);
        return img;
    }

    public static void showBmp(Image img)
    {
        ApplicationConfiguration.Initialize();

        Form form = new Form();

        PictureBox pb = new PictureBox();
        pb.Dock = DockStyle.Fill;
        pb.SizeMode = PictureBoxSizeMode.Zoom;
        form.Controls.Add(pb);

        form.WindowState = FormWindowState.Maximized;
        form.FormBorderStyle = FormBorderStyle.None;

        form.Load += delegate
        {
            pb.Image = img;
        };

        form.KeyDown += (o, e) =>
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        };

        Application.Run(form);
    }

    public static void showRects((Bitmap bmp, float[] img) t, List<Rectangle> list)
    {
        var g = Graphics.FromImage(t.bmp);

        foreach (var rect in list)
            g.DrawRectangle(Pens.Red, rect);

        showBmp(t.bmp);
    }




}