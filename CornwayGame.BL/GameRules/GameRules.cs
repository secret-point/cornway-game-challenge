using CornwayGame.BL.Interfaces;

namespace CornwayGame.BL.GameRules
{
    public class GameRules:IGameRules
    {
        public bool ShouldToggleCell(int i, int h, bool[][] board)
        {
            return false;
        }
    }
}
