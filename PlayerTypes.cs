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
    public class Player_Human : Player
    {
        public Player_Human(Color c) : base(c) { }
        public override int GetPlayerRoll()
        {
            while(true)
            {
                // have a button which breaks this loop and rolls the teetotum when clicked
            }
        }
        public override Move GetPlayerMove()
        {
            while(true)
            {
                // add buttons to all available tiles which breaks this loop and moves the player when clicked
            }
        }
    }
}
