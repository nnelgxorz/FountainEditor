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

        private IMessagePublisher<DragDropMessage> dragDropMessagePublisher;
        private string dragItem;
        private int dragIndex;
        private int dropIndex;

        public FountainOutline(FountainOutlineViewModel viewModel, IMessagePublisher<DragDropMessage> dragDropMessagePublisher)
        {
            InitializeComponent();

            this.dragDropMessagePublisher = dragDropMessagePublisher;
            this.DataContext = viewModel;
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dragIndex = GetCurrentIndex(e.GetPosition);
            dragItem = (string)Outliner.Items[dragIndex];

            if (dragItem.StartsWith("#"))
            {
                DragDrop.DoDragDrop(Outliner, dragItem, DragDropEffects.Move | DragDropEffects.Scroll);
            }
        }

        private void Outliner_DragEnter(object sender, DragEventArgs e)
        {
            dropIndex = GetCurrentIndex(e.GetPosition);
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
                this.dragItem
            ));
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
