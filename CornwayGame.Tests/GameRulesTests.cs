using CornwayGame.BL.GameRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornwayGame.Tests
{
    public class GameRulesTests
    {
        private GameRules _gameRules;
        [SetUp]
        public void SetUp()
        {
            _gameRules = new GameRules();
        }

        [Test]
        public void GivenLiveCell_WhenMatchUnderpopulationRule_ShouldReturnTrue()
        {
            var board = new bool[3][]{
                new bool[3] { false, false, false },
                new bool[3] { false, true, false },
                new bool[3] { false, false, false }
            };
            var expected = true;
            var actual = _gameRules.ShouldToggleCell(1, 1, board);


            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void GivenAExistentBoard_WhenLiveCellNearTwoLiveCells_ThenItShouldReturnDifferentBoardCells()
        {
            var board = new bool[3][]{
                new bool[3] { false, false, true },
                new bool[3] { true, true, false },
                new bool[3] { false, false, false }
            };
            var expected = true;
            var actual = _gameRules.ShouldToggleCell(1, 1, board);


            Assert.That(expected, Is.EqualTo(actual));
        }
    }
}
