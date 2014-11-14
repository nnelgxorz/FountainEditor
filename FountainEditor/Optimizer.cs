using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FountainEditor.Elements;

namespace FountainEditor
{
    class Optimizer
    {
        public void Optimize(List<Element> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] is NullTextElement &&
                    CheckUpper(elements[i].Text))
                {
                    string characterName = "";

                    ScanCharacter(elements, i);
                    foreach (var item in ScanCharacter(elements, i))
                    {
                        characterName += item.Text;
                    }

                    if (elements[i] is ParentheticalTextElement)
                    {
                        i += 1;
                        ScanDialogue(elements, i);
                    }

                    else
                        ScanDialogue(elements, i);
                }

                if (elements[i] is NullTextElement && 
                    elements[i].Text.StartsWith("^") &&
                    CheckUpper(elements[i].Text))
                {
                    string characterName = "";

                    foreach (var item in ScanCharacter(elements, i))
                    {
                        characterName += item.Text;
                        elements.Remove(item);
                    }

                    elements.Insert(i, new CharacterTextElement(characterName));
                    i += 2;

                    if (elements[i] is ParentheticalTextElement)
                    {
                        i += 2;
                        ScanDialogue(elements, i);
                        processDialogue(elements, i);
                    }

                    else
                    {
                        ScanDialogue(elements, i);
                        processDialogue(elements, i);
                    }
                }

                if (elements[i] is NullTextElement && elements[i].Text.StartsWith("."))
                {
                    string sceneHeading = "";

                    foreach (var item in ScanSceneHeading(elements, i))
                    {
                        sceneHeading += item.Text;
                        elements.Remove(item);
                    }
                    elements.Insert(i, new SceneHeadingTextElement(sceneHeading));
                }
                else
                {
                    Element currentElement = elements[i];
                    ActionTextElement action;

                    action = new ActionTextElement(currentElement.Text);
                }
            }
        }

        private bool CheckUpper(string text)
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

        public IEnumerable<Element> ScanDialogue(List<Element> elements, int start)
        {
            for (int i = start; i < elements.Count; i++)
            {//TODO: two consecutive lineendings breaks line?
                if (elements[i] is LineEnding)
                    yield break;
                yield return elements[i];
            }
        }

        private void processDialogue(List<Element> elements, int start)
        {
            string dialogue = "";

            foreach (var item in ScanDialogue(elements, start))
            {
                dialogue += item.Text;
                elements.Remove(item);
            }
            elements.Insert(start, new CharacterTextElement(dialogue));
        }

    }
}

