using System.Text;


public class Mnist{
    public byte Value { get; set; } // the real value
    public byte[] img { get; set; } = new byte[28*28];
    public byte[] imgR { get; set; } = new byte[20*20];
    public byte[] imgReduced { get; set; } = new byte[5*5];

    public override string ToString()
    {
        StringBuilder st = new StringBuilder();
        st.Append(Value);
        st.Append(",");
        for (int i = 0; i < img.Length; i++)
            st.Append(img[i] + ",");
        
        st.Remove(st.Length - 1, 1);
        return st.ToString();
    }

    public string ReducedString()
    {
        StringBuilder st = new StringBuilder();
        for (int i = 0; i < imgReduced.Length; i++)
            st.Append(imgReduced[i] + ",");
        
        return st.ToString();
    }
    
    public void Reduce(){
        this.imgReduced = new byte[5*5];
        int index = 0;

        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                byte max = byte.MinValue;
                byte topleft = imgR[i + j * 5];
                byte topright = imgR[i + 1 + j * 5];
                byte bottomleft = imgR[i + (j + 1) * 5];
                byte bottomright = imgR[i + 1 + (j + 1) * 5];

                if (topleft > max)
                    max = topleft;
                
                if (topright > max)
                    max = topright;
                
                if (bottomleft > max)
                    max = bottomleft;
                
                if (bottomright > max)
                    max = bottomright;
                
                
                imgReduced[index] = max;
                index++;
            }
        }
    }
}