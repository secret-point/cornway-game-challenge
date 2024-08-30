using CornwayGame.Data.Interfaces;
using CornwayGame.Data.Model;

namespace CornwayGame.Data
{
    public class GameRepository : IGameRepository
    {
        private readonly Dictionary<string, Game> _gameRepository;

        public GameRepository() {
            _gameRepository = new Dictionary<string, Game>();
        }
        public string Add(bool[][] board)
        {
            var boardId=Guid.NewGuid().ToString();
            _gameRepository.Add(boardId, new Game { Generation = 0, Board=board });

            return boardId;
        }

        public Game GetById(string boardId)
        {
            return _gameRepository[boardId];
        }

        public void Update(string boardId, Game gameData)
        {
            _gameRepository[boardId] = gameData;
        }
    }
}
