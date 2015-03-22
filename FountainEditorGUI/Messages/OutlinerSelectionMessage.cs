using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI.Messages
{
    public sealed class OutlinerSelectionMessage
    {
        public string text;
        public OutlinerSelectionMessage(string text)
        {
            this.text = text;
        }
    }
}
