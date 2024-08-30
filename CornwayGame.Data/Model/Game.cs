using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornwayGame.Data.Model
{
    public class Game
    {
        public bool[][][] Boards { get; set; } = new bool[0][][];
        public required bool[][] Board
        {
            get
            {
                return new bool[Board.Length][];
            }
            set
            {
                var boards = new bool[Board.Length + 1][][];
                Array.Copy(Boards, boards, Boards.Length);
                boards[Board.Length] = value;
                Boards = boards;
            }
        }
        public int Generation { get; set; }

    }
}
