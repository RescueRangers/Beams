using Beams.Core.Interfaces;
using Beams.Core.Models;
using DraftSight;

namespace Beams.Core.Services
{
    public class SideBeam : ISideBeam
    {
        public void Draw(Application dsApp, Beam beam)
        {
            Document dsDoc = dsApp.GetActiveDocument();
            if (null == dsDoc)
            {
                return;
            }

            Model dsModel = dsDoc.GetModel();
            if (null == dsModel)
            {
                return;
            }

            SketchManager dsSketchManager;
            dsSketchManager = dsModel.GetSketchManager();
            if (null == dsSketchManager)
            {
                return;
            }

            object[] sheetsList = (object[])dsDoc.GetSheets();
            Sheet dsSheet = (Sheet)sheetsList[1];
            if (null == dsSheet)
            {
                return;
            }

            ViewManager dsViewManager = dsDoc.GetViewManager();
            if (null == dsViewManager)
            {
                return;
            }
            dsSetCommandOptionResult_e result;

            double rollingLength = 0;

            var coordinates = beam.GetBeamCoordinates();

            if (coordinates == null)
            {
                return;
            }

            DrawBeam(dsSketchManager, coordinates);
            
            DrawBorder(dsSketchManager, coordinates.Last().X2, beam.Width);
        }

        private void DrawBeam(SketchManager dsSketchManager, List<BeamCoordinate> beamCoordinates)
        {
            foreach (var coordinate in beamCoordinates)
            {
                dsSketchManager.InsertLine(coordinate.X1, coordinate.Y1, 0, coordinate.X2, coordinate.Y2, 0);
            }
        }

        private void DrawBorder(SketchManager dsSketchManager, double rollingLength, double height)
        {
            dsSketchManager.InsertLine(-1, -1, 0.00000000000000, rollingLength + 1, -1, 0.00000000000000);
            dsSketchManager.InsertLine(-1, -1, 0.00000000000000, -1, height + 1, 0.00000000000000);
            dsSketchManager.InsertLine(-1, height + 1, 0.00000000000000, rollingLength + 1, height + 1, 0.00000000000000);
            dsSketchManager.InsertLine(rollingLength + 1, height + 1, 0.00000000000000, rollingLength + 1, -1, 0.00000000000000);
        }
    }
}
