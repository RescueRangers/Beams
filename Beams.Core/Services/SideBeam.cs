using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beams.Core.Interfaces;
using Beams.Core.Models;
using DraftSight;

namespace Beams.Core.Services
{
    public class SideBeam : ISideBeam
    {
        public void Draw(Application dsApp, BeamDimensions dimensions)
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

            double rollingLength = DrawInsideGeometry(dsSketchManager, dimensions);
            DrawBorder(dsSketchManager, rollingLength, dimensions.Width);
        }

        private void DrawBorder(SketchManager dsSketchManager, double rollingLength, double height)
        {
            dsSketchManager.InsertLine(-1, -1, 0.00000000000000, rollingLength + 1, -1, 0.00000000000000);
            dsSketchManager.InsertLine(-1, -1, 0.00000000000000, -1, height + 1, 0.00000000000000);
            dsSketchManager.InsertLine(-1, height + 2, 0.00000000000000, rollingLength + 1, height + 2, 0.00000000000000);
            dsSketchManager.InsertLine(rollingLength + 1, height + 2, 0.00000000000000, rollingLength + 1, -1, 0.00000000000000);
        }

        private double DrawInsideGeometry(SketchManager dsSketchManager, BeamDimensions beamDimensions)
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
            
            //foreach (var length in beamDimensions.Lengths)
            //{
            //    rollingLength += length + beamDimensions.AddedLength;
            //    dsSketchManager.InsertLine(rollingLength - beamDimensions.Width - offset, 0, 0.00000000000000, rollingLength  - offset, beamDimensions.Width, 0.00000000000000);
            //}

            return rollingEndX;
        }
    }
}
