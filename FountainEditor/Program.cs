using System;
using System.IO;

namespace FountainEditor
{
    class Program
    {
        static void Main(params string[] args)
        { 
            var reader = new StreamReader(args[0]);
            var tokens = Tokenizer.Parse(reader.ReadToEnd());

            Optimizer.Optimize(tokens);

            var writer = new StreamWriter(args[1]);

            foreach (var element in tokens)
            {
                writer.Write(element.Print());
            }

            writer.Flush();
        }
    }
}
