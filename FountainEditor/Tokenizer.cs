using FountainEditor.Elements;
using System.Collections.Generic;

namespace FountainEditor
{
    public static class Tokenizer
    {
        public static List<Element> Parse(string text)
        {
            var tokenReader = new TokenReader(text);
            var list = new List<Element>();

            while (!tokenReader.EndOfString)
            {
                list.Add(ParseElement(tokenReader));
            }

            return list;
        }

        private static Element ParseElement(TokenReader tokenReader)
        {
            while (!tokenReader.LastChar)
            {
                if (tokenReader.PeekChar() == ' ')
                {
                    if (tokenReader.PeekChar(1) == ' ')
                    {
                        tokenReader.TakeChar(2);
                        return new DoubleSpaceElement(tokenReader.GetToken());
                    }

                    else
                    {
                        tokenReader.TakeChar();
                        return new SingleSpaceElement(tokenReader.GetToken());
                    }
                }

                if (tokenReader.PeekChar() == '\t')
                {
                    tokenReader.TakeChar();
                    return ScanTabs(tokenReader);
                }

                if (tokenReader.PeekChar(0) == '\r' && 
                    tokenReader.PeekChar(1) == '\n')
                {
                    tokenReader.TakeChar(2);
                    return new LineEnding(tokenReader.GetToken());
                }
                else if (tokenReader.PeekChar() == '\r')
                {
                    tokenReader.TakeChar();
                    return new LineEnding(tokenReader.GetToken());
                }
                else if (tokenReader.PeekChar() == '\n')
                {
                    tokenReader.TakeChar();
                    return new LineEnding(tokenReader.GetToken());
                }
                
                if (tokenReader.PeekChar() == '#')
                {
                    tokenReader.TakeChar();
                    return ScanOutline(tokenReader);
                }
                
                if (tokenReader.PeekChar() == '=')
                {
                    tokenReader.TakeChar();
                    return ScanSynopsis(tokenReader);
                }

                if (tokenReader.PeekChar() == '>')
                {
                    tokenReader.TakeChar();
                    return ScanTransition(tokenReader);
                }

                if (tokenReader.PeekChar() == '~')
                {
                    tokenReader.TakeChar();
                    return ScanLyrics(tokenReader);
                }

                if (tokenReader.PeekChar() == '(')
                {
                    tokenReader.TakeChar();
                    return ScanParenthetical(tokenReader);
                }

                if (tokenReader.PeekChar(1) == '\r' ||
                tokenReader.PeekChar(1) == '[' &&
                tokenReader.PeekChar(2) == '[' ||
                tokenReader.PeekChar(1) == ' ' ||
                tokenReader.PeekChar() == ':')
                {
                    tokenReader.TakeChar();
                    var word = tokenReader.GetToken();

                    switch (word.ToLower())
                    {
                        case "int.":
                            return new SceneHeadingTextElement(word);

                        case "ext.":
                            return new SceneHeadingTextElement(word);

                        case "title:":
                            return new TitlePageKey(word);

                        case "credit:":
                            return new TitlePageKey(word);

                        case "author:":
                            return new TitlePageKey(word);

                        case "authors:":
                            return new TitlePageKey(word);

                        case "source:":
                            return new TitlePageKey(word);

                        case "date:":
                            return new TitlePageKey(word);

                        case "draft date:":
                            return new TitlePageKey(word);

                        case "contact:":
                            return new TitlePageKey(word);

                        case "notes:":
                            return new TitlePageKey(word);

                        default:
                            return new NullTextElement(word);
                    }
                }
                
                
                if (tokenReader.PeekChar(0) == '[' &&
                    tokenReader.PeekChar(1) == '[')
                {
                    tokenReader.TakeChar(2);
                    return ScanNote(tokenReader);
                }

                tokenReader.TakeChar();
            }

            tokenReader.TakeChar();
            var lastword = tokenReader.GetToken();
            tokenReader.SkipChar();

            switch (lastword.ToLower())
            {
                case "int.":
                    return new SceneHeadingTextElement(lastword);

                case "ext.":
                    return new SceneHeadingTextElement(lastword);

                case "to:":
                    return new TransitionTextElement(lastword);
                   
                case "\r":
                    return new LineEnding(lastword);

                case "\n":
                    return new LineEnding(lastword);

                default:
                    return new NullTextElement(lastword);
            }
        }

        private static Element ScanTabs(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar() == '\t')
                {
                    tokenReader.TakeChar();
                }
                else
                {
                    return new TabElement(tokenReader.GetToken());
                }
            }

            return new TabElement(tokenReader.GetToken());
        }

        private static Element ScanParenthetical(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.IsLineEnding())
                {
                    return new NullTextElement(tokenReader.GetToken());
                }

                if (tokenReader.PeekChar() == ')')
                {
                    tokenReader.TakeChar();
                    return new ParentheticalTextElement(tokenReader.GetToken());
                }

                tokenReader.TakeChar();
            }

            return new NullTextElement(tokenReader.GetToken());
        }

        private static Element ScanLyrics(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString && !tokenReader.IsLineEnding())
            {
                tokenReader.TakeChar();
            }

            return new LyricsTextElement(tokenReader.GetToken());
        }

        private static Element ScanTransition(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString && !tokenReader.IsLineEnding())
            {
                if (tokenReader.PeekChar(0) == '<')
                {
                    tokenReader.TakeChar();
                    return new CenteredTextElement(tokenReader.GetToken());
                }

                tokenReader.TakeChar();
            }

            return new TransitionTextElement(tokenReader.GetToken());
        }

        private static Element ScanNote(TokenReader tokenReader)
        {
            while (!tokenReader.LastChar)
            {
                if (tokenReader.PeekChar(0) == '\r' &&
                    tokenReader.PeekChar(1) == '\n' &&
                    tokenReader.PeekChar(2) == '\r' &&
                    tokenReader.PeekChar(3) == '\n')
                {
                    return new NullTextElement(tokenReader.GetToken());
                }

                if (tokenReader.PeekChar(0) == ']' &&
                    tokenReader.PeekChar(1) == ']')
                {
                    tokenReader.TakeChar(2);
                    return new NoteTextElement(tokenReader.GetToken());
                }

                tokenReader.TakeChar();
            }

            return new NullTextElement(tokenReader.GetToken());
        }

        private static Element ScanSynopsis(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (!(tokenReader.PeekChar() == '='))
                {
                    var synopsis = tokenReader.GetToken();
                    if (synopsis.Length >= 3)
                    {
                        return new PageBreakTextElement(synopsis);
                    }

                    else
                    {
                        return new SynopsisTextElement(synopsis);
                    }
                }

                tokenReader.TakeChar();
            }

            return new SynopsisTextElement(tokenReader.GetToken());
        }

        private static Element ScanOutline(TokenReader tokenReader)
        {
            int count = 1;

            while (tokenReader.PeekChar() == '#')
            {
                tokenReader.TakeChar();
                count++;
            }

            while (!tokenReader.EndOfString && !tokenReader.IsLineEnding())
            {
                tokenReader.TakeChar();
            }

            return new OutlineTextElement(tokenReader.GetToken(), count);
        }

        private static bool IsLineEnding(this TokenReader reader)
        {
            if (reader.PeekChar(0) == '\r' &&
                reader.PeekChar(1) == '\n')
            {
                return true; // Windows line ending
            }

            if (reader.PeekChar(0) == '\n')
            {
                return true; // Posix line ending
            }

            if (reader.PeekChar(0) == '\r')
            {
                return true; // Apple line ending
            }

            return false;
        }
    }
}
