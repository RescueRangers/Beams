using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
