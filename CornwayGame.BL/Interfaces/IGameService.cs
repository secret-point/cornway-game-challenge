using CornwayGame.Data.Model;
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
        Game GetGeneration(string boardId);
        bool[][] NextGeneration(string boardId);
        void UpdateLiveCells(string boardId, int[][] liveCellsCoordinates);
    }
}
