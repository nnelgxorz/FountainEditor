using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public sealed class GenerateSubListOfSectionIndices
    {
        public List<SectionIndexClass> Generate (List<SectionIndexClass> indices, int startDrag, int endDrag)
        {
            List<SectionIndexClass> subList = new List<SectionIndexClass>();
            if (startDrag == endDrag)
            {
                subList.Add(indices.ElementAt(startDrag));
            }
            for (int i = startDrag; i < endDrag; i++)
            {
                subList.Add(indices.ElementAt(i));
            }

            return subList;
        }
    }
}
