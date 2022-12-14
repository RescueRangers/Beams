using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using Beams.Core.Interfaces;
using Beams.Core.Models;
using Beams.WPF.Hacks;
using Beams.WPF.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace Beams.WPF.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        public MainWindowViewModel(ISideBeam sideBeam, ILogger<MainWindowViewModel> logger, IMessagingService messagingService)
        {
            this.sideBeam = sideBeam;

            DrawSideBeamCommand = new RelayCommand(StartDraw, () => CanDraw());
            this.logger = logger;
            this.messagingService = messagingService;
            dimensions = new BeamDimensionsViewModel();
            SideBeamTypes = new BindingList<SideBeamTypeViewModel>
            {
                new SideBeamTypeViewModel(SideBeamType.Type1),
                new SideBeamTypeViewModel(SideBeamType.Type2)
            };
            SideBeamTypes.ListChanged += SideBeamTypes_ListChanged;
            Dimensions.BeamsChanged += Dimensions_BeamsChanged;
        }

        private void Dimensions_BeamsChanged(object sender, EventArgs e)
        {
            DrawSideBeamCommand.NotifyCanExecuteChanged();
        }

        private void SideBeamTypes_ListChanged(object? sender, ListChangedEventArgs e)
        {
            DrawSideBeamCommand.NotifyCanExecuteChanged();
        }

        private bool CanDraw()
        {
            return SideBeamTypes.Any(s => s.IsChecked) && Dimensions.Dimensions.Any();
        }

        private void StartDraw()
        {
            var dims = new BeamDimensions
            {
                Width = dimensions.Height,
                MaterialWidth = dimensions.MaterialWidth,
                AddedLength = dimensions.AddedLength
            };

            dims.Lengths = dimensions.Dimensions.Select(s => s.Length).ToList();

            var ds = TryGetDraftSightInstance();

            if (ds == null)
            {
                messagingService.DisplayMessage("Could not find DraftSight", MessageType.Info);
                return;
            }
            else
            {
                var type = SideBeamTypes.FirstOrDefault(s => s.IsChecked).SideBeamType;
                sideBeam.Draw(ds, dims, type);
            }
        }

        private DraftSight.Application? TryGetDraftSightInstance()
        {
            object application;
            try
            {
                application = Marshal2.GetActiveObject("DraftSight.Application");
            }
            catch (Exception ex)
            {
                messagingService.DisplayMessage("DraftSight is not running", MessageType.Error);
                logger.LogError(ex.Message);
                return null;
            }

            if (null != application)
            {
                return application as DraftSight.Application;
            }

            logger.LogError("Could not get the application handle");
            return null;
        }

        private ISideBeam sideBeam;
        private ILogger<MainWindowViewModel> logger;
        private IMessagingService messagingService;
        private BeamDimensionsViewModel dimensions;
        private BindingList<SideBeamTypeViewModel> sideBeamTypes;

        public IRelayCommand DrawSideBeamCommand { get; private set; }

        public BeamDimensionsViewModel Dimensions
        {
            get => dimensions;
            set
            {
                dimensions = value;
                OnPropertyChanged();
            }
        }
        public BindingList<SideBeamTypeViewModel> SideBeamTypes
        {
            get => sideBeamTypes;
            set
            {
                sideBeamTypes = value;
                OnPropertyChanged(nameof(SideBeamTypes));
                
            }
        }
    }
}
