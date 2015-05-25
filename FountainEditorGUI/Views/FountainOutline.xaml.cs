using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using FountainEditor.Messaging;
using FountainEditorGUI.Messages;
using FountainEditorGUI.ViewModels;

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
        private string dropAction;
        private int dragIndex;
        private int dropIndex;
        private int dragItemDepth;
        private int dropItemDepth;
        private ITextCounter counter;
        private GetDropActionFromMousePosition getDropAction;
        private IMessagePublisher<DragDropMessage> dragDropMessagePublisher;
        private IMessagePublisher<OutlinerNavigationMessage> outlineNavigationMessage;
        private IMessagePublisher<OutlinerSelectionMessage> outlinerSelectionMessage;

        public FountainOutline(
            FountainOutlineViewModel viewModel, 
            IMessagePublisher<DragDropMessage> dragDropMessagePublisher,
            IMessagePublisher<OutlinerNavigationMessage> outlineNavigationMessage, 
            ITextCounter counter, 
            IMessagePublisher<OutlinerSelectionMessage> outlinerSelectionMessage, 
            GetDropActionFromMousePosition getDropAction)
        {
            InitializeComponent();

            this.dragDropMessagePublisher = dragDropMessagePublisher;
            this.outlineNavigationMessage = outlineNavigationMessage;
            this.outlinerSelectionMessage = outlinerSelectionMessage;
            this.DataContext = viewModel;
            this.counter = counter;
            this.getDropAction = getDropAction;

            outlinerSelectionMessage.Subscribe(OnOutlinerSelectionChanged);
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dragIndex = GetCurrentIndex(e.GetPosition);
            dragItem = (string)Outliner.Items[dragIndex];

            if (dragItem.StartsWith("#"))
            {
                dragItemDepth = counter.CountHashTags(dragItem);
                DragDrop.DoDragDrop(Outliner, dragItem, DragDropEffects.Move | DragDropEffects.Scroll);
            }
        }

        private void Outliner_DragEnter(object sender, DragEventArgs e)
        {
            dropIndex = GetCurrentIndex(e.GetPosition);

            if (dropIndex < 0)
            {
                return;
            }

            dropItem = (string)Outliner.Items[dropIndex];
            dropItemDepth = counter.CountHashTags(dropItem);
            dropAction = getDropAction.GetAction(Outliner, dropIndex, dropItemDepth, e);
            
        }

        private void Outliner_Drop(object sender, DragEventArgs e)
        {
            if (dragIndex == dropIndex)
            {
                return;
            }

            dragDropMessagePublisher.Publish(new DragDropMessage
            (
                this.dragIndex, 
                this.dropIndex,
                this.dragItem, 
                this.dropItem,
                this.dragItemDepth, 
                this.dropItemDepth,
                this.dropAction
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

        private void OnOutlinerSelectionChanged(OutlinerSelectionMessage message)
        {
            this.Outliner.SelectedItems.Clear();
            this.Outliner.SelectedItems.Add(message.text);
            this.Outliner.ScrollIntoView(message.text);
        }
    }

}
