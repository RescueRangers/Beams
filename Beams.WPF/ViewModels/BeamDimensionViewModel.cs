using CommunityToolkit.Mvvm.ComponentModel;

namespace Beams.WPF.ViewModels
{
    public class BeamDimensionViewModel : ObservableObject
    {
        private double length;

        public double Length
        {
            get => length;
            set
            {
                length = value;
                OnPropertyChanged(nameof(Length));
            }
        }

        public int Number { get; set; }
    }
}
