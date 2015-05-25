using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class FindSectionElementIndexFromText
    {
        public int Find (List<SectionIndexClass> indices, string search)
        {
            int index = 0;
            for (int i = 0; i < indices.Count; i++)
            {
                string currentText = indices.ElementAt(i).text;
                if (currentText.Equals(search))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }
    }
}
