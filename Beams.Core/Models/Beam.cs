namespace Beams.Core.Models
{
    public class Beam
    {
        public double Width { get; set; }
        public double AddedLength { get; set; }
        public double MaterialWidth { get; set; }
        public List<double> Lengths { get; set; }
        public SideBeamType BeamType { get; set; }

        public List<BeamCoordinate>? GetBeamCoordinates()
        {
            return BeamType switch
            {
                SideBeamType.Type1 => GenerateBeam1Dimensions(),
                SideBeamType.Type2 => GenerateBeam2Dimensions(),
                _ => throw new ArgumentNullException(nameof(BeamType), "Beam type must be specified"),
            };
        }

        private List<BeamCoordinate> GenerateBeam2Dimensions()
        {
            var offset = Width - MaterialWidth + AddedLength;
            var coordinate1 = new BeamCoordinate
            {
                X1 = 0,
                Y1 = 0,
                X2 = 0,
                Y2 = Width
            };
            var coordinates = new List<BeamCoordinate>
            {
                coordinate1,
            };

            for (int i = 0; i < Lengths.Count; i++)
            {
                if (i%2 == 0)
                {
                    coordinates.Add(new BeamCoordinate
                    {
                        X1 = Lengths[i] + coordinates[i].X1 + offset - Width,
                        Y1 = 0,
                        X2 = Lengths[i] + coordinates[i].X2 + offset,
                        Y2 = Width
                    });
                }
                else
                {
                    coordinates.Add(new BeamCoordinate
                    {
                        X1 = Lengths[i] + coordinates[i].X1 + offset,
                        Y1 = 0,
                        X2 = Lengths[i] + coordinates[i].X2 + offset - Width,
                        Y2 = Width
                    });
                }
                
            }
            return coordinates;
        }

        private List<BeamCoordinate> GenerateBeam1Dimensions()
        {
            var offset = Width - MaterialWidth + AddedLength;
            var coordinate1 = new BeamCoordinate
            {
                X1 = 0,
                Y1 = 0,
                X2 = Width,
                Y2 = Width
            };
            var coordinates = new List<BeamCoordinate>
            {
                coordinate1,
            };

            for (int i = 0; i < Lengths.Count; i++)
            {
                coordinates.Add(new BeamCoordinate
                {
                    X1 = Lengths[i] + coordinates[i].X1 - Width + offset,
                    Y1 = 0,
                    X2 = Lengths[i] + coordinates[i].X2 - Width + offset,
                    Y2 = Width
                });
            }
            return coordinates;
        }
    }
}
