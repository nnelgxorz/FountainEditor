﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class cTitlePageKey : Element
    {
        public cTitlePageKey(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("{0}", Text);
        }
    }
}