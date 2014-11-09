using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using FountainEditor.Elements;

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


        public IElement Parse(string UserText)
        {

            var match = Outline.Match(UserText);
            int HashNumbers = match.Groups[2].Length;
            string OutlineLevel = HashNumbers.ToString();
            if (match.Groups[2].Success)
            {
                Console.WriteLine("Outline Level {0}", OutlineLevel);
                return new OutlineTextElement(UserText);
            }

            var Amount = Equals.Match(UserText);
            int EqualsNumber = Amount.Groups[1].Length;

            if (EqualsNumber == 1)
            {
                Console.WriteLine("Synopsis");
                return new SynopsisTextElement(UserText);
            }

            if (EqualsNumber == 3)
            {
                Console.WriteLine("Page Break");
                return new PageBreakTextElement();
            }

            if (EqualsNumber == 2 || (EqualsNumber > 3))
            {
                Console.WriteLine("Text");
                return new TextTextElement(UserText);
            }

            if (string.IsNullOrEmpty(UserText))
            {
                Console.WriteLine("Blank");
                return new BlankTextElement();
            }

            if (UserText.StartsWith("[[") || UserText.EndsWith("]]"))
            {
                Console.WriteLine("Note");
                return new NoteTextElement();
            }

            if (UserText.StartsWith("~"))
            {
                Console.WriteLine("Lyrics");
                return new LyricsTextElement(UserText);
            }

            var isScene = SceneHeading.Match(UserText);
            if (isScene.Groups[1].Success)
            {
                Console.WriteLine("Scene Heading");
                return new SceneHeadingTextElement(UserText);
            }

            if (UserText.StartsWith(">") && UserText.EndsWith("<"))
            {
                Console.WriteLine("Centered Text");
                return new CenteredTextElement(UserText);
            }

            if (UserText.StartsWith(">") || UserText.EndsWith("To:", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Transition");
                return new TransitionTextElement(UserText);
            }

            var Char = Character.Match(UserText).Success;
            if (Char == true)
            {
                Console.WriteLine("Character Name");
                return new CharacterTextElement(UserText);
            }

            if (UserText.StartsWith("(") & (UserText.EndsWith(")")))
            //var Parenth = Parenthetical.Match(UserText).Success;
            //if (Parenth == true)
            {
                Console.WriteLine("Parenthetical");
                return new ParentheticalTextElement(UserText);
            }

            else
            {
                Console.WriteLine("Text");
                return new TextTextElement(UserText);
            }
        }

        private static void ParseEmphasis(string UserText)
        {
            var match = Emphasis.Match(UserText);

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
            return;
        }

        internal List<IElement> Parse(System.IO.TextReader textReader)
        {
            string line;
            var List = new List<IElement>();

            while ((line = textReader.ReadLine()) != null)
            {
                List.Add(Parse(line));
                Console.Write("{0,-50} - ", line);
                Parse(line);
            }

            return List;
        }
    }
}
