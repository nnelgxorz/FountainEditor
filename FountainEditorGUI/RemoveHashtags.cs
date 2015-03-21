using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public sealed class RemoveHashtags
    {
        public string Remove(string text, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                text.Substring(1, text.Length);
            }

            return text;
        }
    }
}
