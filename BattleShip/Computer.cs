using System;

class Computer : Player,IPlayer
{
    
    public Computer(int u_id, int Mode)
        : base(u_id)
    {
        this.mode = Mode;
        this.InitUserName();

        bool ShowProcess=true;
        if (this.mode == 1)
            ShowProcess = false;

        this.InitMapPlayer(ShowProcess);
    }
    public void InitUserName()
    {
        username = "Computer";
    }

    public void InitMapPlayer(bool ShowProcess)
    {
        this.InitMap(2, ShowProcess);
    }

}