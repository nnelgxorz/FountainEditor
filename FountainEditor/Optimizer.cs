﻿using System;
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
                if (elements[i] is LineEnding)
                {
                    i++;
                }

                if (elements[i] is NullTextElement &&
                    CheckUpper(elements[i].Text) &&
                    !elements[i].Text.StartsWith("^"))
                {
                    var characterElements = ScanCharacter(elements, i).ToArray();
                    var characterName = string.Join(" ", characterElements.Select(e => e.Text));

                    foreach (var characterElement in characterElements)
                    {
                        elements.Remove(characterElement);
                    }

                    elements.Insert(i, new CharacterTextElement(characterName));
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

                        processDialogue(elements, i);
                        i++;
                    }
                    else
                    {
                        processDialogue(elements, i);
                        i++;
                    }
                }

                if (elements[i] is NullTextElement && elements[i].Text.StartsWith("."))
                {
                    var sceneHeadingElements = ScanSceneHeading(elements, i).ToArray();
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
                    var sceneHeadingElements = ScanSceneHeading(elements, i).ToArray();
                    var sceneHeading = string.Join(" ", sceneHeadingElements.Select(e => e.Text));

                    foreach (var sceneHeadingElement in sceneHeadingElements)
                    {
                        elements.Remove(sceneHeadingElement);
                    }

                    elements.Insert(i, new SceneHeadingTextElement(sceneHeading));
                    i++;
                }


                if (elements[i] is NullTextElement &&
                    elements[i].Text.StartsWith("^") &&
                    CheckUpper(elements[i].Text))
                {
                    var characterElements = ScanCharacter(elements, i).ToArray();
                    var characterName = string.Join(" ", characterElements.Select(e => e.Text));

                    foreach (var characterElement in characterElements)
                    {
                        elements.Remove(characterElement);
                    }

                    elements.Insert(i, new DualDialogueTextElement(characterName));
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

                        processDialogue(elements, i);
                        i++;
                    }
                    else
                    {
                        processDialogue(elements, i);
                        i++;
                    }
                }

                if (elements[i] is TransitionTextElement &&
                    elements[i].Text.Contains("TO:"))
                {
                    var transitionElements = ScanTransition(elements, i).ToArray();
                    var transitionText = string.Join(" ", transitionElements.Select(e => e.Text));

                    foreach (var transitionElement in transitionElements)
                    {
                        elements.Remove(transitionElement);
                    }

                    elements.Insert(i, new TransitionTextElement(transitionText));
                    i++;
                }

                if (elements[i] is NullTextElement)
                {
                    var actionElements = ScanAction(elements, i).ToArray();
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

        private IEnumerable<Element> ScanAction(List<Element> elements, int start)
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

                if (CheckUpper(elements[i].Text) == false)
                    break;

                if (elements[i] is TransitionTextElement)
                    break;

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
            {
                //TODO: two consecutive lineendings breaks line?
                if (elements[i] is LineEnding)
                    yield break;

                yield return elements[i];
            }
        }

        public IEnumerable<Element> ScanTransition(List<Element> elements, int start)
        {
            for (int i = start; i < elements.Count; i--)
            {
                if (elements[i] is LineEnding)
                    yield break;

                yield return elements[i];
            }
        }

        private void processDialogue(List<Element> elements, int start)
        {
            var dialogueElements = ScanDialogue(elements, start).ToArray();
            var dialogue = string.Join(" ", dialogueElements.Select(e => e.Text));

            foreach (var dialogueElement in dialogueElements)
            {
                elements.Remove(dialogueElement);
            }

            elements.Insert(start, new DialogueTextElement(dialogue));
            start++;
        }
    }
}
