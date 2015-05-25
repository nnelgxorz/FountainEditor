using FountainEditor.Messaging;
using FountainEditorGUI.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public sealed class ReOrderSectionIndicesService
    {
        private IMessagePublisher<SectionIndicesChangedMessage> indicesChangedMessage;
        public ReOrderSectionIndicesService(
            IMessagePublisher<SectionIndicesChangedMessage> indicesChangedMessage)
        {
            this.indicesChangedMessage = indicesChangedMessage;
        }

        public List<SectionIndexClass> ReOrder(List<SectionIndexClass> indices)
        {
            indices.ElementAt(0).index = 0;

            for (int i = 1; i < indices.Count; i++)
            {
                var section = indices.ElementAt(i);
                var prevSection = indices.ElementAt(i - 1);

                int newIndex = prevSection.index + prevSection.blockAmount;
                section.index = newIndex;
            }
            return indices;
        }
    }
}
