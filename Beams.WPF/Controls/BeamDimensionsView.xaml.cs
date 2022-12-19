using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Beams.WPF.ViewModels;

namespace Beams.WPF.Controls
{
    /// <summary>
    /// Interaction logic for BeamDimensionsView.xaml
    /// </summary>
    public partial class BeamDimensionsView : UserControl
    {
        public BeamDimensionsView()
        {
            InitializeComponent();
        }

        private void LengthsListBox_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var listBoxItem = FindAncestor<ListBoxItem>((DependencyObject)e.OriginalSource);
            if (listBoxItem != null)
            {
                DragDrop.DoDragDrop(listBoxItem, listBoxItem.Content, DragDropEffects.Move);
                listBoxItem.IsSelected = true;
            }
        }

        private T FindAncestor<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);
            if (parent == null)
            {
                return null;
            }

            var parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }

        private void LengthsListBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }

        private void LengthsListBox_Drop(object sender, DragEventArgs e)
        {
            var listBox = sender as ListBox;
            BeamDimensionsViewModel target = ((ListBoxItem)(sender)).DataContext as BeamDimensionsViewModel;

            // Get the view model from the DataContext of the listbox
            var viewModel = listBox.DataContext as BeamDimensionsViewModel;

            // Get the item being dropped
            var item = e.Data.GetData(typeof(BeamDimensionViewModel)) as BeamDimensionViewModel;
            
            // Get the index of the item being dropped
            var targetIndex = listBox.Items.IndexOf(item);

            // Get the index of the item being dragged
            var draggedIndex = viewModel.Dimensions.IndexOf(item);

            if (targetIndex != draggedIndex)
            {
                // Remove the item from its original position in the view model
                viewModel.Dimensions.RemoveAt(draggedIndex);

                // Insert the item at the new position in the view model
                viewModel.Dimensions.Insert(targetIndex, item);
            }

            //var item = e.Data.GetData(typeof(BeamDimensionViewModel)) as BeamDimensionViewModel;
            //var targetIndex = listBox.Items.IndexOf(item);
            //var draggedIndex = listBox.SelectedIndex;

            //if (targetIndex != draggedIndex)
            //{
            //    listBox.Items.RemoveAt(draggedIndex);
            //    listBox.Items.Insert(targetIndex, item);
            //    listBox.SelectedIndex = targetIndex;
            //}
        }
    }
}
