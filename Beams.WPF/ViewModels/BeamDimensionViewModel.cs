using System;
using Beams.WPF.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Beams.WPF.ViewModels
{
    public class BeamDimensionViewModel : ObservableObject
    {
        private double length;
		public IRelayCommand DeleteDimensionCommand { get; private set; }
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

        public BeamDimensionViewModel()
        {
            DeleteDimensionCommand = new RelayCommand(DeleteDimension);
        }

		private void DeleteDimension()
		{
            WeakReferenceMessenger.Default.Send(new DeleteDimensionMessage(this));
		}

		public override int GetHashCode()
		{
            var hash = 17;
            unchecked
            {
                hash *= 23 + Number.GetHashCode();
                hash *= 23 + Length.GetHashCode();
			}
            return hash;
		}
	}
}
