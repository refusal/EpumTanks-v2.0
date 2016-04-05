using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpumTanks_v2._0
{
    public class Cell
    {
        public Direction TankDirection { get; private set; }
        public Direction BulletDirection { get; private set; }
        public ITank Tank { get; private set; }
        public Bullet Bullet { get; private set; }

        public void SetTank(ITank tank, Direction tankDirection)
        {
            Tank = tank;
            TankDirection = tankDirection;
        }

        public void SetBullet(Direction direction)
        {
            Bullet = new Bullet();
            BulletDirection = direction;
        }

        public void ClearTank()
        {
            Tank = null;
            //TankDirection = null;
        }

        public void ClearBullet()
        {
            Bullet = null;
            //BulletDirection = null;
        }
    }
}
