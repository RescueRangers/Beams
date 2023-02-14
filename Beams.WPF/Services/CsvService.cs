using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beams.WPF.ViewModels;
using Microsoft.Extensions.Logging;

namespace Beams.WPF.Services
{
    public class CsvService : ICsvService
    {
        private ILogger<CsvService> logger;

        public CsvService(ILogger<CsvService> logger)
        {
            this.logger = logger;
        }

        public List<BeamDimensionViewModel> GetDimensionsFromCsv(FileInfo file)
        {
            var dimensions = new List<BeamDimensionViewModel>();
            using (var reader = new StreamReader(file.FullName,
                new FileStreamOptions 
                { 
                    Access = FileAccess.Read, 
                    Mode = FileMode.Open, 
                    Options = FileOptions.Asynchronous, 
                    Share = FileShare.ReadWrite 
                }))
            {
                while (!reader.EndOfStream)
                {
                    try
                    {
                        var line = reader.ReadLine();
                        var data = line.Split(';');
                        if (!int.TryParse(data[0], out var number))
                        {
                            logger.LogError("Number was in the wrong format", data[0]);
                        }
                        if (!double.TryParse(data[1], out var length))
                        {
                            logger.LogError("Length was in the wrong format", data[0]);
                        }

                        var dimension = new BeamDimensionViewModel
                        {
                            Number = number,
                            Length = length
                        };
                        dimensions.Add(dimension);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex.Message);
                        throw;
                    }
                }
            }
            return dimensions;
        }
    }
}
