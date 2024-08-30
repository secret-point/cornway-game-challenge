using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornwayGame.BL.Model
{
    public class FinalStateResponse
    {
        public bool Conclusion { get; set; }
        public bool[][] Board { get; internal set; }
    }
}
