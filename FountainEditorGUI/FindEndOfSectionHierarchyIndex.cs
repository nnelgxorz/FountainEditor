using System.Collections.Generic;
using System.Linq;

namespace FountainEditorGUI
{
    public sealed class FindEndOfSectionHierarchyIndex
    {
        public int Find(List<SectionIndexClass> indices, int start)
        {
            int index = 0;
            int depth = indices.ElementAt(start).hashCount;

            if (start == indices.Count - 1)
            {
                index = start;
            }
            else
            {
                for (int i = start + 1; i < indices.Count; i++)
                {
                    var section = indices.ElementAt(i);
                    int currentDepth = section.hashCount;
                    if (currentDepth <= depth)
                    {
                        index = i;
                        break;
                    }
                }
            }

            return index;
        }
    }
}
