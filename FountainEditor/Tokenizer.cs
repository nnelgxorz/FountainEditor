using FountainEditor.Elements;
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
                        case "int." : 
                            //TODO: Generate new Scene Heading object
                            break;
                        case "ext." :
                            //TODO: Generate new Scene Heading object
                            break;
                        case "to:" :
                            //TODO: Generate new Transition object
                            break;
                        default :
                            //TODO: Generate new Null object
                            break;
                    }
                }

                if (tokenReader.PeekChar() ==  '#')
                {
                    tokenReader.TakeChar();
                    var token = ScanOutline(tokenReader);
                }

                if (tokenReader.PeekChar() == '=')
                {
                    tokenReader.TakeChar();
                    var token = ScanSynopsis(tokenReader);
                }

                if  (tokenReader.PeekChar(0) == '[' &&
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


        private string ScanLyrics(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar() == '\r' 
                    && tokenReader.PeekChar() == '\n')
                {
                    tokenReader.GetToken();
                    //TODO: Generate new Lyrics object
                }

                tokenReader.TakeChar();
                //TODO: Generate new Lyrics Object
            }

            return tokenReader.GetToken();
        }

        private string ScanTransition(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar(0) == '\r' &&
                    tokenReader.PeekChar(1) == '\n')
                {
                    return tokenReader.GetToken();
                    //TODO: Generate new Transition object
                }
                if (tokenReader.PeekChar(0) == '<')
                {
                    tokenReader.TakeChar();
                    return tokenReader.GetToken();
                    //TODO: Generate new Centered Text object

                }
                tokenReader.TakeChar();
                //TODO: Generate new Transition object

            }
            return tokenReader.GetToken();
        }

        private string ScanBoneyard(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar(0) == '*' &&
                    tokenReader.PeekChar(1) == '/')
                {
                    tokenReader.TakeChar(2);
                    return tokenReader.GetToken();
                    //TODO: Generate new Transition object
                }

                tokenReader.TakeChar();
            }
            //TODO: Generate new Action object
            return tokenReader.GetToken();
        }

        private string ScanNote(TokenReader tokenReader)
        {
           while (!tokenReader.EndOfString)
           {
               if(tokenReader.PeekChar(0) == ']' &&
                   tokenReader.PeekChar(1) == ']')
               {
                   tokenReader.TakeChar(2);
                   return tokenReader.GetToken();
                   //TODO: Generate new Note object
               }

               tokenReader.TakeChar();
           }

           return tokenReader.GetToken();
            //TODO: Generate new Action object
        }

        private string ScanSynopsis(TokenReader tokenReader)
        {
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar(0) == '\r' &&
                    tokenReader.PeekChar(1) == '\n')
                {
                    return tokenReader.GetToken();
                    //TODO: Generate new Synopsis object
                }

                if (tokenReader.PeekChar(0) == '=' &&
                    tokenReader.PeekChar(1) == '=')
                {
                    tokenReader.TakeChar(2);
                    return tokenReader.GetToken();
                    //TODO: Generate new PageBreak object
                }

                tokenReader.TakeChar();
                //TODO: Generate new Synopsis object
            }

            return tokenReader.GetToken();
        }

        private string ScanOutline(TokenReader tokenReader)
        {
            int count = 1;
            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar() != '#')
                    break;
                count++;
                tokenReader.TakeChar();
            }

            while (!tokenReader.EndOfString)
            {
                if (tokenReader.PeekChar(0) == '\r' && 
                    tokenReader.PeekChar(1) == '\n')
                {
                    return tokenReader.GetToken();
                    //TODO: Generate new Outline object
                }

                tokenReader.TakeChar();
            }

            return tokenReader.GetToken();
            //TODO: Generate new Outline object
        }      
    }
}
