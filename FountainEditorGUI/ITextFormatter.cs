using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    interface ITextFormatter
    {
        string TextFormat (TextSelection selection, TextRange textRange);
    }
}
