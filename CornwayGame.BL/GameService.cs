using CornwayGame.BL.Interfaces;
using CornwayGame.Data.Interfaces;

namespace CornwayGame.BL
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGameRules _gameRules;
        public GameService(IGameRepository gameRepository, IGameRules gameRules)
        {
            _gameRepository = gameRepository;
            _gameRules = gameRules;
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

        public void NextGeneration(string boardId)
        {
            var board = _gameRepository.GetById(boardId);
            var boardCloned = BoardDeepClone(board);

            for (int i = 0; i < board.Length; i++)
            {
                var column = board[i];
                for (int h = 0; h < column.Length; h++)
                {
                    var toggleCell = _gameRules.ShouldToggleCell(i, h, board);
                    if (toggleCell)
                        boardCloned[i][h] = !board[i][h];
                }
            }
            _gameRepository.Update(boardId, boardCloned);
        }

        public void UpdateLiveCells(string boardId, int[][] liveCellsCoordinates)
        {
            var board = _gameRepository.GetById(boardId); 
            if (board == null) throw new ArgumentException("Board Does not exists.");

            foreach (var cell in liveCellsCoordinates)
            {
                if (cell == null) continue;
                board[cell[0]][cell[1]] = true;
            }
            _gameRepository.Update(boardId, board);
        }

        private bool[][] BoardDeepClone(bool[][] board)
        {
            var clone = new bool[board.Length][];
            for (int i = 0; i < board.Length; i++)
            {
                var row = new bool[board[i].Length];
                for (int h = 0; h < row.Length; h++)
                {
                    row[h] = board[i][h];
                }
                clone[i] = row;
            }
            return clone;
        }

    }
}
