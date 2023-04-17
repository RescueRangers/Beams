using Beams.Core.Interfaces;
using Beams.Core.Models;
using Beams.Core.Util;
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
				dsDoc = dsApp.NewDocument("standard.dwt");
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

            var coordinates = beam.GetBeamCoordinates();

            if (coordinates == null)
            {
                return;
            }

            DrawBeam(dsSketchManager, coordinates);
            
            DrawBorder(dsSketchManager, beam.TotalLength, beam.Width);

            if (!string.IsNullOrWhiteSpace(beam.SavePath))
            {
				dsDoc.SaveAs($"{beam.SavePath}+{beam.AddedLength}mm", dsDocumentSaveAsOption_e.dsDocumentSaveAs_R12_ASCII_DXF, out var saveErrors);

				if (saveErrors == dsDocumentSaveError_e.dsDocumentSave_Succeeded)
				{
                    dsApp.CloseDocument(dsDoc.GetPathName(), false);
				}
				else
                {
					throw new FileSaveException($"Could not save the file: {saveErrors}");
				}
			}
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
