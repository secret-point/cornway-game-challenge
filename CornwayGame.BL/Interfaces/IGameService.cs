using CornwayGame.BL.Model;
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
        FinalStateResponse GetFinalState(string boardId);
        Game GetGeneration(string boardId);
        (bool[][],bool) NextGeneration(string boardId);
        void Restart(string boardId);
        void UpdateLiveCells(string boardId, int[][] liveCellsCoordinates);
    }
}
