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

        #region Live Cell rules
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
        public void GivenALiveCell_WhenLiveCellNearTwoLiveCells_ThenItShouldNotToggle()
        {
            var board = new bool[3][]{
                new bool[3] { false, false, true },
                new bool[3] { true, true, false },
                new bool[3] { false, false, false }
            };
            var expected = false;
            var actual = _gameRules.ShouldToggleCell(1, 1, board);

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void GivenALiveCell_WhenLiveCellNearTreeLiveCells_ThenItShouldNotToggle()
        {
            var board = new bool[3][]{
                new bool[3] { false, true, true },
                new bool[3] { true, true, false },
                new bool[3] { false, false, false }
            };
            var expected = false;
            var actual = _gameRules.ShouldToggleCell(1, 1, board);

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void GivenALiveCell_WhenOverPopulationHappen_ThenItShouldToggle()
        {
            var board = new bool[3][]{
                new bool[3] { false, true, true },
                new bool[3] { true, true, true },
                new bool[3] { false, false, false }
            };
            var expected = true;
            var actual = _gameRules.ShouldToggleCell(1, 1, board);

            Assert.That(expected, Is.EqualTo(actual));
        }
        #endregion

        #region Dead Cell rules
        [Test]
        public void GivenDeadCell_WhenDoesNotMatchTreeLiveCell_ThenShouldNotToggleCell()
        {

            var board = new bool[3][]{
                new bool[3] { false, false, false },
                new bool[3] { false, false, false },
                new bool[3] { false, false, false }
            };
            var expected = false;
            var actual = _gameRules.ShouldToggleCell(1, 1, board);

            Assert.That(expected, Is.EqualTo(actual));
        }
        [Test]
        public void GivenDeadCell_WhenDoesMatchTreeLiveCell_ThenShouldToggleCell()
        {

            var board = new bool[3][]{
                new bool[3] { false, true, false },
                new bool[3] { true, false, false },
                new bool[3] { false, true, false }
            };
            var expected = true;
            var actual = _gameRules.ShouldToggleCell(1, 1, board);

            Assert.That(expected, Is.EqualTo(actual));
        }
        #endregion
    }
}
