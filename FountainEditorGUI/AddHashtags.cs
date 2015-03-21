using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public sealed class AddHashtags
    {
        public string Add (string text, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                text = string.Format("#{0}", text);
            }

            return text;
        }
    }
}
