using FountainEditor.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace FountainEditor
{
    class Tokenizer
    {
        private static Regex Emphasis;
        private static Regex Outline;
        private static Regex Equals;
        private static Regex Boneyard;
        private static Regex Character;
        private static Regex SceneHeading;
        private static Regex Parenthetical;

        static Tokenizer()
        {
            try
            {
                Emphasis = new Regex(@"^([\*_]+)([^\*_]*)([\*_]+)", RegexOptions.Compiled);
                Outline = new Regex(@"((#+)(\s*[^\n]*))\n?", RegexOptions.Compiled);
                Equals = new Regex(@"(=+)", RegexOptions.Compiled);
                Boneyard = new Regex(@"(\/\*)([^\*]*)(\*\/)", RegexOptions.Compiled);
                Character = new Regex(@"^([^a-z*_]+)$|(^[@]+)([a-zA-Z ]*)", RegexOptions.Compiled);
                SceneHeading = new Regex(@"([iI][nN][tT]|[eE][xX][tT]|^\.(?=[a-zA-Z]))(.*)", RegexOptions.Compiled);
                Parenthetical = new Regex(@"(\([^<>]*?\)[\\s]?)\n", RegexOptions.Compiled);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public Element Parse(string userText)
        {
            var match = Outline.Match(userText);
            var hashNumbers = match.Groups[2].Length;
            var outlineLevel = hashNumbers.ToString();

            if (match.Groups[2].Success)
            {
                Console.WriteLine("Outline Level {0}", outlineLevel);
                return new OutlineTextElement(userText);
            }

            var amount = Equals.Match(userText);
            var equalsNumber = amount.Groups[1].Length;

            if (equalsNumber == 1)
            {
                Console.WriteLine("Synopsis");
                return new SynopsisTextElement(userText);
            }

            if (equalsNumber == 3)
            {
                Console.WriteLine("Page Break");
                return new PageBreakTextElement("");
            }

            if (equalsNumber == 2 || (equalsNumber > 3))
            {
                Console.WriteLine("Text");
                return new TextTextElement(userText);
            }

            if (string.IsNullOrEmpty(userText))
            {
                Console.WriteLine("Blank");
                return new BlankTextElement();
            }

            if (userText.StartsWith("[[") || userText.EndsWith("]]"))
            {
                Console.WriteLine("Note");
                return new NoteTextElement("");
            }

            if (userText.StartsWith("~"))
            {
                Console.WriteLine("Lyrics");
                return new LyricsTextElement(userText);
            }

            var isScene = SceneHeading.Match(userText);
            if (isScene.Groups[1].Success)
            {
                Console.WriteLine("Scene Heading");
                return new SceneHeadingTextElement(userText);
            }

            if (userText.StartsWith(">") && userText.EndsWith("<"))
            {
                Console.WriteLine("Centered Text");
                return new CenteredTextElement(userText);
            }

            if (userText.StartsWith(">") || userText.EndsWith("To:", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Transition");
                return new TransitionTextElement(userText);
            }

            var character = Character.Match(userText).Success;
            if (character == true)
            {
                Console.WriteLine("Character Name");
                return new CharacterTextElement(userText);
            }

            if (userText.StartsWith("(") && userText.EndsWith(")"))
            //var parenth = Parenthetical.Match(UserText).Success;
            //if (parenth == true)
            {
                Console.WriteLine("Parenthetical");
                return new ParentheticalTextElement(userText);
            }

            else
            {
                Console.WriteLine("Text");
                return new TextTextElement(userText);
            }
        }

        private static void ParseEmphasis(string userText)
        {
            var match = Emphasis.Match(userText);

            if (match.Groups[1].Value != match.Groups[3].Value)
            {
                Console.WriteLine("Emphasis Failed");
                return;
            }

            switch (match.Groups[1].Value)
            {
                case "*":
                    Console.WriteLine("Italics");
                    break;
                case "**":
                    Console.WriteLine("Bold");
                    break;
                case "***":
                    Console.WriteLine("Bold Italics");
                    break;
                case "_":
                    Console.WriteLine("Underlined");
                    break;

                default:
                    Console.WriteLine("Emphasis Failed");
                    break;
            }
        }

        internal List<Element> Parse(TextReader textReader)
        {
            var list = new List<Element>();

            string line;

            while ((line = textReader.ReadLine()) != null)
            {
                Console.Write("{0,-50} - ", line);
                list.Add(Parse(line));
            }

            Console.WriteLine();

            return list;
        }
    }
}
