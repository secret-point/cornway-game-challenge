using CornwayGame.BL;
using CornwayGame.BL.Interfaces;
using CornwayGame.Data.Interfaces;
using Moq;

namespace CornwayGame.Tests
{
    public class GameServiceTests
    {
        private IGameService _gameService;
        private Mock<IGameRepository> _gameRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _gameService = new GameService(_gameRepositoryMock.Object);
        }

        [Test]
        public void GivenANewBoard_WhenIsCreated_ThenShouldBeStored()
        {
            int height = 8;
            int width = 5;

            string expectedBoardId = "1234";

            bool[][] actualBoard = Array.Empty<bool[]>();

            _gameRepositoryMock.Setup(x => x.Add(It.IsAny<bool[][]>()))
                .Callback<bool[][]>((x) => actualBoard = x)
                .Returns(expectedBoardId);

            string actualBoarId = _gameService.CreateBoard(height, width);

            Assert.Multiple(() =>
            {
                Assert.That(expectedBoardId, Is.EqualTo(actualBoarId));
                Assert.That(width, Is.EqualTo(actualBoard.Length));
            });
            foreach (var row in actualBoard)
            {
                Assert.That(height, Is.EqualTo(row.Length));
            }
        }
    }