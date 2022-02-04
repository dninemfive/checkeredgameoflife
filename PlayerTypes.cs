using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CheckeredGameOfLife
{
    public class Player_CPU_Random : Player
    {
        public Player_CPU_Random(Color c) : base(c) { }
        public override int GetPlayerRoll() => Game.Roll();
        public override Move GetPlayerMove() => AvailableMoves.Random();
    }
}
