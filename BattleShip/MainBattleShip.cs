using System;
using System.Runtime.CompilerServices;


interface IInterface
{
     
}




class BattleShipDemo
{
    private static void Main()
    {
        var bt=new BattleShip();
        bt.InitMode();

        if (bt.mode == 1)
        {
            var hm = new Humen(1);
            hm.InitMapPlayer();

            //create computer
            var cp = new Computer(2);
            cp.InitMapPlayer();
        }

        if (bt.mode == 2)
        {
            var hm = new Humen(1);
            hm.InitMapPlayer();

            var hm2 = new Humen(2);
            hm2.InitMapPlayer();
        }

        if (bt.mode == 3)
        {
            var cp = new Computer(1);
            cp.InitMapPlayer();

            var cp2 = new Computer(2);
            cp2.InitMapPlayer();
        }
    }
}


