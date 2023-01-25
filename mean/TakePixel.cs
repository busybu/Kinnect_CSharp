public class TakePixel
{
    public bool Detect(Point point, Point last, int Min, int Max){
        var DistanciaDoUltimo = (last.X - point.X)*(last.X - point.X) + (last.Y - point.Y)*(last.Y - point.Y);
        
        if (DistanciaDoUltimo > Min && DistanciaDoUltimo < Max)
        {
            return true;
        }
        return false;
    }
}
