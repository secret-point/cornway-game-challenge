using CornwayGame.BL.Interfaces;

namespace CornwayGame.BL.GameRules
{
    public class GameRules : IGameRules
    {
        //Neighbors
        //123
        //4*5
        //678

        private readonly int[][] neighborCoordinates = new int[8][]{
            new int[2] { -1, -1 },//position1
            new int[2] { 0, -1 },//position2
            new int[2] { 1, -1 },//position3
        
            new int[2] { -1, 0 },//position4
            new int[2] { 1, 0 },//position5

            new int[2] { -1, 1 },//position6
            new int[2] { 0, 1 },//position7
            new int[2] { 1, 1 },//position8
        };

        public bool ShouldToggleCell(int i, int h, bool[][] board)
        {
            (int countLiveNeighbor, int countDeadNeighbor) = CalculateNeighbor(i, h, board);

            var underpopulationRule = countLiveNeighbor < 2;
            var nearNeighborRule = countLiveNeighbor != 2 && countLiveNeighbor != 3;
            var overpopulationRule = countLiveNeighbor > 3;

            return underpopulationRule || nearNeighborRule || overpopulationRule;
        }

        public (int, int) CalculateNeighbor(int i, int h, bool[][] board)
        {
            var countLiveNeighbor = 0;
            var countDeadNeighbor = 0;

            for (int coordinateIndex = 0; coordinateIndex < neighborCoordinates.Length; coordinateIndex++)
            {
                int newI = i + neighborCoordinates[coordinateIndex][0];
                int newH = h + neighborCoordinates[coordinateIndex][1];
                if (newH < 0 || newI < 0 || newI >= board.Length || newH >= board[i].Length) continue;

                if (board[newI][newH])
                {
                    countLiveNeighbor++;

                }
                else
                {
                    countDeadNeighbor++;
                }
            }

            return (countLiveNeighbor, countDeadNeighbor);
        }
    }
}
