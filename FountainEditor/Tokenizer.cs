﻿using FountainEditor.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace FountainEditor
{
    class Tokenizer
    {
        public void Parse()
        {
            var tokenReader = new TokenReader("# Dick Butt ");

            ParseElement(tokenReader);
        }

        private Element ParseElement(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar() == ' ' ||
                    tokenReader.PeekChar() == '\r' ||
                    tokenReader.PeekChar() == '\n')
                {
                    tokenReader.TakeChar();
                    var word = tokenReader.GetToken();

                    switch (word)
                    {
                        case "int.":
                            return new SceneHeadingTextElement(word);
                
                        case "ext.":
                            return new SceneHeadingTextElement(word);
                        
                        case "to:":
                            return new TransitionTextElement(word);

                        default:
                            return new NullElement(word);
                            
                    }
                }

                if (tokenReader.PeekChar() == '#')
                {
                    tokenReader.TakeChar();
                    var token = ScanOutline(tokenReader);
                }

                if (tokenReader.PeekChar() == '=')
                {
                    tokenReader.TakeChar();
                    var token = ScanSynopsis(tokenReader);
                }

                if (tokenReader.PeekChar(0) == '[' &&
                    tokenReader.PeekChar(1) == '[')
                {
                    tokenReader.TakeChar(2);
                    var token = ScanNote(tokenReader);
                }

                if (tokenReader.PeekChar(0) == '/' &&
                    tokenReader.PeekChar(1) == '*')
                {
                    tokenReader.TakeChar(2);
                    var token = ScanBoneyard(tokenReader);
                }

                if (tokenReader.PeekChar(0) == '>')
                {
                    tokenReader.TakeChar(0);
                    var token = ScanTransition(tokenReader);
                }

                if (tokenReader.PeekChar() == '~')
                {
                    tokenReader.TakeChar();
                    var token = ScanLyrics(tokenReader);
                }

                tokenReader.TakeChar();
            }
        }


        private Element ScanLyrics(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar() == '\r'
                    && tokenReader.PeekChar() == '\n')
                {
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

        private Element ScanBoneyard(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar(0) == '*' &&
                    tokenReader.PeekChar(1) == '/')
                {
                    tokenReader.TakeChar(2);
                    return new BoneyardTextElement(tokenReader.GetToken());
                }

                tokenReader.TakeChar();
            }
            return new NullElement(tokenReader.GetToken());
        }

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

            return new NullElement(tokenReader.GetToken());
        }

        private Element ScanSynopsis(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar(0) == '\r' &&
                    tokenReader.PeekChar(1) == '\n')
                {
                    return new SynopsisTextElement(tokenReader.GetToken());
                }

                if (tokenReader.PeekChar(0) == '=' &&
                    tokenReader.PeekChar(1) == '=')
                {
                    return new PageBreakTextElement(tokenReader.GetToken());
                }

                tokenReader.TakeChar();
            }

            return new SynopsisTextElement(tokenReader.GetToken());
        }

        private Element ScanOutline(TokenReader tokenReader)
        {
            int count = 1;
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar() != '#')
                count++;
                tokenReader.TakeChar();
            }

            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar(0) == '\r' &&
                    tokenReader.PeekChar(1) == '\n')
                {
                    return new OutlineTextElement(tokenReader.GetToken(), count);
                }

                tokenReader.TakeChar();
            }

            return new OutlineTextElement(tokenReader.GetToken(), count);
        }
    }
}
