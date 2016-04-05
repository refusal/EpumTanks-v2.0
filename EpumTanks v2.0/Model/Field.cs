using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpumTanks_v2._0
{
    public class Field
    {
        public Cell[,] cells { get; private set; }

        public Field(int width, int height)
        {
            cells = new Cell[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    cells[i, j] = new Cell();
                }
            }
        }

        public void GetBulletCoord(ref int x,ref int y,Bullet bullet)
        {
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; i < cells.GetLength(1); i++)
                {
                    if (cells[i, j].Bullet == bullet)
                    {
                        x = i;
                        y = j;
                    }
                }
            }
        }

        public void GetTankCoord(ref int x,ref int y, ITank tank)
        {
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    if(cells[i,j].Tank==tank)
                    {
                        x = i;
                        y = j;
                        DefineCenter(cells[i, j].TankDirection, ref x, ref y);
                        return;
                    }
                }
            }
        }

        private void DefineCenter(Direction direction, ref int x,ref int y)
        {
            switch(direction)
            {
                case Direction.Down:
                    x++;
                    y++;
                    break;
                case Direction.Up:
                    x++;
                    break;
                case Direction.Left:
                    x++;
                    break;
                case Direction.Right:
                    x++;
                    y++;
                    break;
                
            }
        }


    }
}
