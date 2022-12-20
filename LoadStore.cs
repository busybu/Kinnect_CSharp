// using System; 
// using System.IO; 
// using System.Runtime.Serialization; 
// using System.Runtime.Serialization.Formatters.Binary;
// public class SerialTest 
// {

//     public void SerializeNow() 
//     {
//         NeuralNetwork n = new NeuralNetwork(2, 2, 100, 4);
//         FileStream f = File.Create("temp.dat"); 
//         BinaryFormatter b = new BinaryFormatter();
//         b.Serialize(f, n);
//         f.Close();
//     }

//     public void DeSerializeNow() {
//         NeuralNetwork n = new NeuralNetwork();
//         FileStream f = new File.Open("temp.dat");
//         Stream s = f.Open(FileMode.Open);
//         BinaryFormatter b = new BinaryFormatter();
//         n = (NeuralNetwork) b.Deserialize(s);
//         Console.WriteLine(n.Layers);
        
//         s.Close();
//     }
// }

