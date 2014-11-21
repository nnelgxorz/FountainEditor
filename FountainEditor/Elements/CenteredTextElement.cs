﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class CenteredTextElement : Element
    {
        public CenteredTextElement(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<CenT> {0} </CenT>", Text);
        }
    }
}
