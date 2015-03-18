using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    interface ITextTrimmer
    {
        string TrimText (string text, int start, int end);
    }
}
