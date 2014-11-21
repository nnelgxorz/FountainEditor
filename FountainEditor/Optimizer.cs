using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FountainEditor.Elements;

namespace FountainEditor
{
    public static class Optimizer
    {
        public static void Optimize(List<Element> elements)
        {
            if (elements[0] is TitlePageKey)
            {
                for (int i = 0; i < elements.Count; i++)
                {
                    if (elements[i] is LineEnding &&
                        elements[i + 1] is LineEnding)
                    {
                        break;
                    }

                    if (elements[i] is TitlePageKey)
                    {
                        while (!(elements[i] is NullTextElement))
                        {
                            i++;
                        }

                        if (elements[i] is NullTextElement)
                        {
                            var titleValues = ScanTitlePage(elements, i).ToArray();
                            var titleText = string.Join("", titleValues.Select(e => e.Text));

                            foreach (var titleValue in titleValues)
                            {
                                elements.Remove(titleValue);
                            }

                            elements.Insert(i, new TitlePageValue(titleText));
                        }
                    }
                }
            }

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] is TransitionTextElement &&
                    !elements[i].Text.StartsWith("."))
                {
                    var transitionElements = ScanBackward(elements, i).Reverse().ToArray();
                    var transitionText = string.Join("", transitionElements.Select(e => e.Text));

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
                if (elements[i] is LineEnding ||
                    elements[i] is TabElement)
                {
                    continue;
                }

                if (elements[i] is NullTextElement &&
                    CheckUpper(elements[i].Text) &&
                    elements[i].Text.StartsWith("!"))
                {
                    continue;
                }

                if (elements[i] is NullTextElement &&
                    CheckUpper(elements[i].Text) &&
                    elements[i].Text.Length > 1)
                {
                    if (elements[i - 1] is LineEnding ||
                        elements[i - 1] is TabElement)
                    {
                        var characterElements = ScanCharacter(elements, i).ToArray();
                        var characterName = string.Join("", characterElements.Select(e => e.Text));

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
                    }
                    continue;
                }

                if (elements[i].Text.StartsWith("@"))
                {
                    if (elements[i - 1] is LineEnding ||
                        elements[i - 1] is TabElement)
                    {
                        var characterElements = ScanForward(elements, i).ToArray();
                        var characterName = string.Join("", characterElements.Select(e => e.Text));

                        foreach (var characterElement in characterElements)
                        {
                            elements.Remove(characterElement);
                        }

                        elements.Insert(i, new CharacterTextElement(characterName));
                    }
                    continue;
                }

                if (i >= 2 &&
                    elements[i - 0] is NullTextElement && 
                    elements[i - 1] is LineEnding)
                {
                    if (elements[i - 2] is CharacterTextElement ||
                        elements[i - 2] is DualDialogueTextElement ||
                        elements[i - 2] is ParentheticalTextElement ||
                        elements[i - 2] is DialogueTextElement)
                    {
                        var dialogueElements = ScanDialogue(elements, i).ToArray();
                        var dialogue = string.Join("", dialogueElements.Select(e => e.Text));

                        foreach (var dialogueElement in dialogueElements)
                        {
                            elements.Remove(dialogueElement);
                        }

                        elements.Insert(i, new DialogueTextElement(dialogue));
                        continue;
                    }
                }

                if (i >= 3 &&
                    elements[i - 0] is NullTextElement &&
                    elements[i - 1] is TabElement &&
                    elements[i - 2] is LineEnding)
                {
                    if (elements[i - 3] is CharacterTextElement ||
                        elements[i - 3] is DualDialogueTextElement ||
                        elements[i - 3] is ParentheticalTextElement ||
                        elements[i - 3] is DialogueTextElement)
                    {
                        var dialogueElements = ScanDialogue(elements, i).ToArray();
                        var dialogue = string.Join("", dialogueElements.Select(e => e.Text));

                        foreach (var dialogueElement in dialogueElements)
                        {
                            elements.Remove(dialogueElement);
                        }

                        elements.Insert(i, new DialogueTextElement(dialogue));
                        continue;
                    }
                }

                if (elements[i] is NullTextElement && elements[i].Text.StartsWith("."))
                {
                    var sceneHeadingElements = ScanForward(elements, i).ToArray();
                    var sceneHeading = string.Join("", sceneHeadingElements.Select(e => e.Text));

                    foreach (var sceneHeadingElement in sceneHeadingElements)
                    {
                        elements.Remove(sceneHeadingElement);
                    }

                    elements.Insert(i, new SceneHeadingTextElement (sceneHeading));
                    continue;
                }

                if (elements[i] is SceneHeadingTextElement)
                {
                    var sceneHeadingElements = ScanForward(elements, i).ToArray();
                    var sceneHeading = string.Join("", sceneHeadingElements.Select(e => e.Text));

                    foreach (var sceneHeadingElement in sceneHeadingElements)
                    {
                        elements.Remove(sceneHeadingElement);
                    }

                    elements.Insert(i, new SceneHeadingTextElement(sceneHeading));
                    continue;
                }

                if (elements[i] is DoubleSpaceElement &&
                    elements[i - 1] is LineEnding)
                {
                    var doubleSpaceText = elements[i].Text;

                    if (elements[i -2] is DialogueTextElement)
                    {
                        elements.Remove(elements[i]);
                        elements.Insert(i, new DialogueTextElement(doubleSpaceText));
                    } 
                }
            }

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] is ParentheticalTextElement)
                {
                    if(elements[i - 1] is LineEnding && elements[i + 1] is LineEnding ||
                       elements[i - 1] is TabElement)
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
                    var actionElements = ScanAction(elements, i).ToArray();
                    var actionText = string.Join("", actionElements.Select(e => e.Text));

                    foreach (var actionElement in actionElements)
                    {
                        elements.Remove(actionElement);
                    }

                    elements.Insert(i, new ActionTextElement(actionText));
                    continue;
                }
            }
        }

        private static IEnumerable<Element> ScanTitlePage(List<Element> elements, int start)
        {
            return elements.Skip(start).TakeWhile(e => !(e is LineEnding));
        }

        private static bool CheckUpper(string text)
        {
            return !text.Any(char.IsLower);
        }

        private static IEnumerable<Element> ScanCharacter(List<Element> elements, int start)
        {
            return elements.Skip(start).TakeWhile(e => !(e is LineEnding) && CheckUpper(e.Text));
        }

        private static IEnumerable<Element> ScanDialogue(List<Element> elements, int start)
        {
            return elements.Skip(start).TakeWhile(e => !(e is LineEnding));
        }

        private static IEnumerable<Element> ScanForward(List<Element> elements, int start)
        {
            return elements.Skip(start).TakeWhile(e => !(e is LineEnding));
        }

        private static IEnumerable<Element> ScanBackward(List<Element> elements, int start)
        {
            for (int i = start; i < elements.Count; i--)
            {
                if (elements[i] is LineEnding ||
                    elements[i] is TabElement)
                {
                    yield break;
                }

                yield return elements[i];
            }
        }

        private static IEnumerable<Element> ScanAction(List<Element> elements, int start)
        {
            return elements.Skip(start).TakeWhile(e => !(e is LineEnding) && !(e is NoteTextElement));
        }
    }
}
