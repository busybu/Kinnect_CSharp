using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public abstract class Tela
{
    public abstract void Desenhar(
        Bitmap cam, Bitmap bmp, Graphics g,
        Point cursor, bool isDown, string text, string valor);
}