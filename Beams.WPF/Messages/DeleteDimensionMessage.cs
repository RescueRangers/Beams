using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beams.WPF.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Beams.WPF.Messages
{
	class DeleteDimensionMessage : ValueChangedMessage<BeamDimensionViewModel>
	{
		public DeleteDimensionMessage(BeamDimensionViewModel value) : base(value)
		{
		}
	}
}
