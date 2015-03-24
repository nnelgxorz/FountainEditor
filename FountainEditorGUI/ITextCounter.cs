using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public interface ITextCounter
    {
        Range<int> CountMarkdownSymbols(string text);

        int CountHashTags(string text);
    }
}
