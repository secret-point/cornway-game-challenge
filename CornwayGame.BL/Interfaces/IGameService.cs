using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornwayGame.BL.Interfaces
{
    public interface IGameService
    {
        string CreateBoard(int height, int width);
        void NextGeneration(string boardId);
        void UpdateLiveCells(string boardId, int[][] liveCellsCoordinates);
    }
}
