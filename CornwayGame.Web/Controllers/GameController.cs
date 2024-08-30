using CornwayGame.BL.Interfaces;
using CornwayGame.Web.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CornwayGame.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public string NewGame([FromBody] NewGameDTO newGameDTO)
        {
            var boardId = _gameService.CreateBoard(newGameDTO.Height, newGameDTO.Width);
            _gameService.UpdateLiveCells(boardId, newGameDTO.LiveCellCoordinates);

            return boardId;
        }

        [HttpPost("{boardId}/NextGeneration")]
        public bool[][] NextGeneration([FromRoute] string boardId)
        {
            var nextGenerationBoard = _gameService.NextGeneration(boardId);
            return nextGenerationBoard;
        }

        [HttpPost("{boardId}/GetGeneration")]
        public int GetGeneration([FromRoute] string boardId)
        {
            var nextGenerationBoard = _gameService.GetGeneration(boardId);
            return nextGenerationBoard.Generation;
        }

        [HttpPost("{boardId}/GetFinalState")]
        public int GetFinalState([FromRoute] string boardId)
        {
            var nextGenerationBoard = _gameService.GetGeneration(boardId);
            return nextGenerationBoard.Generation;
        }

        [HttpPost("{boardId}/Restart")]
        public int Restart([FromRoute] string boardId)
        {
            var nextGenerationBoard = _gameService.GetGeneration(boardId);
            return nextGenerationBoard.Generation;
        }
    }
}
