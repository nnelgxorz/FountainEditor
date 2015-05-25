using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class GenerateIndexListOfSectionElementsInDocument
    {
        private TextCounter textCounter;
        public GenerateIndexListOfSectionElementsInDocument(TextCounter textCounter)
        {
            this.textCounter = textCounter;
        }
        public List<SectionIndexClass> Generate(FlowDocument document)
        {
            List<SectionIndexClass> indices = new List<SectionIndexClass>();
            for (int blockIndex = 0; blockIndex < document.Blocks.Count; blockIndex++)
            {
                Block block = document.Blocks.ElementAt(blockIndex);
                string text = new TextRange(block.ContentStart, block.ContentEnd).Text;
                if (text.StartsWith("#"))
                {
                    int hashCount = textCounter.CountHashTags(text);
                    SectionIndexClass index = new SectionIndexClass(blockIndex, text, 0, hashCount);
                    indices.Add(index);
                }
            }

            int blockAmount;
            int count = indices.Count;

            for (int i = 0; i < count; i++)
            {
                SectionIndexClass index = indices.ElementAt(i);
                if (i == count - 1)
                {
                    blockAmount = document.Blocks.Count - index.index;
                    index.blockAmount = blockAmount;
                }
                else
                {
                    blockAmount = indices.ElementAt(i + 1).index - index.index;
                    index.blockAmount = blockAmount;
                }
            }
            return indices;
        }
    }
}
