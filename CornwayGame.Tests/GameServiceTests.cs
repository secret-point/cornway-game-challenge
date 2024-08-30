using CornwayGame.BL;
using CornwayGame.BL.Interfaces;
using CornwayGame.Data.Interfaces;
using CornwayGame.Data.Model;
using Moq;

namespace CornwayGame.Tests
{
    public class GameServiceTests
    {
        private IGameService _gameService;
        private Mock<IGameRepository> _gameRepositoryMock;
        private Mock<IGameRules> _gameRulesMock;

        [SetUp]
        public void Setup()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _gameRulesMock = new Mock<IGameRules>();
            _gameService = new GameService(_gameRepositoryMock.Object, _gameRulesMock.Object);
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
            Game previousGame = new Game
            {
                Board = new bool[3][]{
                new bool[] { false, false, false, false, false } ,
                new bool[] { false, false, false, false, false },
                new bool[] { false, false, false, false, false }
            }
            };

            Game actualBoard = new Game { Board = new bool[0][] { } };
            bool[][] expectedBoard = new bool[3][]{
                new bool[] { false, false, false, false, false },
                new bool[] { false, false, true, false, false },
                new bool[] { false, false, false, false, true }
            };

            _gameRepositoryMock.Setup(x => x.GetById(expectedBoardId))
                    .Returns(previousGame);
            _gameRepositoryMock.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<Game>()))
                    .Callback<string, Game>((_, x) => actualBoard = x);

            _gameService.UpdateLiveCells(expectedBoardId, liveCellsCoordinates);

            for (int i = 0; i < expectedBoard.Length; i++)
            {
                var row = expectedBoard[i];
                for (int j = 0; j < row.Length; j++)
                {
                    Assert.That(actualBoard.Board[i][j], Is.EqualTo(row[j]));
                }
            }
        }

        [Test]
        public void GivenNonExistent_WhenFillLiveCells_ThenShouldThrowException()
        {
            _gameRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                    .Returns<Game>(null);
            Assert.Throws<ArgumentException>(() => _gameService.UpdateLiveCells(string.Empty, new int[][] { }), "Board Does not exists.");
        }

        [Test]
        public void GivenAExistent_WhenFillLiveCellsInvalid_ThenNotRunNormally()
        {
            string expectedBoardId = "2345";
            string actualBoardId = string.Empty;
            int[][] liveCellsCoordinates = new int[3][] { null, new int[] { 1, 2 }, new int[] { 2, 4 } };
            Game previousBoard = new Game
            {
                Board = new bool[3][]{
                new bool[] { false, false, false, false, false } ,
                new bool[] { false, false, false, false, false },
                new bool[] { false, false, false, false, false }
            }
            };

            Game actualBoard = new Game { Board = new bool[0][] { } };
            bool[][] expectedBoard = new bool[3][]{
                new bool[] { false, false, false, false, false },
                new bool[] { false, false, true, false, false },
                new bool[] { false, false, false, false, true }
            };

            _gameRepositoryMock.Setup(x => x.GetById(expectedBoardId))
                    .Returns(previousBoard);
            _gameRepositoryMock.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<Game>()))
                    .Callback<string, Game>((_, x) => actualBoard = x);

            _gameService.UpdateLiveCells(expectedBoardId, liveCellsCoordinates);

            for (int i = 0; i < expectedBoard.Length; i++)
            {
                var row = expectedBoard[i];
                for (int j = 0; j < row.Length; j++)
                {
                    Assert.That(actualBoard.Board[i][j], Is.EqualTo(row[j]));
                }
            }
        }
        #endregion

        #region Next Generation
        [Test]
        public void GivenAExistentBoard_WhenNextGeneration_ThenItShouldReturnSameBoard()
        {
            var expectedBoard = new Game
            {
                Board = new bool[3][] {
                new bool[] { false, false, false, false },
                new bool[] { false, false, false, false },
                new bool[] { false, false, false, false }
            }
            };

            var actualBoard = new Game { Board = new bool[0][] { } };

            _gameRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(expectedBoard);

            _gameRepositoryMock.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<Game>()))
                            .Callback<string, Game>((x, y) => actualBoard = y);

            _gameRulesMock.Setup(x => x.ShouldToggleCell(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool[][]>()))
                .Returns(false);
            _gameService.NextGeneration(string.Empty);

            Assert.That(expectedBoard.Board, Is.EqualTo(actualBoard.Board));
        }

        [Test]
        public void GivenAExistentBoard_WhenNextGeneration_ThenItShouldReturnDifferentBoardCells()
        {
            var expectedBoard = new Game
            {
                Board = new bool[3][] {
                new bool[] { false, false, false, false },
                new bool[] { false, false, false, false },
                new bool[] { false, false, false, false }
            }
            };

            var actualBoard = new Game { Board = new bool[0][] { } };

            _gameRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(expectedBoard);

            _gameRepositoryMock.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<Game>()))
                            .Callback<string, Game>((x, y) => actualBoard = y);

            _gameRulesMock.Setup(x => x.ShouldToggleCell(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool[][]>()))
                .Returns(true);

            _gameService.NextGeneration(string.Empty);

            Assert.That(expectedBoard.Board, Is.Not.EqualTo(actualBoard.Board));
        }
        #endregion

        
    }

}