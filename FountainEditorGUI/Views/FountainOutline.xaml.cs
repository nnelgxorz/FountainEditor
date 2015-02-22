using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Data;
using System.Windows.Input;
using FountainEditorGUI.ViewModels;
using System;
using FountainEditor.Messaging;
using FountainEditorGUI.Messages;

namespace FountainEditorGUI.Views
{
    /// <summary>
    /// Interaction logic for FountainOutline.xaml
    /// </summary>
    public partial class FountainOutline : UserControl
    {
        public FountainOutline()
        {
            this.dragdropmessagepublisher = ServiceLocator.Current.GetInstance<IMessagePublisher<DragDropMessage>>();
            InitializeComponent();
        }

        string dragItem;
        int dragIndex;
        int dropIndex;
        private IMessagePublisher<DragDropMessage> dragdropmessagepublisher;

        private void Outliner_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                dragIndex = GetCurrentIndex(e.GetPosition);
                if (dragIndex < 0)
                {
                    while (e.LeftButton == MouseButtonState.Pressed)
                    { return; }
                }

                dragItem = (string)Outliner.Items[dragIndex];
                DragDrop.DoDragDrop(Outliner, dragItem, DragDropEffects.Move);
            }
        }

        private void Outliner_DragEnter(object sender, DragEventArgs e)
        {
            dropIndex = GetCurrentIndex(e.GetPosition);
        }

        private void Outliner_Drop(object sender, DragEventArgs e)
        {
            if (dragIndex == dropIndex)
            { return; }
            
            //MessageBox.Show(string.Format("Inserting Index {0} at Index {1}. Content: {2}", dragIndex, dropIndex, dragItem));
            dragdropmessagepublisher.Publish(new DragDropMessage
            (
                this.dragIndex,
                this.dropIndex,
                this.dragItem
            ));
        }

        delegate Point GetPositionDelegate(IInputElement element);

        private int GetCurrentIndex (GetPositionDelegate getPosition)
        {
            int index = -1;
            for (int i = 0; i < Outliner.Items.Count; i++)
            {
                ListViewItem item = GetListViewItem(i);
                if (item == null)
                { continue; }

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
            if (Outliner.ItemContainerGenerator.Status != System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
                return null;
            return Outliner.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
        }

        private bool IsMouseOverTarget(Visual target, GetPositionDelegate getPosition)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            Point mousePosition = getPosition((IInputElement)target);
            return bounds.Contains(mousePosition);
        }

        private void Grid_DragEnter(object sender, DragEventArgs e)
        {
            Grid g = sender as Grid;
            g.Background = Brushes.SteelBlue;
        }

        private void Grid_DragLeave(object sender, DragEventArgs e)
        {
            Grid g = sender as Grid;
            g.Background = null;
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            Grid g = sender as Grid;
            g.Background = null;
        }
    }
}
