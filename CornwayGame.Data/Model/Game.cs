using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornwayGame.Data.Model
{
    public class Game
    {
        public required bool[][] Board { get; set; }
        public int Generation { get; set; }
        
    }
}
