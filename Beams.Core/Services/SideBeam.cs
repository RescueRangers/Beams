using Beams.Core.Interfaces;
using Beams.Core.Models;
using DraftSight;

namespace Beams.Core.Services
{
    public class SideBeam : ISideBeam
    {
        public void Draw(Application dsApp, BeamDimensions dimensions, SideBeamType beamType)
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

            switch (beamType)
            {
                case SideBeamType.Type1:
                    rollingLength = DrawType1Geometry(dsSketchManager, dimensions);
                    break;
                case SideBeamType.Type2:
                    rollingLength = DrawType2Geometry(dsSketchManager, dimensions);
                    break;
                default:
                    break;
            }
            DrawBorder(dsSketchManager, rollingLength, dimensions.Width);
        }

        private double DrawType2Geometry(SketchManager dsSketchManager, BeamDimensions dimensions)
        {
            var offset = (dimensions.Width - dimensions.MaterialWidth) + dimensions.AddedLength;
            var rollingStartX = 0d;
            var rollingEndX = rollingStartX;

            for (int i = 0; i < dimensions.Lengths.Count; i += 2)
            {
                dsSketchManager.InsertLine(rollingStartX, 0.00000000000000, 0.00000000000000, rollingEndX, dimensions.Width, 0.00000000000000);
                rollingStartX += dimensions.Lengths[i] + offset - dimensions.Width;
                rollingEndX += dimensions.Lengths[i] + offset;
                dsSketchManager.InsertLine(rollingStartX, 0.00000000000000, 0.00000000000000, rollingEndX, dimensions.Width, 0.00000000000000);
                rollingStartX += dimensions.Lengths[i + 1] + offset;
                rollingEndX = rollingStartX;
            }
            dsSketchManager.InsertLine(rollingStartX, 0.00000000000000, 0.00000000000000, rollingEndX, dimensions.Width, 0.00000000000000);
            return rollingEndX;
        }

        private void DrawBorder(SketchManager dsSketchManager, double rollingLength, double height)
        {
            dsSketchManager.InsertLine(-1, -1, 0.00000000000000, rollingLength + 1, -1, 0.00000000000000);
            dsSketchManager.InsertLine(-1, -1, 0.00000000000000, -1, height + 1, 0.00000000000000);
            dsSketchManager.InsertLine(-1, height + 1, 0.00000000000000, rollingLength + 1, height + 1, 0.00000000000000);
            dsSketchManager.InsertLine(rollingLength + 1, height + 1, 0.00000000000000, rollingLength + 1, -1, 0.00000000000000);
        }

        private double DrawType1Geometry(SketchManager dsSketchManager, BeamDimensions beamDimensions)
        {
            var offset = (beamDimensions.Width - beamDimensions.MaterialWidth) + beamDimensions.AddedLength;
            var rollingStartX = 0d;
            var rollingEndX = beamDimensions.Width;

            dsSketchManager.InsertLine(rollingStartX, 0.00000000000000, 0.00000000000000, rollingEndX, beamDimensions.Width, 0.00000000000000);
            rollingStartX += beamDimensions.Lengths[0] + offset - beamDimensions.Width;
            rollingEndX = beamDimensions.Lengths[0] + offset;
            dsSketchManager.InsertLine(rollingStartX, 0.00000000000000, 0.00000000000000, rollingEndX, beamDimensions.Width, 0.00000000000000);

            for (int i = 1; i < beamDimensions.Lengths.Count; i++)
            {
                double length = beamDimensions.Lengths[i];
                rollingStartX += length + offset - beamDimensions.Width;
                rollingEndX += length + offset - beamDimensions.Width;
                dsSketchManager.InsertLine(rollingStartX, 0.00000000000000, 0.00000000000000, rollingEndX, beamDimensions.Width, 0.00000000000000);
            }

            return rollingEndX;
        }
    }
}
