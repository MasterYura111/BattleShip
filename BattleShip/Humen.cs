using System;

class Humen : Player
{
    public Humen(int u_id)
        : base(u_id)
    {
        this.InitUserName();
    }
    private void InitUserName()
    {
        Console.WriteLine("What is your name? (Player №" + user_id + ")");
        username = Console.ReadLine();
    }
    public void InitMapPlayer()
    {

        int first_x;
        int first_y;
        int second_x;
        int second_y;
        string ansvererror;
        string text_first = "";

        for (int i = 4; i >= 1; i--)
        {
            for (int j = 1; j <= this.create_fleet[i]; j++)
            {
                Console.Clear();
                this.ShowMode();
                this.ShowUserName();
                Console.WriteLine("step: Create map user");
                this.InitMap(mapplayer);

                for (; ; )
                {
                    if (i != 1)
                        text_first = "first";
                    else
                        text_first = "";
                    Console.WriteLine("enter " + text_first + " point coordinates {0}x ship, count:{1}/{2}", i, j, create_fleet[i]);
                    this.ReadCoordinates(out first_x, out first_y);
                    if (i != 1)
                    {
                        Console.WriteLine("enter second point coordinates {0}x ship, count:{1}/{2}", i, j, create_fleet[i]);
                        this.ReadCoordinates(out second_x, out second_y);
                    }
                    else
                    {
                        second_x = first_x;
                        second_y = first_y;
                    }


                    if (this.CheckingTwoPoint(first_x, first_y, second_x, second_y, i, out  ansvererror))
                    {
                        this.InstalShipTwoPoints(first_x, first_y, second_x, second_y);
                        this.BlockingAdjacentPoints(first_x, first_y, second_x, second_y);
                        break;
                    }
                    else
                        Console.WriteLine("Error:'" + ansvererror + "'. Try again.\n\tfor exit: exit");

                }
            }
        }
    }
}