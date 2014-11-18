using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FountainEditor.Elements;

namespace FountainEditor
{
    public class Optimizer
    {
        public void Optimize(List<Element> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] is TransitionTextElement &&
                    elements[i].Text.Contains("TO:"))
                {
                    var transitionElements = ScanBackward(elements, i).ToArray();
                    Array.Reverse(transitionElements);
                    var transitionText = string.Join(" ", transitionElements.Select(e => e.Text));

                    foreach (var transitionElement in transitionElements)
                    {
                        elements.Remove(transitionElement);
                    }

                    elements.Insert(i -= transitionElements.Count() - 1, new TransitionTextElement(transitionText));
                    i++;
                }
            }

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] is NullTextElement &&
                    CheckUpper(elements[i].Text) && 
                    elements[i -1] is LineEnding)
                {
                    var characterElements = ScanCharacter(elements, i).ToArray();
                    var characterName = string.Join(" ", characterElements.Select(e => e.Text));

                    foreach (var characterElement in characterElements)
                    {
                        elements.Remove(characterElement);
                    }

                    if (characterName.StartsWith("^"))
                    {
                        elements.Insert(i, new DualDialogueTextElement(characterName));
                    }
                    else
                    {
                        elements.Insert(i, new CharacterTextElement(characterName));
                    }
                    i++;

                    if (elements[i] is LineEnding)
                    {
                        i++;
                    }

                    if (elements[i] is ParentheticalTextElement)
                    {
                        i++;

                        if (elements[i] is LineEnding)
                        {
                            i++;
                        }
                    }

                    if (elements[i] is NullTextElement)
                    {
                        var dialogueElements = ScanDialogue(elements, i).ToArray();
                        var dialogue = string.Join(" ", dialogueElements.Select(e => e.Text));

                        foreach (var dialogueElement in dialogueElements)
                        {
                            elements.Remove(dialogueElement);
                        }

                        elements.Insert(i, new DialogueTextElement(dialogue));
                        i++;
                    }
                }

                if (elements[i] is NullTextElement && elements[i].Text.StartsWith("."))
                {
                    var sceneHeadingElements = ScanForward(elements, i).ToArray();
                    var sceneHeading = string.Join(" ", sceneHeadingElements.Select(e => e.Text));

                    foreach (var sceneHeadingElement in sceneHeadingElements)
                    {
                        elements.Remove(sceneHeadingElement);
                    }

                    elements.Insert(i, new SceneHeadingTextElement (sceneHeading));
                    i++;
                }

                if (elements[i] is SceneHeadingTextElement)
                {
                    var sceneHeadingElements = ScanForward(elements, i).ToArray();
                    var sceneHeading = string.Join(" ", sceneHeadingElements.Select(e => e.Text));

                    foreach (var sceneHeadingElement in sceneHeadingElements)
                    {
                        elements.Remove(sceneHeadingElement);
                    }

                    elements.Insert(i, new SceneHeadingTextElement(sceneHeading));
                    i++;
                }
            }

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] is ParentheticalTextElement)
                {
                    if(elements[i - 1] is LineEnding && elements[i - 2] is CharacterTextElement)
                    {
                        break;
                    }

                    else
                    {
                        string text = elements[i].Text;

                        elements.Remove(elements[i]);
                        elements.Insert(i, new NullTextElement(text));
                    }
                }
            }

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] is NullTextElement)
                {
                    var actionElements = ScanForward(elements, i).ToArray();
                    var actionText = string.Join(" ", actionElements.Select(e => e.Text));

                    foreach (var actionElement in actionElements)
                    {
                        elements.Remove(actionElement);
                    }

                    elements.Insert(i, new ActionTextElement(actionText));
                    i++;
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
                {
                    yield break;
                }

                if (CheckUpper(elements[i].Text) == false)
                {
                    break;
                }

                yield return elements[i];
            }
        }

        public IEnumerable<Element> ScanDialogue(List<Element> elements, int start)
        {
            for (int i = start; i < elements.Count; i++)
            {
                if (elements[i] is LineEnding && elements[i + 1] is LineEnding)
                {
                    yield break;
                }
                yield return elements[i];
            }
        }

        public IEnumerable<Element> ScanForward(List<Element> elements, int start)
        {
            for (int i = start; i < elements.Count; i++)
            {
                if (elements[i] is LineEnding)
                {
                    yield break;
                }
                yield return elements[i];
            }
        }

        public IEnumerable<Element> ScanBackward(List<Element> elements, int start)
        {
            for (int i = start; i < elements.Count; i--)
            {
                if (elements[i] is LineEnding)
                {
                    yield break;
                }
                yield return elements[i];
            }
        }

    }
}
