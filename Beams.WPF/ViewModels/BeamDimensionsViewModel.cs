using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Beams.WPF.ViewModels
{
    public class BeamDimensionsViewModel : ObservableObject
    {
        private double height = 410;
        private double addedLength = 15;
        private ObservableCollection<BeamDimensionViewModel> dimensions = new();
        private int numOfBeams;
        private double materialWidth = 405;



        public BeamDimensionsViewModel()
        {
            AddBeamsCommand = new RelayCommand(() => AddBeams());
            ClearBeamsCommand = new RelayCommand(() => ClearBeams());
        }

        private void AddBeams()
        {
            for (int i = 0; i < numOfBeams; i++)
            {
                dimensions.Add(new BeamDimensionViewModel());
            }
            if (BeamsChanged != null)
            {
                BeamsChanged(this, new EventArgs());
            }
        }

        private void ClearBeams()
        {
            Dimensions.Clear();
            if (BeamsChanged != null)
            {
                BeamsChanged(this, new EventArgs());
            }
        }

        public double Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        public double AddedLength
        {
            get => addedLength;
            set
            {
                addedLength = value;
                OnPropertyChanged(nameof(AddedLength));
            }
        }
        public ObservableCollection<BeamDimensionViewModel> Dimensions
        {
            get => dimensions;
            set
            {
                dimensions = value;
                OnPropertyChanged(nameof(Dimensions));
            }
        }
        public int NumOfBeams
        {
            get => numOfBeams;
            set
            {
                numOfBeams = value;
                OnPropertyChanged(nameof(NumOfBeams));
            }
        }
        public double MaterialWidth
        {
            get => materialWidth;
            set
            {
                materialWidth = value;
                OnPropertyChanged(nameof(MaterialWidth));
            }
        }
        public IRelayCommand AddBeamsCommand { get; private set; }
        public IRelayCommand ClearBeamsCommand { get; private set; }

        public delegate void BeamsChangedEventHandler(object sender, EventArgs e);
        public event BeamsChangedEventHandler BeamsChanged;
    }
}
