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
using System.Windows.Media.Animation;

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
            { return; }
            
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
    }
}
