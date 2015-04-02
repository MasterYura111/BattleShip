using System;
using System.Runtime.CompilerServices;


 class BattleShip
{
    public int mode;
    public string[] modename;
    public int[] create_fleet;
    public char[] horizontal_axis;

    
    public BattleShip()
    {   
        horizontal_axis = new[] {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'};
        
        modename = new[] {"", "human vs computer", "human vs human", "computer vs computer"};

        create_fleet = new int[5];
        create_fleet[1] = 4;
        create_fleet[2] = 3;
        create_fleet[3] = 2;
        create_fleet[4] = 1;
    }
    public void InitMode()
    {
        string readline;
        
        Console.Write("Choose game mode: ");
        for (int i = 1; i <= 3; i++)
        {
            Console.Write("\n\t"+modename[i]+" \tmode: "+i);
        }
        Console.WriteLine();

        for (;;)
        {
            readline = Console.ReadLine();

            if (readline == "exit")
                Environment.Exit(0);

            if (String.IsNullOrEmpty(readline))
            {
                Console.WriteLine("Invalid input. Enter  value from 1 to 3.\n\tfor exit: exit ");
            }
            else
            {
                this.mode = Convert.ToInt32(readline);
                if (this.mode != 1 && this.mode != 2 && this.mode != 3)
                    Console.WriteLine("Invalid input. Enter  value from 1 to 3.\n\tfor exit: exit ");
                else
                    break;
            }
        }
    }
   

    public string ShowCharMap(char ch)
    {
        if (ch == '0')
            return " ";
        else if (ch == '1')
            return "X";
        else if (ch == 'b')
            return "b";
        else
        {
            return "";
        }
    }

    public void InitMap(char[,] map)
    {
        Console.WriteLine("\t");
        Console.Write(" | ");

        for (int j = 0; j <= 9; j++)
        {
            Console.Write(horizontal_axis[j] + " ");
        }

        Console.WriteLine();
        Console.WriteLine();
        for (int i = 0; i <= 9; i++)
        {
            if (i % 2 == 0)
                Console.BackgroundColor = ConsoleColor.DarkCyan;
            else
                Console.BackgroundColor = ConsoleColor.DarkGray;

            Console.Write(i + "| ");
            for (int j = 0; j <= 9; j++)
            {
                if (ShowCharMap(map[i, j]).Equals("X"))
                    Console.ForegroundColor = ConsoleColor.Blue;
                else
                    Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write(ShowCharMap(map[i, j]) + " ");

                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine();
        }
        Console.ResetColor();
        Console.WriteLine();
    }
    public void ShowMode()
    {
        Console.WriteLine("Mode: " + modename[mode]+"mode: "+mode);
    }
}

class Player:BattleShip
{
    public string username;
    public char[,] mapplayer;
    public char[,] mapenemy;
    public int user_id;

    public Player(int u_id)
    {
        user_id = u_id;
        mapplayer = new char[10, 10];
        for (int i = 0; i <= 9; i++)
            for (int j = 0; j <= 9; j++)
                mapplayer[i, j] = '0';

        mapenemy = new char[10, 10];

    }
   
    public void ShowUserName()
    {
        Console.WriteLine("User: " + username+" ("+user_id+")");
    }
    
   public void ReadCoordinates(out int x, out int y)
    {
        
        string readline;
        for (; ; )
        {
            readline = Console.ReadLine();
            if (String.IsNullOrEmpty(readline))
                readline = "";
            if (readline == "exit")
                Environment.Exit(0);

            if (!this.ParceCoordinates(readline, out x, out y))
                break;
        }
    }
    public bool ParceCoordinates(string readline, out int x, out int y)
    {
        x = y = -5;
        bool wrong = false;

        if (readline.Length != 2)
            wrong = true;
        else if (String.IsNullOrEmpty(readline))
        {
            wrong = true;
        }
        else
        {
            if ((readline[0] >= '0' && readline[0] <= '9') &&
                ((readline[1] >= 'A' && readline[1] <= 'J') || (readline[1] >= 'a' && readline[1] <= 'j')))
            {
                x = Array.IndexOf(horizontal_axis, Char.ToUpper(readline[1]));
                y = Convert.ToInt32(readline[0].ToString());
            }
            else if ((readline[1] >= '0' && readline[1] <= '9') &&
                ((readline[0] >= 'A' && readline[0] <= 'J') || (readline[0] >= 'a' && readline[0] <= 'j')))
            {
                x = Array.IndexOf(horizontal_axis, Char.ToUpper(readline[0]));
                y = Convert.ToInt32(readline[1].ToString());
            }
            else
                wrong = true;
        }

        if (wrong)
        {
            Console.WriteLine("Invalid input. Enter  Coordinates from 0A to 9J.\n\tfor exit: exit");
            return true;
        }
        else
            return false;
    }
    public bool CheckingTwoPoint(int first_x, int first_y, int second_x, int second_y, int length_ship, out string ansvererror)
    {
        ansvererror = "";
        bool one_point = false;
        if (first_x == second_x && first_y == second_y && length_ship == 1)
            one_point = true;

        if ((first_x != second_x && first_y != second_y) ||
            (first_x == second_x && first_y == second_y && !one_point))
        {
            ansvererror = "two coordinates not create a line";
            return false;
        }
        else if (first_x == second_x && !one_point)
        {
            if (Math.Abs(first_y - second_y) != length_ship - 1)
            {
                ansvererror = "incorrect length ship: " + (Math.Abs(first_y - second_y)) + "!=" + (length_ship - 1) + "";
                return false;
            }

            for (int i = Math.Min(first_y, second_y); i <= Math.Max(first_y, second_y); i++)
            {
                if (mapplayer[i, first_x] != '0')
                {
                    ansvererror = "point on the map is not available or blocked point: " + i + "," + first_x + "";
                    return false;
                }

            }
            return true;
        }
        else if (first_y == second_y && !one_point)
        {
            if (Math.Abs(first_x - second_x) != length_ship - 1)
            {
                ansvererror = "incorrect length ship: " + (Math.Abs(first_y - second_y)) + "!=" + (length_ship - 1) + "";
                return false;
            }

            for (int i = Math.Min(first_x, second_x); i <= Math.Max(first_x, second_x); i++)
            {
                if (mapplayer[first_y, i] != '0')
                {
                    ansvererror = "point on the map is not available or blocked point: " + i + "," + first_y + "";
                    return false;
                }
            }
            return true;
        }
        else if (one_point)
        {
            if (mapplayer[first_y, first_x] != '0')
            {
                ansvererror = "point on the map is not available or blocked point: " + first_x + "," + first_y + "";
                return false;
            }
            return true;
        }
        else
            return false;

    }
    public void InstalShipTwoPoints(int first_x, int first_y, int second_x, int second_y)
    {
        for (int i = Math.Min(first_x, second_x); i <= Math.Max(first_x, second_x); i++)
            for (int j = Math.Min(first_y, second_y); j <= Math.Max(first_y, second_y); j++)
                mapplayer[j, i] = '1';
    }
    public void BlockingAdjacentPoints(int first_x, int first_y, int second_x, int second_y)
    {
        for (int i = Math.Min(first_x, second_x) - 1; i <= Math.Max(first_x, second_x) + 1; i++)
            for (int j = Math.Min(first_y, second_y) - 1; j <= Math.Max(first_y, second_y) + 1; j++)
            {
                if ((i >= 0 && i <= 9) && (j >= 0 && j <= 9))
                    if (mapplayer[j, i] != '1')
                        mapplayer[j, i] = 'b';
            }
    }
}

class Humen : IPlayer
{
    public  Humen(int u_id):base(u_id)
    {
        this.InitUserName();
    }
    public void InitUserName()
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


class Computer : IPlayer
{
    private int[] availablepointmapplayer;
    public Computer(int u_id)
        : base(u_id)
    {
        var pl = new Player();

        availablepointmapplayer =new int[100];
        for (int i = 0; i < 100; i++)
            availablepointmapplayer[i] = i;

        this.InitUserName();
    }
    public void InitUserName()
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
            for (int j = 1; j <= this.create_fleet[i]+1; j++)
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
                        
                            if(!this.RandomSecondPoint(first_x, first_y, i,out second_x,out second_y))
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
        int[][] PossiblePoints=new int[0][];
        length_ship--;
        if (first_x - length_ship >=0)
        {
            if (this.mapplayer[first_y, first_x - length_ship] == '0')
            {
                Array.Resize(ref PossiblePoints, PossiblePoints.Length+1);
                PossiblePoints[PossiblePoints.Length - 1] = new int[] { first_y, first_x - length_ship };
            }
        }
        if (first_x + length_ship <=9)
        {
            if (this.mapplayer[first_y, first_x + length_ship] == '0')
            {
                Array.Resize(ref PossiblePoints, PossiblePoints.Length + 1);
                PossiblePoints[PossiblePoints.Length - 1] = new int[] { first_y, first_x + length_ship };
            }
        }
        if (first_y - length_ship >= 0)
        {
            if (this.mapplayer[first_y-length_ship, first_x] == '0')
            {
                Array.Resize(ref PossiblePoints, PossiblePoints.Length + 1);
                PossiblePoints[PossiblePoints.Length - 1] = new int[] { first_y - length_ship, first_x };
            }
        }
        if (first_y + length_ship <=9)
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

    public bool RandomPoints(out int x, out int y)
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



interface IPlayer
{
    void InitMapPlayer();
}


class BattleShipDemo
{
    public static void Main()
    {
        BattleShip bt=new BattleShip();
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


