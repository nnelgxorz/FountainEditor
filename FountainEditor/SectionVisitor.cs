using System.Collections.Generic;
using FountainEditor.ObjectModel;

namespace FountainEditor
{
    public sealed class SectionVisitor : FountainVisitor
    {
        private SectionElement root;
        private Stack<SectionElement> stack = new Stack<SectionElement>();

        public SectionElement RootSection
        {
            get { return root; }
        }

        public SectionVisitor()
        {
            root = new SectionElement("");
            stack.Push(root);
        }

        public override void Visit(SectionElement element)
        {
            var parent = stack.Pop();
            if (parent.Level < element.Level)
            {
                parent.Children.Add(element);
                stack.Push(parent);
            }

            stack.Push(element);
        }

        public override void VisitAny(Element element)
        {
            if (element is SectionElement)
            {
                return;
            }

            var parent = stack.Pop();
            parent.Children.Add(element);

            stack.Push(parent);
        }
    }
}
