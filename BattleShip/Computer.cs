using System;

class Computer : Player,IPlayer
{
    
    public Computer(int u_id)
        : base(u_id)
    {
        this.InitUserName();
        this.InitMapPlayer();
    }
    public void InitUserName()
    {
        username = "Computer";
    }

    public void InitMapPlayer()
    {
        this.InitMap(2);
    }

   
    

    
}