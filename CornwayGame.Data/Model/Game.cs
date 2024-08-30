using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornwayGame.Data.Model
{
    public class Game
    {
        public List<bool[][]> Boards { get; set; } = new List<bool[][]>();
        public required bool[][] Board
        {
            get
            {
                return Boards.Last();
            }
            set
            {
                Boards = new List<bool[][]> { value};
            }
        }
        public int Generation { get; set; }

    }
}
