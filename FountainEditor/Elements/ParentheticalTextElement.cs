﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    class ParentheticalTextElement : IElement
    {
        public string Text { get; set; }

        public ParentheticalTextElement (string text)
        {
            this.Text = text;
        }
    }
}