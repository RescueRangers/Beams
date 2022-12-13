using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beams.Core.Models
{
    public class BeamDimensions
    {
        public double Width { get; set; }
        public double AddedLength { get; set; }
        public double MaterialWidth { get; set; }
        public List<double> Lengths { get; set; }
    }
}
