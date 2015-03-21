using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class GetTextOffsetService
    {
        public int GetOffset(TextPointer start, TextPointer end)
        {
            int offset = start.GetOffsetToPosition(end);
            return offset;
        }
    }
}
