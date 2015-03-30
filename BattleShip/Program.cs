using System;
using System.Runtime.CompilerServices;


internal class BattleShip
{
    private int mode;
    private string username;
    private int[] create_fleet;
    private int[,] mapenemy;
    private char[] horizontal_axis;
    private int x, y;

    public BattleShip()
    {
        create_fleet = new int[5];
        for(int i=0;i<=9;i++)
            for (int j = 0; j <= 9; j++)
                mapplayer[i, j] = '0';

        mapenemy = new int[10, 10];

        horizontal_axis = new[] {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'};
    }

    public void InitMode()
    {
        string readline;
        Console.WriteLine("Choose game mode: \n \thuman vs computer \t mode: 1" +
                          "\n \thuman vs human \t\t mode: 2" +
                          "\n \tcomputer vs computer \t mode: 3 \n\n" +
                          "set mode number (example: 1)");

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
        
        for (;;)
        {
            Console.Clear();
            this.InitMap(mapplayer);
            Console.WriteLine("Set the coordinates for creation ship (example 1A)");
            this.ReadCoordinates();
            InitShipPoint();

        }
        
    }

    public void InitShipPoint()
    {
        char newchar;
        if (mapplayer[y, x] == '0')
        {
            newchar = '1';
            mapplayer[y, x] = newchar;

        }
        else if (mapplayer[y, x] == '1')
        {
            newchar = '0';
            mapplayer[y, x] = newchar;
        }
        else if (mapplayer[y, x] == 'b')
        {
            
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
        Console.WriteLine("x:{0} , y: {1}",x,y);
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

        if (wrong)
        {
            Console.WriteLine("Invalid input. Enter  Coordinates from 0A to 9J.\n\tfor exit: exit");
            return true;
        }

        else
            return false;

    }
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
                    Console.Write(ShowCharMap(map[i, j]) + " ");
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
        bt.InitMapPlayer();

    }
}


