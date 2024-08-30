using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornwayGame.BL.GameRules
{
    public abstract class BaseRuleGame
    {
        public abstract bool CanToggleCell(int currentCountNeighbor);
    }
}
