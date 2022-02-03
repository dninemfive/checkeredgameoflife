using System;
using System.Collections.Generic;
using System.Text;

namespace CheckeredGameOfLife
{
    public class Tile
    {
        #region TileDefs
        public static readonly Tile_GainPoints Wealth = new("Wealth", 10);
        public static readonly Tile Matrimony = new("Matrimony");
        public static readonly Tile_GoTo Gambling = new("Gambling", Ruin);
        public static readonly Tile_GainPoints HappyOldAge = new("Happy Old Age", 5);
        public static readonly Tile_GoTo Perseverence = new("Perseverance", Success);
        public static readonly Tile Truth = new("Truth");
        public static readonly Tile_GoTo Politics = new("Politics", Congress);
        public static readonly Tile_GoTo Intemperance = new("Intemperance", Intemperance);
        public static readonly Tile_GoTo Crime = new("Crime", Prison);
        public static readonly Tile_GainPoints Happiness = new("Happiness", 5);
        public static readonly Tile_GoTo Idleness = new("Idleness", Disgrace);
        public static readonly Tile_GainPoints Success = new("Success", 5);
        public static readonly Tile_GainPoints Congress = new("Congress", 5);
        public static readonly Tile_GainPoints Honor = new("Honor");
        public static readonly Tile_GoTo Cupid = new("Cupid", Matrimony);
        public static readonly Tile_GoToByRole Speculation = new(
            "Speculation",
            new()
            {
                (new int[] { 3, 6 }, Wealth)
            },
            Ruin
        );
        public static readonly Tile_GoTo Honesty = new("Honesty", Happiness);
        public static readonly Tile_GoTo Industry = new("Industry", Wealth);
        public static readonly Tile_GoTo Bravery = new("Bravery", Honor);
        public static readonly Tile Ruin = new("Ruin");
        public static readonly Tile Poverty = new("Poverty");
        public static readonly Tile_GoTo Ambition = new("Ambition", Fame);
        public static readonly Tile_GainPoints College = new("College");
        public static readonly Tile_Death Suicide = new("Suicide");
        public static readonly Tile_GoTo School = new("School", College);
        public static readonly Tile_GoTo Influence = new("Influence", FatOffice);
        public static readonly Tile Fame = new("Fame");
        public static readonly Tile_GainPoints FatOffice = new("Fat Office");
        public static readonly Tile Infancy = new("Infancy");
        public static readonly Tile Disgrace = new("Disgrace");
        public static readonly Tile Jail = new("Jail");
        public static readonly SkipNextTurn Prison = new("Prison");

        public static readonly DefaultDict<string, Tile> TilesByName = new()
        {
            { Perseverence.Name, Perseverence },
            { Truth.Name,        Truth        },
            { Politics.Name,     Politics     },
            { Intemperance.Name, Intemperance },
            { Crime.Name,        Crime        },
            { Happiness.Name,    Happiness    },
            { Idleness.Name,     Idleness     },
            { Success.Name,      Success      },
            { Congress.Name,     Congress     },
            { Honor.Name,        Honor        },
            { Cupid.Name,        Cupid        },
            { Speculation.Name,  Speculation  },
            { Honesty.Name,      Honesty      },
            { Industry.Name,     Industry     },
            { Bravery.Name,      Bravery      },
            { Ruin.Name,         Ruin         },
            { Poverty.Name,      Poverty      },
            { Ambition.Name,     Ambition     },
            { College.Name,      College      },
            { Suicide.Name,      Suicide      },
            { School.Name,       School       },
            { Influence.Name,    Influence    },
            { Fame.Name,         Fame         },
            { FatOffice.Name,    FatOffice    },
            { Infancy.Name,      Infancy      },
            { Disgrace.Name,     Disgrace     },
            { Jail.Name,         Jail         },
            { Prison.Name,       Prison       }
        };
        #endregion TileDefs
        public string Name { get; private set; } = null;
        public Uri IconUri { get; private set; } = null;
        public (int x, int y) Pos { get; private set; }
        public Player Player { get; private set; } = null;
        public Tile(string name) 
        {
            Name = name;
            IconUri = new Uri(name);
        }
        public virtual void ReceivePlayer(Player p)
        {
            if (Player != null) Player.GoTo(Jail);
            Player = p;
        }
    }
    public class Tile_GainPoints : Tile
    {
        public int Points { get; private set; }
        public Tile_GainPoints(string name, int points = 5) : base(name)
        {
            Points = points;
        }
        public override void ReceivePlayer(Player p)
        {
            base.ReceivePlayer(p);
            p.Points += Points;
        }
    }
    public class Tile_GoTo : Tile
    {
        public Tile TargetTile { get; private set; }
        public Tile_GoTo(string name, Tile targetTile) : base(name)
        {
            TargetTile = targetTile;
        }
        public override void ReceivePlayer(Player p)
        {
            // no call of base because a player can never stay here
            p.GoTo(TargetTile);
        }
    }
    public class SkipNextTurn : Tile
    {
        public SkipNextTurn(string name) : base(name) { }
        public override void ReceivePlayer(Player p)
        {
            base.ReceivePlayer(p);
            p.SkipNextTurn();
        }
    }
    public class Tile_Death : Tile
    {
        public Tile_Death(string name) : base(name) { }
        public override void ReceivePlayer(Player p)
        {
            base.ReceivePlayer(p);
            p.Dead = true;
        }
    }
    public class Tile_GoToByRole : Tile
    {
        public DefaultDict<int, Tile> TargetTilesByRoll;
        public Tile_GoToByRole(string name, HashSet<(int[], Tile)> targetTilesByRoll, Tile defaultTile) : base(name)
        {
            TargetTilesByRoll = new(defaultTile);
            foreach ((int[] ns, Tile t) in targetTilesByRoll)
                foreach (int n in ns) TargetTilesByRoll[n] = t;
        }
        public override void ReceivePlayer(Player p)
        {
            base.ReceivePlayer(p);
            p.GoTo(TargetTilesByRoll[Game.Roll()]);
        }
    }
}
