﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class rTitlePageKey : Element
    {
        public rTitlePageKey(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("{0}", Text);
        }
    }
}