using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpumTanks_v2._0
{
    public interface ITank
    {
        Action GetAction(Field field);
    }
}
