using System.Collections.Generic;
using System.IO;
using Beams.WPF.ViewModels;

namespace Beams.WPF.Services
{
    public interface ICsvService
    {
        List<BeamDimensionViewModel> GetDimensionsFromCsv(FileInfo file);
    }
}