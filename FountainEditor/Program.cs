using System;
using System.IO;

namespace FountainEditor
{
    class Program
    {
        static void Main(params string[] args)
        { 
            var text = new StreamReader(@"C:\Users\Glenn\Desktop\test\screenplayIN.txt");
            //var text = new StreamReader(@"C:\Users\Glenn\Desktop\test\BigFishScript.txt");

            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Parse(text.ReadToEnd());

            var optimizer = new Optimizer();
            optimizer.Optimize(tokens);

            var writer = new StreamWriter(@"C:\Users\Glenn\Desktop\test\screenplayOUT.txt");

            foreach (var element in tokens)
            {
                writer.Write(element.Text);
            }
            writer.Flush();
        }
    }
}
