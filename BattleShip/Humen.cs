using System;

class Humen : Player,IPlayer
{
    public Humen(int u_id, int Mode)
        : base(u_id)
    {
        this.mode = Mode;
        this.InitUserName();
        bool ShowProcess=true;
        this.InitMapPlayer(ShowProcess);
    }
    public void InitUserName()
    {
        username = "Human";
    }
    public void InitMapPlayer(bool ShowProcess)
    {
        ShowProcess = true;
        Console.Clear();
        this.ShowUserName();
        Console.WriteLine("Create map manually? \t Enter y/n (y-manually, n-random)");

        int ManuallOrRandom = 0;

        string readline;
        for (; ; )
        {
            readline = Console.ReadLine();
            if (String.IsNullOrEmpty(readline))
                readline = "";
            if (readline == "exit")
                Environment.Exit(0);

            if (readline == "y")
            {
                ManuallOrRandom = 1;
                break;
            }
            else if (readline == "n")
            {
                ManuallOrRandom = 2;
                break;
            }
            else
            {
                Console.WriteLine("Incorrect. Enter y if manually, n if random)");
            }
        }
        this.InitMap(ManuallOrRandom,true);
    }
}