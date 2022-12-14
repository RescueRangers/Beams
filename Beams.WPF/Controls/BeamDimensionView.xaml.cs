using System.Windows.Controls;
using System.Windows.Input;

namespace Beams.WPF.Controls
{
    /// <summary>
    /// Interaction logic for BeamDimensionView.xaml
    /// </summary>
    public partial class BeamDimensionView : UserControl
    {
        public BeamDimensionView()
        {
            InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && sender is TextBox listBox)
            {
                e.Handled = true;
                listBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
            }
        }
    }
}
