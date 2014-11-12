using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FountainEditor.Elements;

namespace FountainEditor
{
    static class CheckUpper
    {
        public static bool isUpper(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLower(text[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }

    class Optimizer
    {
        public void Optimize(List<Element> elements)
        {
                int consecutiveLB = 0;
                int lastCN = 0;
                int currentLine = 0;

            foreach (NullTextElement current in elements)
            {
                if (CheckUpper.isUpper(current.Text)) //current text begins with an uppercase letter.
                {

                }

                if (current.Text.StartsWith("@"))
                {
                    //convert current NullText to Character Name
                    while (true) //current element is NullTextElement and isn't a LineEnding.
                    {
                        //convert element to Character Name
                    }
                }

                if (current.Text.StartsWith("."))
                {
                    //convert current NullText to SceneHeading
                    while (true) //current element is NullTextElement and isn't a LineEnding.
                    {
                        //convert element
                    }
                }
            }
            currentLine++;
        }
    }
}

