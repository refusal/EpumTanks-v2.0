using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace EpumTanks_v2._0
{
    public class GameManager
    {
        public List<ITank> tanks = new List<ITank>();
        public Field field { get; private set; }

        public void CreateField(int x, int y)
        {
            field = new Field(x, y);

        }

        public void CreateTanks()//
        {
            tanks.Add(new SlavaTank());
            tanks.Add(new SlavaTank());
            tanks.Add(new SlavaTank());
            tanks.Add(new SlavaTank());
            SetTank(tanks[0], Direction.Up,10,10);
            SetTank(tanks[1], Direction.Up,10,50);
            SetTank(tanks[2], Direction.Up,50,50);
            SetTank(tanks[3], Direction.Up,50,10);
        }

        private void DoTankAction(ITank tank, Action action)
        {
            int i;
            int x=0;
            int y=0;
            field.GetTankCoord(ref x,ref y, tank);
            if (x > 0 && y > 0 && y + 1 < field.cells.GetLength(1) && x + 1 < field.cells.GetLength(0))
            ClearTank(tank, x, y);
            else
            {
                tanks.Remove(tank);
                return;
            }
            switch (action)
            {

                case Action.Back:
                    #region
                    switch (field.cells[x, y].TankDirection)
                    {
                        case Direction.Down:
                            if (y> 0)
                                SetTank(tank, field.cells[x, y].TankDirection,x,y-1);
                            break;
                        case Direction.Up:
                            if (y + 1 < field.cells.GetLength(1))
                                SetTank(tank, field.cells[x, y].TankDirection,x,y+1);
                            break;
                        case Direction.Left:
                            if (x + 1 < field.cells.GetLength(0))
                                SetTank(tank, field.cells[x, y].TankDirection,x+1,y);
                            break;
                        case Direction.Right:
                            if (x> 0)
                                SetTank(tank, field.cells[x, y].TankDirection,x-1,y);
                            break;
                    }

                    #endregion
                    break;
                case Action.Forward:
                    #region
                    switch (field.cells[x, y].TankDirection)
                    {
                        case Direction.Down:
                            if (y> 0)
                            SetTank(tank, field.cells[x, y].TankDirection,x,y+1);
                            break;
                        case Direction.Up:
                            if (y - 1 < field.cells.GetLength(1))
                            SetTank(tank, field.cells[x, y].TankDirection,x,y-1);
                            break;
                        case Direction.Left:
                            if (x - 1 < field.cells.GetLength(0))
                            SetTank(tank, field.cells[x, y].TankDirection,x-1,y);
                            break;
                        case Direction.Right:
                            if (x> 0)
                            SetTank(tank, field.cells[x, y].TankDirection,x+1,y);
                            break;
                    }

                    #endregion
                    break;
                case Action.TurnLeft:
                    i = (int)field.cells[x, y].TankDirection;
                    if (i == (int)Direction.Up)
                    {
                        i = (int)Direction.Left;
                        SetTank(tank, (Direction)i,x,y);
                    }
                    else
                    {
                        i--;
                        SetTank(tank, (Direction)i,x,y);
                    }
                    break;
                case Action.TurnRight:
                    i = (int)field.cells[x, y].TankDirection;
                    if (i == (int)Direction.Left)
                    {
                        i = (int)Direction.Up;
                        SetTank(tank, (Direction)i,x,y);
                    }
                    else
                    {
                        i++;
                        SetTank(tank, (Direction)i,x,y);
                    }
                    break;
                case Action.Fire:
                    SetTank(tank, field.cells[x, y].TankDirection, x, y);
                    DoFire(tank,x,y);
                    break;
                
            }
        }

        private void DoFire(ITank tank,int x,int y)
        {
            switch (field.cells[x, y].TankDirection)
            {
                case Direction.Down:
                    if (y + 2 < field.cells.GetLength(1))
                    {
                        field.cells[x, y + 2].SetBullet(field.cells[x, y].TankDirection);
                    }
                    break;
                case Direction.Up:
                    if (y - 2 >= 0)
                    {
                        field.cells[x, y - 2].SetBullet(field.cells[x, y].TankDirection);
                    }
                    break;
                case Direction.Left:
                    if (x - 2 >= 0)
                    {
                        field.cells[x - 2, y].SetBullet(field.cells[x, y].TankDirection);
                    }
                    break;
                case Direction.Right:
                    if (x + 2 < field.cells.GetLength(0))
                    {
                        field.cells[x + 2, y].SetBullet(field.cells[x, y].TankDirection);
                    }
                    break;
            }
        }

        private void ClearTank(ITank tank,int x,int y)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int a = y - 1; a <= y + 1; a++)
                {
                    if (field.cells[i, a].Tank == tank)
                        field.cells[i, a].ClearTank();
                }
            }
        }

        private void SetTank(ITank tank, Direction direction ,int x,int y)
        {

            if (x > 0 && y > 0 && x < (field.cells.GetLength(0) - 1) && y < (field.cells.GetLength(1) - 1))
            {
                switch (direction)
                {
                    case Direction.Up:
                        #region
                        if ((field.cells[x, y].Tank == null || field.cells[x, y].Tank == tank) &&
                            (field.cells[x + 1, y].Tank == null || field.cells[x + 1, y].Tank == tank) &&
                            (field.cells[x - 1, y].Tank == null || field.cells[x - 1, y].Tank == tank) &&
                            (field.cells[x, y - 1].Tank == null || field.cells[x, y - 1].Tank == tank) &&
                            (field.cells[x - 1, y + 1].Tank == null || field.cells[x - 1, y + 1].Tank == tank) &&
                            (field.cells[x + 1, y + 1].Tank == null || field.cells[x + 1, y + 1].Tank == tank))
                        {
                            field.cells[x + 1, y].SetTank(tank, direction);
                            field.cells[x - 1, y].SetTank(tank, direction);
                            field.cells[x, y - 1].SetTank(tank, direction);
                            field.cells[x - 1, y + 1].SetTank(tank, direction);
                            field.cells[x + 1, y + 1].SetTank(tank, direction);
                            field.cells[x, y].SetTank(tank, direction);
                        }
                        else
                        {
                            tanks.Remove(tank);
                        }
                        #endregion
                        break;
                    case Direction.Down:
                        #region

                        if ((field.cells[x, y].Tank == null || field.cells[x, y].Tank == tank) &&
                            (field.cells[x + 1, y].Tank == null || field.cells[x + 1, y].Tank == tank) &&
                            (field.cells[x - 1, y].Tank == null || field.cells[x - 1, y].Tank == tank) &&
                            (field.cells[x, y + 1].Tank == null || field.cells[x, y + 1].Tank == tank) &&
                            (field.cells[x - 1, y - 1].Tank == null || field.cells[x - 1, y - 1].Tank == tank) &&
                            (field.cells[x + 1, y - 1].Tank == null || field.cells[x + 1, y - 1].Tank == tank))
                        {
                            field.cells[x, y].SetTank(tank, direction);
                            field.cells[x + 1, y].SetTank(tank, direction);
                            field.cells[x - 1, y].SetTank(tank, direction);
                            field.cells[x - 1, y - 1].SetTank(tank, direction);
                            field.cells[x + 1, y - 1].SetTank(tank, direction);
                            field.cells[x, y + 1].SetTank(tank, direction);
                        }
                        else
                        {
                            tanks.Remove(tank);

                        }
                        #endregion
                        break;
                    case Direction.Left:
                        #region

                        if ((field.cells[x, y].Tank == null || field.cells[x, y].Tank == tank) &&
                            (field.cells[x + 1, y - 1].Tank == null || field.cells[x + 1, y - 1].Tank == tank) &&
                            (field.cells[x - 1, y].Tank == null || field.cells[x - 1, y].Tank == tank) &&
                            (field.cells[x, y - 1].Tank == null || field.cells[x, y - 1].Tank == tank) &&
                            (field.cells[x, y + 1].Tank == null || field.cells[x, y + 1].Tank == tank) &&
                            (field.cells[x + 1, y + 1].Tank == null || field.cells[x + 1, y + 1].Tank == tank))
                        {
                            field.cells[x, y].SetTank(tank, direction);
                            field.cells[x + 1, y - 1].SetTank(tank, direction);
                            field.cells[x - 1, y].SetTank(tank, direction);
                            field.cells[x, y - 1].SetTank(tank, direction);
                            field.cells[x, y + 1].SetTank(tank, direction);
                            field.cells[x + 1, y + 1].SetTank(tank, direction);
                        }
                        else
                        {
                            tanks.Remove(tank);
                        }
                        #endregion
                        break;
                    case Direction.Right:
                        #region

                        if ((field.cells[x, y].Tank == null || field.cells[x, y].Tank == tank) &&
                           (field.cells[x + 1, y].Tank == null || field.cells[x + 1, y].Tank == tank) &&
                           (field.cells[x, y + 1].Tank == null || field.cells[x, y + 1].Tank == tank) &&
                           (field.cells[x, y - 1].Tank == null || field.cells[x, y - 1].Tank == tank) &&
                           (field.cells[x - 1, y + 1].Tank == null || field.cells[x - 1, y + 1].Tank == tank) &&
                           (field.cells[x - 1, y - 1].Tank == null || field.cells[x - 1, y - 1].Tank == tank))
                        {
                            field.cells[x, y].SetTank(tank, direction);
                            field.cells[x + 1, y].SetTank(tank, direction);
                            field.cells[x, y + 1].SetTank(tank, direction);
                            field.cells[x, y - 1].SetTank(tank, direction);
                            field.cells[x - 1, y + 1].SetTank(tank, direction);
                            field.cells[x - 1, y - 1].SetTank(tank, direction);
                        }
                        else
                        {
                            tanks.Remove(tank);
                        }
                        #endregion
                        break;

                }
            }
        }


        public delegate void Graphic();
        public delegate void Message(string winner);
        public event Graphic AfterTickEvent;
        public event Message AfterEndEvent;
        public void Run()
        {
            do
            {
                Tick();
                AfterTickEvent();
            } while (!IsEnd());
            AfterEndEvent(tanks[0].GetType().ToString());
        }

        private bool IsEnd()
        {
            return tanks.Count < 2;
        }

        private void MoveBullets()
        {
            for (int i = 0; i < field.cells.GetLength(0); i++)
            {
                for (int j = 0; j < field.cells.GetLength(1); j++)
                {
                    if(field.cells[i,j].Bullet!=null)
                    {
                        switch (field.cells[i, j].BulletDirection)
                        {
                            case Direction.Down:
                                if (j + 1 < field.cells.GetLength(1))
                                {
                                    field.cells[i, j].ClearBullet();
                                    j++;
                                    field.cells[i, j].SetBullet(field.cells[i, j - 1].BulletDirection);
                                    CheckShot(i, j);

                                }
                                else
                                {
                                    field.cells[i, j].ClearBullet();

                                }
                                break;
                            case Direction.Up:
                                if (j > 0)
                                {
                                    field.cells[i, j].ClearBullet();
                                    j--;
                                    field.cells[i, j].SetBullet(field.cells[i, j + 1].BulletDirection);
                                    CheckShot( i, j);

                                }
                                else
                                {
                                    field.cells[i, j].ClearBullet();

                                }
                                break;
                            case Direction.Left:
                                if (i > 0)
                                {
                                    field.cells[i, j].ClearBullet();
                                    i--;
                                    field.cells[i, j].SetBullet(field.cells[i + 1, j].BulletDirection);
                                    CheckShot(i, j);

                                }
                                else
                                {
                                    field.cells[i, j].ClearBullet();

                                }
                                break;

                            case Direction.Right:
                                if (i + 1 < field.cells.GetLength(0))
                                {
                                    field.cells[i, j].ClearBullet();
                                    i++;
                                    field.cells[i, j].SetBullet(field.cells[i - 1, j].BulletDirection);
                                    CheckShot(i, j);

                                }
                                else
                                {
                                    field.cells[i, j].ClearBullet();

                                }
                                break;

                        }
                    }
                }
            }
            
        }

        private void CheckShot(int x,int y)
        {
            if (field.cells[x, y].Tank != null)
            {
                field.cells[x, y].ClearBullet();
                var tank = field.cells[x, y].Tank;
                int cx = 0;
                int cy = 0;
                field.GetTankCoord(ref cx,ref cy, tank);
                ClearTank(tank,cx,cy);
                tanks.Remove(tank);

            }
        }

        private void Tick()
        {
            var tankActionByTanks = tanks.Select(t => new KeyValuePair<ITank, Action>(t, t.GetAction(field))).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            foreach (var Tank in tankActionByTanks.Keys)
            {
                var action = tankActionByTanks[Tank];
                DoTankAction(Tank, action);
            }
            MoveBullets();

            

        }

    }
}
