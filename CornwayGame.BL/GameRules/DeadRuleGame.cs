using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornwayGame.BL.GameRules
{
    public class DeadRuleGame : BaseRuleGame
    {
        public override bool CanToggleCell(int currentDeadNeighbor)
        {
            return currentDeadNeighbor == 3;
        }
    }
}
