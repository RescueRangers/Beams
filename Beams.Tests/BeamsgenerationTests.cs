using Beams.Core.Models;

namespace Beams.Tests
{
    public class BeamsgenerationTests
    {
        private List<BeamCoordinate> knownGoodBeam1Coordinates;
        private List<BeamCoordinate> knownGoodBeam2Coordinates;
        private List<BeamCoordinate> knownGoodBeam3Coordinates;
        private List<double> lengths = new List<double> { 1000, 2000, 3000, 4000, 5000 };
        private Beam type1Beam;
        private Beam type2Beam;
        private Beam type3Beam;

        [SetUp]
        public void Setup()
        {
            GenerateGoodBeam1Coordinates();
            GenerateGoodBeam2Coordinates();
            GenerateGoodBeam3Coordinates();

            type1Beam = new Beam
            {
                AddedLength = 15,
                MaterialWidth = 405,
                Width = 410,
                Lengths = lengths,
                BeamType = SideBeamType.Type1
            };
            type2Beam = new Beam
            {
                AddedLength = 15,
                MaterialWidth = 405,
                Width = 410,
                Lengths = lengths,
                BeamType = SideBeamType.Type2
            };
            type3Beam = new Beam
            {
                AddedLength = 15,
                MaterialWidth = 405,
                Width = 410,
                Lengths = lengths,
                BeamType = SideBeamType.Type3
            };
        }

        [Test]
        public void TestBeam1CoordinateGeneration()
        {
            var coordinates = type1Beam.GetBeamCoordinates();

            for (int i = 0; i < coordinates.Count; i++)
            {
                BeamCoordinate? coord = coordinates[i];
                Assert.That(coord, Is.Not.Null);
                Assert.Multiple(() =>
                {
                    Assert.That(coord.X1, Is.EqualTo(knownGoodBeam1Coordinates[i].X1));
                    Assert.That(coord.Y1, Is.EqualTo(knownGoodBeam1Coordinates[i].Y1));
                    Assert.That(coord.X2, Is.EqualTo(knownGoodBeam1Coordinates[i].X2));
                    Assert.That(coord.Y2, Is.EqualTo(knownGoodBeam1Coordinates[i].Y2));
                });
            }
        }

        [Test]
        public void TestBeam2CoordinateGeneration()
        {
            var coordinates = type2Beam.GetBeamCoordinates();

            for (int i = 0; i < coordinates.Count; i++)
            {
                BeamCoordinate? coord = coordinates[i];
                Assert.That(coord, Is.Not.Null);
                Assert.Multiple(() =>
                {
                    Assert.That(coord.X1, Is.EqualTo(knownGoodBeam2Coordinates[i].X1));
                    Assert.That(coord.Y1, Is.EqualTo(knownGoodBeam2Coordinates[i].Y1));
                    Assert.That(coord.X2, Is.EqualTo(knownGoodBeam2Coordinates[i].X2));
                    Assert.That(coord.Y2, Is.EqualTo(knownGoodBeam2Coordinates[i].Y2));
                });
            }
        }

        [Test]
        public void TestBeam3CoordinateGeneration()
        {
            var coordinates = type3Beam.GetBeamCoordinates();

            for (int i = 0; i < coordinates.Count; i++)
            {
                BeamCoordinate? coord = coordinates[i];
                Assert.That(coord, Is.Not.Null);
                Assert.Multiple(() =>
                {
                    Assert.That(coord.X1, Is.EqualTo(knownGoodBeam3Coordinates[i].X1));
                    Assert.That(coord.Y1, Is.EqualTo(knownGoodBeam3Coordinates[i].Y1));
                    Assert.That(coord.X2, Is.EqualTo(knownGoodBeam3Coordinates[i].X2));
                    Assert.That(coord.Y2, Is.EqualTo(knownGoodBeam3Coordinates[i].Y2));
                });
                Assert.That(type3Beam.TotalLength, Is.EqualTo(14280));
            }
        }

        private void GenerateGoodBeam1Coordinates()
        {
            var coordinate1 = new BeamCoordinate
            {
                X1 = 0,
                X2 = 410,
                Y1 = 0,
                Y2 = 410
            };
            var coordinate2 = new BeamCoordinate
            {
                X1 = 610,
                X2 = 1020,
                Y1 = 0,
                Y2 = 410
            };
            var coordinate3 = new BeamCoordinate
            {
                X1 = 2220,
                X2 = 2630,
                Y1 = 0,
                Y2 = 410
            };
            var coordinate4 = new BeamCoordinate
            {
                X1 = 4830,
                X2 = 5240,
                Y1 = 0,
                Y2 = 410
            };
            var coordinate5 = new BeamCoordinate
            {
                X1 = 8440,
                X2 = 8850,
                Y1 = 0,
                Y2 = 410
            };
            var coordinate6 = new BeamCoordinate
            {
                X1 = 13050,
                X2 = 13460,
                Y1 = 0,
                Y2 = 410
            };
            knownGoodBeam1Coordinates = new List<BeamCoordinate>
            {
                coordinate1,
                coordinate2,
                coordinate3,
                coordinate4,
                coordinate5,
                coordinate6
            };
        }

        private void GenerateGoodBeam2Coordinates()
        {
            var coordinate1 = new BeamCoordinate
            {
                X1 = 0,
                X2 = 0,
                Y1 = 0,
                Y2 = 410
            };
            var coordinate2 = new BeamCoordinate
            {
                X1 = 610,
                X2 = 1020,
                Y1 = 0,
                Y2 = 410
            };
            var coordinate3 = new BeamCoordinate
            {
                X1 = 2630,
                X2 = 2630,
                Y1 = 0,
                Y2 = 410
            };
            var coordinate4 = new BeamCoordinate
            {
                X1 = 5240,
                X2 = 5650,
                Y1 = 0,
                Y2 = 410
            };
            var coordinate5 = new BeamCoordinate
            {
                X1 = 9260,
                X2 = 9260,
                Y1 = 0,
                Y2 = 410
            };
            var coordinate6 = new BeamCoordinate
            {
                X1 = 13870,
                X2 = 14280,
                Y1 = 0,
                Y2 = 410
            };
            knownGoodBeam2Coordinates = new List<BeamCoordinate>
            {
                coordinate1,
                coordinate2,
                coordinate3,
                coordinate4,
                coordinate5,
                coordinate6
            };
        }

        private void GenerateGoodBeam3Coordinates()
        {
            var coordinate1 = new BeamCoordinate
            {
                X1 = 0,
                X2 = 0,
                Y1 = 0,
                Y2 = 410
            };
            var coordinate2 = new BeamCoordinate
            {
                X1 = 1020,
                X2 = 610,
                Y1 = 0,
                Y2 = 410
            };
            var coordinate3 = new BeamCoordinate
            {
                X1 = 2630,
                X2 = 2630,
                Y1 = 0,
                Y2 = 410
            };
            var coordinate4 = new BeamCoordinate
            {
                X1 = 5650,
                X2 = 5240,
                Y1 = 0,
                Y2 = 410
            };
            var coordinate5 = new BeamCoordinate
            {
                X1 = 9260,
                X2 = 9260,
                Y1 = 0,
                Y2 = 410
            };
            var coordinate6 = new BeamCoordinate
            {
                X1 = 14280,
                X2 = 13870,
                Y1 = 0,
                Y2 = 410
            };
            knownGoodBeam3Coordinates = new List<BeamCoordinate>
            {
                coordinate1,
                coordinate2,
                coordinate3,
                coordinate4,
                coordinate5,
                coordinate6
            };
        }
    }
}