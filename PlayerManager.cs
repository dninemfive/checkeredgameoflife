using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckeredGameOfLife
{
    public class PlayerManager : IEnumerable<Player>
    {
        private List<Player> _players;
        public IEnumerator<Player> GetEnumerator() => _players.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _players.GetEnumerator();
        private int _currentPlayerInd = 0;
        public int CurrentPlayerInd
        {
            get => _currentPlayerInd;
            private set => _currentPlayerInd = (value % _players.Count);
        }
        public Player CurrentPlayer => _players[CurrentPlayerInd];
        public Player GetNextPlayer() => _players[++CurrentPlayerInd];        
    }
}
