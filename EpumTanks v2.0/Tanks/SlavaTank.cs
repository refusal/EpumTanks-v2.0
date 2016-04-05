using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpumTanks_v2._0
{
    class SlavaTank : ITank
    {
        static Random random = new Random();

        public Action GetAction(Field field)
        {
            int value = random.Next(5);
            if (value == 1)
                return Action.Back;
            else
            if (value == 2)
                return Action.TurnLeft;
            else
            if (value == 3)
                return Action.TurnRight;
            else
            if (value == 4)
                return Action.Forward;
            else
                return Action.Fire;
        }
    }
}
