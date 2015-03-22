using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public class MoveTextPointerToNextContext
    {
        public TextPointer Move (TextPointer pointer, LogicalDirection lookDirection, LogicalDirection moveDirection, TextPointerContext context)
        {
            while (pointer.GetPointerContext(lookDirection) != context)
            {
                pointer = pointer.GetNextContextPosition(moveDirection);
            }

            return pointer;
        }
    }
}
