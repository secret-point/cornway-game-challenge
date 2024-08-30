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
        #region Create Board Tests
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

        [Test]
        public void GivenAExistent_WhenFillLiveCells_ThenShouldHaveLiveCells()
        {
            string expectedBoardId = "2345";
            string actualBoardId = string.Empty;
            int[][] liveCellsCoordinates = new int[2][] { new int[] { 1, 2 }, new int[] { 2, 4 } };
            bool[][] previousBoard = new bool[3][]{
                new bool[] { false, false, false, false, false } ,
                new bool[] { false, false, false, false, false },
                new bool[] { false, false, false, false, false }
            };

            bool[][] actualBoard = Array.Empty<bool[]>();
            bool[][] expectedBoard = new bool[3][]{
                new bool[] { false, false, false, false, false },
                new bool[] { false, false, true, false, false },
                new bool[] { false, false, false, false, true }
            };

            _gameRepositoryMock.Setup(x => x.GetById(expectedBoardId))
                    .Returns(previousBoard);
            _gameRepositoryMock.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<bool[][]>()))
                    .Callback<string, bool[][]>((_, x) => actualBoard = x);

            _gameService.UpdateLiveCells(expectedBoardId, liveCellsCoordinates);

            for (int i = 0; i < expectedBoard.Length; i++)
            {
                var row = expectedBoard[i];
                for (int j = 0; j < row.Length; j++)
                {
                    Assert.That(actualBoard[i][j], Is.EqualTo(row[j]));
                }
            }
        }

        [Test]
        public void GivenNonExistent_WhenFillLiveCells_ThenShouldThrowException()
        {
            _gameRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                    .Returns<bool[][]>(null);
            Assert.Throws<ArgumentException>(() => _gameService.UpdateLiveCells(string.Empty, new int[][] { }), "Board Does not exists.");
        }

        [Test]
        public void GivenAExistent_WhenFillLiveCellsInvalid_ThenNotRunNormally()
        {
            string expectedBoardId = "2345";
            string actualBoardId = string.Empty;
            int[][] liveCellsCoordinates = new int[3][] { null, new int[] { 1, 2 }, new int[] { 2, 4 } };
            bool[][] previousBoard = new bool[3][]{
                new bool[] { false, false, false, false, false } ,
                new bool[] { false, false, false, false, false },
                new bool[] { false, false, false, false, false }
            };

            bool[][] actualBoard = Array.Empty<bool[]>();
            bool[][] expectedBoard = new bool[3][]{
                new bool[] { false, false, false, false, false },
                new bool[] { false, false, true, false, false },
                new bool[] { false, false, false, false, true }
            };

            _gameRepositoryMock.Setup(x => x.GetById(expectedBoardId))
                    .Returns(previousBoard);
            _gameRepositoryMock.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<bool[][]>()))
                    .Callback<string, bool[][]>((_, x) => actualBoard = x);

            _gameService.UpdateLiveCells(expectedBoardId, liveCellsCoordinates);

            for (int i = 0; i < expectedBoard.Length; i++)
            {
                var row = expectedBoard[i];
                for (int j = 0; j < row.Length; j++)
                {
                    Assert.That(actualBoard[i][j], Is.EqualTo(row[j]));
                }
            }
        }
        #endregion

    }

}