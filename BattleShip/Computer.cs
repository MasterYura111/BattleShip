using System;

class Computer : Player
{
    private int[] availablepointmapplayer;
    public Computer(int u_id)
        : base(u_id)
    {
        availablepointmapplayer = new int[100];
        for (int i = 0; i < 100; i++)
            availablepointmapplayer[i] = i;

        this.InitUserName();
    }
    private void InitUserName()
    {
        username = "Computer";
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
            for (int j = 1; j <= this.create_fleet[i] + 1; j++)
            {
                Console.Clear();
                this.ShowMode();
                this.ShowUserName();
                Console.WriteLine("step: Create map user");
                this.InitMap(mapplayer);
                if (j <= create_fleet[i])
                    for (; ; )
                    {
                        if (i != 1)
                            text_first = "first";
                        else
                            text_first = "";
                        Console.WriteLine("enter " + text_first + " point coordinates {0}x ship, count:{1}/{2}", i, j, create_fleet[i]);

                        //random point
                        this.RandomPoints(out first_x, out first_y);

                        Console.WriteLine("random point: " + this.horizontal_axis[first_x] + first_y);
                        System.Threading.Thread.Sleep(1000);
                        if (i != 1)
                        {
                            Console.WriteLine("enter second point coordinates {0}x ship, count:{1}/{2}", i, j, create_fleet[i]);

                            if (!this.RandomSecondPoint(first_x, first_y, i, out second_x, out second_y))
                                Console.WriteLine("Error second point");

                            Console.WriteLine("random point: " + this.horizontal_axis[second_x] + second_y);

                            System.Threading.Thread.Sleep(1000);
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
                            this.UpdateAvailablePoint();
                            break;
                        }
                        else
                            Console.WriteLine("Error:'" + ansvererror + "'. Try again.\n\tfor exit: exit");

                    }
            }
        }
    }

    private void UpdateAvailablePoint()
    {
        availablepointmapplayer = new int[0];

        for (int i = 0; i <= 9; i++)
            for (int j = 0; j <= 9; j++)
            {
                if (this.mapplayer[j, i] == '0')
                {
                    Array.Resize(ref this.availablepointmapplayer, this.availablepointmapplayer.Length + 1);

                    string str_i = i.ToString();
                    string str_j = j.ToString();
                    string new_str_i_j = str_j + str_i;
                    int new_yx = Convert.ToInt32(new_str_i_j);

                    this.availablepointmapplayer[this.availablepointmapplayer.Length - 1] = new_yx;
                }

            }
    }
    private bool RandomSecondPoint(int first_x, int first_y, int length_ship, out int x, out int y)
    {
        int random;
        x = y = -5;
        //PossibleCoordinatesSecondPoint
        int[][] PossiblePoints = new int[0][];
        length_ship--;
        if (first_x - length_ship >= 0)
        {
            if (this.mapplayer[first_y, first_x - length_ship] == '0')
            {
                Array.Resize(ref PossiblePoints, PossiblePoints.Length + 1);
                PossiblePoints[PossiblePoints.Length - 1] = new int[] { first_y, first_x - length_ship };
            }
        }
        if (first_x + length_ship <= 9)
        {
            if (this.mapplayer[first_y, first_x + length_ship] == '0')
            {
                Array.Resize(ref PossiblePoints, PossiblePoints.Length + 1);
                PossiblePoints[PossiblePoints.Length - 1] = new int[] { first_y, first_x + length_ship };
            }
        }
        if (first_y - length_ship >= 0)
        {
            if (this.mapplayer[first_y - length_ship, first_x] == '0')
            {
                Array.Resize(ref PossiblePoints, PossiblePoints.Length + 1);
                PossiblePoints[PossiblePoints.Length - 1] = new int[] { first_y - length_ship, first_x };
            }
        }
        if (first_y + length_ship <= 9)
        {
            if (this.mapplayer[first_y + length_ship, first_x] == '0')
            {
                Array.Resize(ref PossiblePoints, PossiblePoints.Length + 1);
                PossiblePoints[PossiblePoints.Length - 1] = new int[] { first_y + length_ship, first_x };
            }
        }

        if (PossiblePoints.Length > 0)
        {
            Random rnd = new Random();
            random = rnd.Next(PossiblePoints.Length);
            y = PossiblePoints[random][0];
            x = PossiblePoints[random][1];

            return true;
        }
        else
        {
            return false;
        }

    }

    private bool RandomPoints(out int x, out int y)
    {
        int random;
        string randomcoordinates;

        Random rnd = new Random();
        random = rnd.Next(availablepointmapplayer.Length);
        randomcoordinates = availablepointmapplayer[random].ToString("D2");
        y = Convert.ToInt32(randomcoordinates[0].ToString());
        x = Convert.ToInt32(randomcoordinates[1].ToString());
        return true;

    }
}