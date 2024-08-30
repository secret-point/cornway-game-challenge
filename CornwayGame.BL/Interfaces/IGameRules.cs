using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornwayGame.BL.Interfaces
{
    public interface IGameRules
    {
        bool ShouldToggleCell(int i, int h, bool[][] board);
    }
}
