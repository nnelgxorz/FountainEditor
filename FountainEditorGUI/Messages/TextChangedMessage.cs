using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI.Messages
{
    public class TextChangedMessage
    {
        public string Text { get; set; }

        public TextChangedMessage(string text)
        {
            this.Text = text;
        }
    }
}
