using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornwayGame.BL.GameRules
{
    public abstract class BaseRuleGame
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
        public abstract bool CanToggleCell(int indexX, int indexY, bool[][] board);

        protected int CalculateNeighbor(int indexX, int indexY, bool[][] board, bool isLive)
        {
            var currentNeighborCount = 0;

            for (int coordinateIndex = 0; coordinateIndex < neighborCoordinates.Length; coordinateIndex++)
            {
                int newI = indexX + neighborCoordinates[coordinateIndex][0];
                int newH = indexY + neighborCoordinates[coordinateIndex][1];
                if (newH < 0 || newI < 0 || newI >= board.Length || newH >= board[indexX].Length) continue;

                if (board[newI][newH] == isLive)
                {
                    currentNeighborCount++;

                }
            }

            return currentNeighborCount;
        }
    }
}
