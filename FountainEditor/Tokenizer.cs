using FountainEditor.Elements;
using System.Collections.Generic;

namespace FountainEditor
{
    public class Tokenizer
    {
        public List<Element> Parse(string text)
        {
            var tokenReader = new TokenReader(text);
            var list = new List<Element>();

            while (!tokenReader.EndOfString)
            {
                list.Add(ParseElement(tokenReader));
            }

            return list;
        }

        public Element ParseElement(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar() == ' ')
                {
                    var word = tokenReader.GetToken();
                    tokenReader.SkipChar();

                    switch (word.ToLower())
                    {
                        case "int.":
                            return new SceneHeadingTextElement(word);
                
                        case "ext.":
                            return new SceneHeadingTextElement(word);
                        
                        case "to:":
                            return new TransitionTextElement(word);

                        default:
                            return new NullTextElement(word);
                    }
                }
                
                if (tokenReader.PeekChar(0) == '\r' && tokenReader.PeekChar(1) == '\n')
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
                
                if (tokenReader.PeekChar(0) == '[' &&
                    tokenReader.PeekChar(1) == '[')
                {
                    tokenReader.TakeChar(2);
                    return ScanNote(tokenReader);
                }
                
                //if (tokenReader.PeekChar(0) == '/' &&
                //    tokenReader.PeekChar(1) == '*')
                //{
                //    tokenReader.TakeChar(2);
                //    return ScanBoneyard(tokenReader);
                //}
                
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

                tokenReader.TakeChar();
            }
            
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

                default:
                    return new NullTextElement(lastword);
            }
        }

        private Element ScanParenthetical(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar(0) == '\r' &&
                    tokenReader.PeekChar(1) == '\n')
                {
                    tokenReader.TakeChar(2);
                    break;
                }

                if (tokenReader.PeekChar() == ')')
                {
                    return new ParentheticalTextElement(tokenReader.GetToken());
                }

                tokenReader.TakeChar();
            }
            return new NullTextElement(tokenReader.GetToken());
        }

        private Element ScanLyrics(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar(0) == '\r' &&
                    tokenReader.PeekChar(1) == '\n')
                {
                    tokenReader.TakeChar(2);
                    break;
                }

                tokenReader.TakeChar();
            }

            return new LyricsTextElement(tokenReader.GetToken());
        }

        private Element ScanTransition(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar(0) == '\r' &&
                    tokenReader.PeekChar(1) == '\n')
                {
                    tokenReader.TakeChar(2);
                    break;
                }

                if (tokenReader.PeekChar(0) == '<')
                {
                    tokenReader.TakeChar();
                    return new CenteredTextElement(tokenReader.GetToken());
                }

                tokenReader.TakeChar();
            }

            return new TransitionTextElement(tokenReader.GetToken());
        }

        //private Element ScanBoneyard(TokenReader tokenReader)
        //{
        //    while (!tokenReader.EndOfString)
        //    {
        //        if (tokenReader.PeekChar(0) == '*' &&
        //            tokenReader.PeekChar(1) == '/')
        //        {
        //            tokenReader.TakeChar(2);
        //            return new BoneyardTextElement(tokenReader.GetToken());
        //        }

        //        tokenReader.TakeChar();
        //    }

        //    return new NullTextElement(tokenReader.GetToken());
        //}

        private Element ScanNote(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
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

        private Element ScanSynopsis(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar(0) == '\r' &&
                    tokenReader.PeekChar(1) == '\n')
                {
                    tokenReader.TakeChar(2);
                    break;
                }

                if (tokenReader.PeekChar(0) == '=' &&
                    tokenReader.PeekChar(1) == '=')
                {
                    tokenReader.TakeChar(2);
                    return new PageBreakTextElement(tokenReader.GetToken());
                }

                tokenReader.TakeChar();
            }

            return new SynopsisTextElement(tokenReader.GetToken());
        }

        private Element ScanOutline(TokenReader tokenReader)
        {
            int count = 1;

            while (!tokenReader.EndOfString && tokenReader.PeekChar() == '#')
            {
                tokenReader.TakeChar();
                count++;
            }

            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar(0) == '\r' &&
                    tokenReader.PeekChar(1) == '\n')
                {
                    tokenReader.TakeChar(2);
                    break;
                }

                tokenReader.TakeChar();
            }

            return new OutlineTextElement(tokenReader.GetToken(), count);
        }
    }
}
