using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornwayGame.BL.GameRules
{
    public class LiveRuleGame : BaseRuleGame
    {
        public override bool CanToggleCell(int currentLiveNeighbor)
        {
            var underpopulationRule = currentLiveNeighbor < 2;
            var nearNeighborRule = currentLiveNeighbor != 2 && currentLiveNeighbor != 3;
            var overpopulationRule = currentLiveNeighbor > 3;

            return underpopulationRule || nearNeighborRule || overpopulationRule;
        }
    }
}
