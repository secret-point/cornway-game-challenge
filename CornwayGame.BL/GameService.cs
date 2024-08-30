using CornwayGame.BL.Interfaces;
using CornwayGame.Data.Interfaces;

namespace CornwayGame.BL
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public string CreateBoard(int height, int width)
        {
            bool[][] boardGame = new bool[width][];
            for (int i = 0; i < width; i++)
            {
                var row = new bool[height];

                for (int h = 0; h < height; h++)
                {
                    row[h] = false;
                }

                boardGame[i] = row;
            }
            var boardId = _gameRepository.Add(boardGame);

            return boardId;
        }

        public void UpdateLiveCells(string boardId, int[][] liveCellsCoordinates)
        {
            var board = _gameRepository.GetById(boardId);
            foreach (var cell in liveCellsCoordinates)
            {
                if (cell == null) continue;
                board[cell[0]][cell[1]] = true;
            }
            _gameRepository.Update(boardId, board);
        }
    }
}
