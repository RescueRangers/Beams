using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beams.Core.Models;
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

    }
}
