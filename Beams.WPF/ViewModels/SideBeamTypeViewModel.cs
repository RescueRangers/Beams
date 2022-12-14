using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beams.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Beams.WPF.ViewModels
{
    public class SideBeamTypeViewModel : ObservableObject
    {

        public SideBeamTypeViewModel(SideBeamType type)
        {
            SideBeamType = type;
        }

        private bool isChecked;
        private SideBeamType sideBeamType;

        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }
        public SideBeamType SideBeamType
        {
            get => sideBeamType;
            set
            {
                sideBeamType = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayImagePath));
            }
        }

        public string DisplayImagePath => $@".\..\Images\SB_type{(int)SideBeamType}.png";
    }
}
