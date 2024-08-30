using CornwayGame.BL.Interfaces;
using CornwayGame.BL.Model;
using CornwayGame.Data.Interfaces;
using CornwayGame.Data.Model;
using Microsoft.Extensions.Options;

namespace CornwayGame.BL
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGameRules _gameRules;
        private readonly GameSettings _gameSettings;

        public GameService(IGameRepository gameRepository, IGameRules gameRules, IOptions<GameSettings> options)
        {
            _gameRepository = gameRepository;
            _gameRules = gameRules;
            _gameSettings = options.Value;
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

        public (bool[][], bool) NextGeneration(string boardId)
        {
            var gameData = _gameRepository.GetById(boardId);
            var board = gameData.Board;
            var boardCloned = BoardDeepClone(board);
            var hasChanged = false;
            for (int i = 0; i < board.Length; i++)
            {
                var column = board[i];
                for (int h = 0; h < column.Length; h++)
                {
                    var toggleCell = _gameRules.ShouldToggleCell(i, h, board);
                    hasChanged = hasChanged || toggleCell;
                    if (toggleCell)
                        boardCloned[i][h] = !board[i][h];
                }
            }
            var game = new Game
            {
                Board = boardCloned,
                Generation = gameData.Generation + 1
            };
            _gameRepository.Update(boardId, game);
            return (boardCloned, hasChanged);
        }

        public void UpdateLiveCells(string boardId, int[][] liveCellsCoordinates)
        {
            var gameData = _gameRepository.GetById(boardId);
            if (gameData == null) throw new ArgumentException("Board Does not exists.");
            var board = gameData.Board;
            if (board == null) throw new ArgumentException("Board Does not exists.");

            foreach (var cell in liveCellsCoordinates)
            {
                if (cell == null) continue;
                board[cell[0]][cell[1]] = true;
            }
            gameData.Board = board;
            _gameRepository.Update(boardId, gameData);
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

        public Game GetGeneration(string boardId)
        {
            return _gameRepository.GetById(boardId);
        }

        public FinalStateResponse GetFinalState(string boardId)
        {
            bool[][] currentGame = null;
            for (int i = 0; i < _gameSettings.MaxGeneration; i++)
            {
                (currentGame, var hasChanged) = NextGeneration(boardId);
                if (!hasChanged)
                {
                    return new FinalStateResponse { Conclusion = true, Board = currentGame };
                }
            }
            return new FinalStateResponse { Conclusion = false, Board = currentGame };

        }

        public void Restart(string boardId)
        {
            var gameData = _gameRepository.GetById(boardId);
            var newGameData = new Game { Board = gameData.Board, Generation = 0 };
            _gameRepository.Update(boardId, newGameData);
        }
    }
}
