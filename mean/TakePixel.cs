public class TakePixel
{
    public bool Detect(Point point, Point last, int Min, int Max){
        var DistanciaDoUltimo = Math.Sqrt(Math.Pow(Math.Abs(last.X - point.X),2) + Math.Pow(Math.Abs(last.Y - point.Y),2));
        
        if (DistanciaDoUltimo > Min && DistanciaDoUltimo > Max)
        {
            return true;
        }
        return false;
    }
}
