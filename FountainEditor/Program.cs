using System;
using System.IO;

namespace FountainEditor
{
    class Program
    {
        static void Main(params string[] args)
        { 
            var text = new StreamReader(@"C:\Users\Glenn\Desktop\Test\screenplayIN.txt");

            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Parse(text.ReadToEnd());

            var optimizer = new Optimizer();
            optimizer.Optimize(tokens);

            var writer = new StreamWriter(@"C:\Users\Glenn\Desktop\Test\screenplayOUT.txt");

            foreach (var element in tokens)
            {
                writer.Write(element.Text);
            }
            writer.Flush();
        }
    }
}
