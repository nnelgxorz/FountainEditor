using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using FountainEditor.Messaging;
using FountainEditorGUI.Messages;
using FountainEditorGUI.ViewModels;
using System.Windows.Documents;

namespace FountainEditorGUI.Views
{
    /// <summary>
    /// Interaction logic for FountainOutline.xaml
    /// </summary>
    public partial class FountainOutline : UserControl
    {
        private delegate Point GetPositionDelegate(IInputElement element);

        private string dragItem;
        private string dropItem;
        private int dragIndex;
        private int dropIndex;
        private int dragItemDepth;
        private int dropItemDepth;
        private CountHashTags countHashTags;
        private IMessagePublisher<DragDropMessage> dragDropMessagePublisher;
        private IMessagePublisher<OutlinerNavigationMessage> outlineNavigationMessage;
        private AdornerLayer adornerLayer;

        public FountainOutline(FountainOutlineViewModel viewModel, IMessagePublisher<DragDropMessage> dragDropMessagePublisher,
            IMessagePublisher<OutlinerNavigationMessage> outlineNavigationMessage, CountHashTags countHashTags)
        {
            InitializeComponent();

            this.dragDropMessagePublisher = dragDropMessagePublisher;
            this.outlineNavigationMessage = outlineNavigationMessage;
            this.DataContext = viewModel;
            this.countHashTags = countHashTags;
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dragIndex = GetCurrentIndex(e.GetPosition);
            dragItem = (string)Outliner.Items[dragIndex];

            if (dragItem.StartsWith("#"))
            {
                dragItemDepth = countHashTags.Count(dragItem);
                DragDrop.DoDragDrop(Outliner, dragItem, DragDropEffects.Move | DragDropEffects.Scroll);
            }
        }

        private void Outliner_DragEnter(object sender, DragEventArgs e)
        {
            dropIndex = GetCurrentIndex(e.GetPosition);
            dropItem = (string)Outliner.Items[dropIndex];
            dropItemDepth = countHashTags.Count(dropItem);
        }

        private void Outliner_Drop(object sender, DragEventArgs e)
        {
            if (dragIndex == dropIndex | !(dragItemDepth >= dropItemDepth))
            {
                return;
            }

            dragDropMessagePublisher.Publish(new DragDropMessage
            (
                this.dragIndex, this.dropIndex,
                this.dragItem, this.dropItem,
                this.dragItemDepth, this.dropItemDepth
            ));
        }

        private void Outliner_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var index = GetCurrentIndex(e.GetPosition);
            string itemText = (string) this.Outliner.Items.GetItemAt(index);

            outlineNavigationMessage.Publish(new OutlinerNavigationMessage(itemText));
        }

        private int GetCurrentIndex (GetPositionDelegate getPosition)
        {
            int index = -1;

            for (int i = 0; i < Outliner.Items.Count; i++)
            {
                var item = GetListViewItem(i);
                if (item == null)
                {
                    continue;
                }

                if (IsMouseOverTarget(item, getPosition))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }
            
        private ListViewItem GetListViewItem (int index)
        {
            if (Outliner.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
            {
                return null;
            }

            return Outliner.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
        }

        private bool IsMouseOverTarget(Visual target, GetPositionDelegate getPosition)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            Point mousePosition = getPosition((IInputElement)target);
            return bounds.Contains(mousePosition);
        }
    }

}
