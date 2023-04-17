using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using Beams.Core.Interfaces;
using Beams.Core.Models;
using Beams.WPF.Hacks;
using Beams.WPF.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GongSolutions.Wpf.DragDrop;
using Microsoft.Extensions.Logging;

namespace Beams.WPF.ViewModels
{
	public class MainWindowViewModel : ObservableObject, IDropTarget
	{
		public MainWindowViewModel(ISideBeam sideBeam, ILogger<MainWindowViewModel> logger, IMessagingService messagingService, ICsvService csvService)
		{
			this.sideBeam = sideBeam;

			DrawSideBeamCommand = new RelayCommand(StartDraw, () => CanDraw());
			this.logger = logger;
			this.messagingService = messagingService;
			this.csvService = csvService;
			dimensions = new BeamDimensionsViewModel();
			SideBeamTypes = new BindingList<SideBeamTypeViewModel>
			{
				new SideBeamTypeViewModel(SideBeamType.Type1),
				new SideBeamTypeViewModel(SideBeamType.Type2),
				new SideBeamTypeViewModel(SideBeamType.Type3)
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
			var beam = new Beam
			{
				Width = dimensions.Height,
				MaterialWidth = dimensions.MaterialWidth,
				AddedLength = dimensions.AddedLength,
				BeamType = SideBeamTypes.FirstOrDefault(s => s.IsChecked).SideBeamType,
				SavePath = SaveFilePath
			};

			beam.Lengths = dimensions.Dimensions.Select(s => s.Length).ToList();

			var ds = TryGetDraftSightInstance();

			if (ds == null)
			{
				messagingService.DisplayMessage("Could not find DraftSight", MessageType.Info);
				return;
			}
			else
			{
				var type = SideBeamTypes.FirstOrDefault(s => s.IsChecked).SideBeamType;
				try
				{
					sideBeam.Draw(ds, beam);
					SaveFilePath = null;
				}
				catch (Exception ex)
				{
					messagingService.DisplayMessage(ex.Message, MessageType.Error);
				}
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

		public void DragOver(IDropInfo dropInfo)
		{
			if (dropInfo.Data is IDataObject)
			{
				DataObjectDragOver(dropInfo);
			}
		}

		private void DataObjectDragOver(IDropInfo dropInfo)
		{
			var dataObject = dropInfo.Data as IDataObject;
			if (CheckIfFileDroppedIsSingleCsv(dataObject))
			{
				dropInfo.Effects = DragDropEffects.Copy;
			}
			else
			{
				dropInfo.Effects = DragDropEffects.None;
			}
		}

		private bool CheckIfFileDroppedIsSingleCsv(IDataObject? dataObject)
		{
			if (dataObject != null && dataObject.GetDataPresent(DataFormats.FileDrop))
			{
				var data = dataObject.GetData(DataFormats.FileDrop);
				var dataString = data as string[];
				if (dataString == null) return false;
				if (string.Compare(dataString[0].Substring(dataString[0].Length - 3, 3), "csv", true) == 0)
				{
					return true;
				}
			}
			return false;
		}


		public void Drop(IDropInfo dropInfo)
		{
			if (dropInfo.Data is IDataObject)
			{
				DropDataObject(dropInfo);
			}
		}

		private void DropDataObject(IDropInfo dropInfo)
		{
			var dataObject = dropInfo.Data as IDataObject;
			var data = dataObject.GetData(DataFormats.FileDrop);
			var dataString = data as string[];

			var file = new FileInfo(dataString[0]);
			if (!file.Exists) return;

			SaveFilePath = file.FullName.Remove(file.FullName.Length - 4);
			List<BeamDimensionViewModel> csvDimensions = csvService.GetDimensionsFromCsv(file);
			dimensions.ClearBeams();
			dimensions.AddBeams(csvDimensions);
			DrawSideBeamCommand.NotifyCanExecuteChanged();
		}

		private ISideBeam sideBeam;
		private ILogger<MainWindowViewModel> logger;
		private IMessagingService messagingService;
		private ICsvService csvService;
		private BeamDimensionsViewModel dimensions;
		private BindingList<SideBeamTypeViewModel> sideBeamTypes;
		private string? saveFilePath;

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

		public string? SaveFilePath
		{
			get => saveFilePath; 
			set
			{
				saveFilePath = value;
				OnPropertyChanged(nameof(SaveFilePath));
			}
		}
	}
}
