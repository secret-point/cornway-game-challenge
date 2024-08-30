using CornwayGame.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornwayGame.Data.Interfaces
{
    public interface IGameRepository
    {
        string Add(bool[][] bools);
        Game GetById(string boardId);
        void Update(string boardId, Game GameData);
    }
}
