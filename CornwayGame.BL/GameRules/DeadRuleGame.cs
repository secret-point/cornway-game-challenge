using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornwayGame.BL.GameRules
{
    public class DeadRuleGame : BaseRuleGame
    {
        public override bool CanToggleCell(int indexX, int indexY, bool[][] board)
        {
            var currentDeadNeighbor = CalculateNeighbor(indexX, indexY, board, false);

            return currentDeadNeighbor == 3;
        }
    }
}
