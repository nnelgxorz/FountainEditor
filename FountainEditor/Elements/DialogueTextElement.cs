﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class DialogueTextElement : Element
    {
        public DialogueTextElement(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<Dial> {0} </Dial>", Text);
        }
    }
}
