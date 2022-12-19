using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GongSolutions.Wpf.DragDrop;

namespace Beams.WPF.ViewModels
{
    public class BeamDimensionsViewModel : ObservableObject, IDropTarget
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
            var currentBeamCount = 0;
            if (dimensions.Any()) currentBeamCount = dimensions.Last().Number;

            for (int i = 0; i < numOfBeams; i++)
            {
                dimensions.Add(new BeamDimensionViewModel { Number = currentBeamCount + i+1});
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

        public void DragOver(IDropInfo dropInfo)
        {
            var sourceItem = dropInfo.Data as BeamDimensionViewModel;
            var targetItem = dropInfo.TargetItem as BeamDimensionViewModel;

            if (sourceItem != null && targetItem != null)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            var sourceItem = dropInfo.Data as BeamDimensionViewModel;
            var targetItem = dropInfo.TargetItem as BeamDimensionViewModel;

            var dims = new List<BeamDimensionViewModel>(Dimensions);

            var sourceIndex = dims.IndexOf(sourceItem);
            var targetIndex = dims.IndexOf(targetItem);
            dims.RemoveAt(sourceIndex);
            dims.Insert(targetIndex, sourceItem);

            for (int i = 0; i < Dimensions.Count; i++)
            {
                dims[i].Number = i + 1;
            }
            Dimensions = new ObservableCollection<BeamDimensionViewModel>(dims);
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
