using FountainEditorGUI.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class GetPointersAtStartAndEndOfSectionTextHierarchy
    {
    //    private FindSectionElementIndexFromText findSectionIndex;
    //    private FindEndOfSectionHierarchyIndex findEndOfHierarchyIndex;
    //    public GetPointersAtStartAndEndOfSectionTextHierarchy(
    //        FindSectionElementIndexFromText findSectionIndex,
    //        FindEndOfSectionHierarchyIndex findEndOfHierarchyIndex)
    //    {
    //        this.findSectionIndex = findSectionIndex;
    //        this.findEndOfHierarchyIndex = findEndOfHierarchyIndex;
    //    }
    //    public TextRange Selection(FlowDocument document, List<SectionIndexClass> indexes, string search)
    //    {
    //        int startIndex = findSectionIndex.Find(indexes, document, search);
    //        int depth = indexes.ElementAt(startIndex).hashCount;
    //        int stopIndex = findEndOfHierarchyIndex.Find(document, indexes, depth, startIndex);
    //        TextPointer start = document.Blocks.ElementAt(startIndex).ContentStart;
    //        TextPointer end = document.Blocks.ElementAt(stopIndex).ContentEnd;

    //        if (stopIndex == 0 || stopIndex < startIndex)
    //        {
    //            end = document.ContentEnd;
    //        }

    //        return new TextRange(start, end);
    //    }
    }
}
