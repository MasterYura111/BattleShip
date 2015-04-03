using System;

class Player : BattleShip
{
    protected string username;
    protected char[,] mapplayer;
    protected char[,] mapenemy;
    protected int user_id;

    public Player(int u_id)
    {
        user_id = u_id;
        mapplayer = new char[10, 10];
        for (int i = 0; i <= 9; i++)
            for (int j = 0; j <= 9; j++)
                mapplayer[i, j] = '0';

        mapenemy = new char[10, 10];

    }

    protected void ShowUserName()
    {
        Console.WriteLine("User: " + username + " (" + user_id + ")");
    }

    protected void ReadCoordinates(out int x, out int y)
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
    private bool ParceCoordinates(string readline, out int x, out int y)
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
    protected bool CheckingTwoPoint(int first_x, int first_y, int second_x, int second_y, int length_ship, out string ansvererror)
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
    protected void InstalShipTwoPoints(int first_x, int first_y, int second_x, int second_y)
    {
        for (int i = Math.Min(first_x, second_x); i <= Math.Max(first_x, second_x); i++)
            for (int j = Math.Min(first_y, second_y); j <= Math.Max(first_y, second_y); j++)
                mapplayer[j, i] = '1';
    }
    protected void BlockingAdjacentPoints(int first_x, int first_y, int second_x, int second_y)
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