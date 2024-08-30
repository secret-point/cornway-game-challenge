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
            return string.Empty;
        }
    }
}
