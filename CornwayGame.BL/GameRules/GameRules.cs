using CornwayGame.BL.Interfaces;

namespace CornwayGame.BL.GameRules
{
    public class GameRules : IGameRules
    {

        private BaseRuleGame deadRuleGame;
        private BaseRuleGame liveRuleGame;

        public GameRules()
        {
            liveRuleGame = new LiveRuleGame();
            deadRuleGame = new DeadRuleGame();
        }

        public bool ShouldToggleCell(int i, int h, bool[][] board)
        {

            var currentCell = board[i][h];
            var currentRuleGame = currentCell ? liveRuleGame : deadRuleGame;


            return currentRuleGame.CanToggleCell(i, h, board);
        }


    }
}
