using Beams.Core.Models;
using DraftSight;

namespace Beams.Core.Interfaces
{
    public interface ISideBeam
    {
        void Draw(Application dsApp, Beam dimensions);
    }
}
