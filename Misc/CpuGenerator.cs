using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CheckeredGameOfLife
{
    public abstract class CpuGenerator<T>
    {
        internal abstract List<T> AllItems { get; }
        internal readonly List<T> TakenItems = new();
        internal List<T> NonTakenItems => AllItems.Where(x => !TakenItems.Contains(x)).ToList();
        public T Random
        {
            get
            {
                T rand = NonTakenItems.Random();
                TakenItems.Add(rand);
                return rand;
            }
        }
    }
    public class CpuColorGenerator : CpuGenerator<Color>
    {
        internal override List<Color> AllItems => Constants.CpuPlayerColors;
    }
    public class CpuNameGenerator : CpuGenerator<string>
    {
        internal override List<string> AllItems => Constants.CpuPlayerNames;
    }
}
