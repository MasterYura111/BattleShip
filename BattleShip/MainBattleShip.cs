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
            var hm = new Humen(1);
            var cp = new Computer(2);

            bt.InitOngoingGame(hm,cp);
            
        }

        if (bt.mode == 2)
        {
            var hm = new Humen(1);
           

            var hm2 = new Humen(2);
           
        }

        if (bt.mode == 3)
        {
            var cp = new Computer(1);
           

            var cp2 = new Computer(2);
            bt.InitOngoingGame(cp, cp2);
            
        }
    }
}


