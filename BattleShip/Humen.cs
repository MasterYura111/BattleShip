using System;

class Humen : Player,IPlayer
{
    public Humen(int u_id)
        : base(u_id)
    {
        this.InitUserName();
        this.InitMapPlayer();
    }
    public void InitUserName()
    {
        Console.WriteLine("What is your name? (Player №" + user_id + ")");
        username = Console.ReadLine();
    }
    public void InitMapPlayer()
    {

        this.InitMap(1);
    }
}