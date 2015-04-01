using System;
using System.Runtime.CompilerServices;


internal class BattleShip
{
    private int mode;
    private string[] modename;
    private string username;
    private int[] create_fleet;
    private char[,] mapplayer;
    private int[,] mapenemy;
    private char[] horizontal_axis;
    private int x, y;

    public BattleShip()
    {
        create_fleet = new int[5];
        mapplayer = new char[10, 10];
        for(int i=0;i<=9;i++)
            for (int j = 0; j <= 9; j++)
                mapplayer[i, j] = '0';

        mapenemy = new int[10, 10];

        horizontal_axis = new[] {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'};
        this.InitCreatFleet();

        modename = new[] {"", "human vs computer", "human vs human", "computer vs computer"};
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
                mode = Convert.ToInt32(readline);
                if (mode != 1 && mode != 2 && mode != 3)
                    Console.WriteLine("Invalid input. Enter  value from 1 to 3.\n\tfor exit: exit ");
                else
                    break;
            }



        }

        //Console.WriteLine("mode:" + mode);

    }

    public void InitUserName()
    {
        Console.WriteLine("What is your name?");
        username = Console.ReadLine();
        //Console.WriteLine("username:" + username);
    }

    public void InitCreatFleet()
    {
        create_fleet[1] = 4;
        create_fleet[2] = 3;
        create_fleet[3] = 2;
        create_fleet[4] = 1;
    }

    public void InitMapPlayer()
    {
        
            this.InitShipPoint();
        
        
    }

    public void ShowMode()
    {
        Console.WriteLine("Mode: "+modename[mode]);
    }

    public void ShowUserName()
    {
        Console.WriteLine("User: " + username);
    }
    public void InitShipPoint()
    {
        int first_x;
        int first_y;
        int second_x;
        int second_y;
        string ansvererror;
        string text_first = "";

        for (int i=4;i>=1;i--)
        {
            for (int j = 1; j <= create_fleet[i]; j++)
            {
                Console.Clear();
                this.ShowMode();
                this.ShowUserName();
                Console.WriteLine("step: Create map user");
                this.InitMap(mapplayer);

                for (;;)
                {

                    if (i != 1)
                        text_first = "first";
                    else
                        text_first = "";
                    Console.WriteLine("enter " + text_first + " point coordinates {0}x ship, count:{1}/{2}", i, j, create_fleet[i]);
                    this.ReadCoordinates();
                    
                    first_x = x;
                    first_y = y;

                    if (i != 1)
                    {
                        Console.WriteLine("enter second point coordinates {0}x ship, count:{1}/{2}", i, j, create_fleet[i]);
                        this.ReadCoordinates();
                    }

                    second_x = x;
                    second_y = y;
                    

                    //check all options
                    if (this.CheckingTwoPoint(first_x, first_y, second_x, second_y, i, out  ansvererror))
                    {
                        this.InstalShipTwoPoints(first_x, first_y, second_x, second_y);
                        this.BlockingAdjacentPoints(first_x, first_y, second_x, second_y);
                        break;

                    }
                    else
                    {
                        Console.WriteLine("Error:'" + ansvererror + "'. Try again.\n\tfor exit: exit");
                    }
                }
                
            }
            
        }

        

    }

    public void BlockingAdjacentPoints(int first_x, int first_y, int second_x, int second_y)
    {
        for (int i=Math.Min(first_x,second_x)-1;i<=Math.Max(first_x,second_x)+1;i++)
            for (int j = Math.Min(first_y, second_y) - 1; j <= Math.Max(first_y, second_y) + 1; j++)
            {
                if((i>=0 && i<=9) && (j>=0 && j<=9))
                    if(mapplayer[j,i]!='1')
                        mapplayer[j, i] = 'b';
            }
    }
    public void InstalShipTwoPoints(int first_x, int first_y, int second_x, int second_y)
    {
        for(int i=Math.Min(first_x,second_x);i<=Math.Max(first_x,second_x);i++)
            for (int j = Math.Min(first_y, second_y); j <= Math.Max(first_y, second_y); j++)
                mapplayer[j, i] = '1';
    }
    public bool CheckingTwoPoint(int first_x, int first_y, int second_x, int second_y, int length_ship,out string  ansvererror)  //перевірка всіх правил по двом точкам
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
            if (Math.Abs(first_y - second_y) != length_ship-1)
            {
                ansvererror = "incorrect length ship: " + (Math.Abs(first_y - second_y)) +"!="+ (length_ship - 1)+"";
                return false;
            }
            
            for (int i = Math.Min(first_y, second_y); i <= Math.Max(first_y, second_y); i++)
            {
                if (mapplayer[i, first_x] != '0')
                {
                    ansvererror = "point on the map is not available or blocked point: "+i+","+first_x+"";
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
   
    public bool CheckShipPoint(int current_x, int current_y)
    {
       
        if (mapplayer[current_y, current_x] == '0')
        {
            mapplayer[current_y, current_x] = '1';
            return true;
        }
        else if (mapplayer[current_y, current_x] == '1' || mapplayer[current_y, current_x] == 'b')
        {
            return false;
        }
        else
        {
            return false;
        }
        
            
    }

   
    public void ReadCoordinates()
    {
        string readline;
        for (;;)
        {
            readline = Console.ReadLine();
            if (String.IsNullOrEmpty(readline))
                readline = "";
            if (readline == "exit")
                    Environment.Exit(0);

            if(!this.ParceCoordinates(readline))
                break;
            
        }
    }
    

    public bool ParceCoordinates(string readline)
    {
        
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
    public void  InitMap( char[,] map)
    {
            Console.WriteLine("\t");
            Console.Write(" | ");
    
            for (int j = 0; j <= 9; j++)
            {
                Console.Write(horizontal_axis[j]+" ");
            }
            
            Console.WriteLine();
            Console.WriteLine();
            for (int i=0; i <= 9; i++)
            {
                if (i%2 == 0)
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                else
                    Console.BackgroundColor = ConsoleColor.DarkGray;

                Console.Write(i+"| ");
                for(int j=0;j<=9;j++)
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

        public string ShowCharMap(char i)
        {
            if (i == '0')
                return " ";
            else if(i=='1')
                return "X";
            else if (i == 'b')
                return "b";
            else
            {
                return "";
            }
        }
}


       
    

class BattleShipDemo
{
    public static void Main()
    {
        BattleShip bt=new BattleShip();
        bt.InitMode();
        bt.InitUserName();
        bt.InitMapPlayer();

    }
}


