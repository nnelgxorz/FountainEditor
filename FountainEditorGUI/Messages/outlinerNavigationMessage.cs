using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI.Messages
{
    public sealed class OutlinerNavigationMessage
    {
        public string text { get; private set; }

        public OutlinerNavigationMessage(string text)
        {
            this.text = text;
        }
    }
}
