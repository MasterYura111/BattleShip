using System;
using System.Runtime.CompilerServices;


class MainBattleShip:BattleShip
{
    public  MainBattleShip()
    {
        this.InitMode();
        Player pl1;
        Player pl2;
        if (this.mode == 1)
        {
              pl1 = new Humen(1, this.mode);
              pl2 = new Computer(2, this.mode);
              this.InitOngoingGame(pl1, pl2);
        }

        if (this.mode == 2)
        {
             pl1 = new Humen(1, this.mode);
             pl2 = new Humen(2, this.mode);
             this.InitOngoingGame(pl1, pl2);
        }

        if (this.mode == 3)
        {
             pl1 = new Computer(1, this.mode);
             pl2 = new Computer(2, this.mode);
             this.InitOngoingGame(pl1, pl2);
        }
    }
}


