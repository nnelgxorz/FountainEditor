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
            for (int i = 0; i < elements.Count; i++)
            {
                //Check for Character Name element
                if (elements[i] is NullTextElement && isUpper(elements[i].Text))
                {
                    string characterName = "";

                    ScanCharacter(elements, i);
                    foreach (var item in ScanCharacter(elements, i))
                    {
                        characterName += item.Text;
                    }

                }
                //Check NullText for ForcedSceneHeading syntax.
                if (elements[i] is NullTextElement && elements[i].Text.StartsWith("."))
                {
                    string sceneHeading = "";

                    foreach (var item in ScanSceneHeading(elements, i))
                    {
                        sceneHeading += item.Text;
                    }
                }

                if (elements[i] is LineEnding)
                {
                    // check for blank line or connecting element
                    if (elements[i + 1] is LineEnding && 
                        elements[i + 2] is NullTextElement && 
                        elements[i + 3] is LineEnding)
                    {
                        if (elements[i + 2].Text.Length == 2 && 
                            elements[i + 2].Text.Contains("  "))
                        {
                            //assign NullTextElement to type before last found LineEnding
                        }
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
        public IEnumerable<Element> ScanSceneHeading(List<Element> elements, int start)
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

