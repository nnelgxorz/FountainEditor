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
            for (int i; i < elements.Count; i++)
            {
                if (elements[i] is NullTextElement && isUpper(elements[i].Text)) 
                {
                    string characterName = ""; 

                    foreach (var item in ScanCharacter(elements, i))
                    {
                        characterName += item.Text;
                    }
                }
            }
        }

        public IEnumerable<Element> ScanCharacter(List<Element> elements, int start)
        {
            for (int i = start; i < elements.Count; i++)
            {
                if (elements[i] is LineEnding)
                    yield break;
                yield return elements[i];
            }
        }

        public bool isUpper(string text)
        {
            foreach (var item in text)
            {
                if (char.IsLower(item))
                    return false;
            }
            return true;
        }
    }
}

