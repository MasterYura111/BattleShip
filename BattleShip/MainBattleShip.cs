using System;
using System.Runtime.CompilerServices;


class BattleShipDemo
{
    private static void Main()
    {
        var bt=new BattleShip();

        bt.InitMode();

        if (bt.mode == 1)
        {
            var hm = new Humen(1, bt.mode);
            var cp = new Computer(2, bt.mode);

            bt.InitOngoingGame(hm, cp);
            
        }

        if (bt.mode == 2)
        {
            var hm = new Humen(1, bt.mode);
            var hm2 = new Humen(2, bt.mode);

            bt.InitOngoingGame(hm, hm2);
        }

        if (bt.mode == 3)
        {
           var cp = new Computer(1, bt.mode);
           var cp2 = new Computer(2, bt.mode);

           bt.InitOngoingGame(cp, cp2);
        }
    }
}


