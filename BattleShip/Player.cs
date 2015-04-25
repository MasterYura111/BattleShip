using System;

class Player : BattleShip
{
    protected string username;
    public char[,] mapplayer;
    public int?[] RandomWay;
    public bool IsNullRandomWay=true;
    public int[] LastShootPoint;
    public int LastShootWay;
    public int user_id;
    private int[] availablepointmapplayer;
    public char LastMissWoundedKilled;

    protected Player(int u_id)
    {
        user_id = u_id;
        mapplayer = new char[10, 10];
        for (int i = 0; i <= 9; i++)
            for (int j = 0; j <= 9; j++)
                mapplayer[i, j] = '0';

        availablepointmapplayer = new int[100];
        for (int i = 0; i < 100; i++)
            availablepointmapplayer[i] = i;
    }
    public void ShowUserName()
    {
        Console.WriteLine("User: " + username + " (" + user_id + ")");
    }
    private void ReadCoordinates(out int x, out int y)
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
    private bool CheckingTwoPoint(int first_x, int first_y, int second_x, int second_y, int length_ship, out string ansvererror)
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
    private void InstalShipTwoPoints(int first_x, int first_y, int second_x, int second_y)
    {
        for (int i = Math.Min(first_x, second_x); i <= Math.Max(first_x, second_x); i++)
            for (int j = Math.Min(first_y, second_y); j <= Math.Max(first_y, second_y); j++)
                mapplayer[j, i] = '1';
    }
    private void BlockingAdjacentPoints(int first_x, int first_y, int second_x, int second_y)
    {
        for (int i = Math.Min(first_x, second_x) - 1; i <= Math.Max(first_x, second_x) + 1; i++)
            for (int j = Math.Min(first_y, second_y) - 1; j <= Math.Max(first_y, second_y) + 1; j++)
            {
                if ((i >= 0 && i <= 9) && (j >= 0 && j <= 9))
                    if (mapplayer[j, i] != '1')
                        mapplayer[j, i] = 'b';
            }
    }
    protected void InitMap(int UserOrComputer, bool ShowProcess)
    {
        int first_x;
        int first_y;
        int second_x;
        int second_y;
        string ansvererror;
        string text_first = "";
        bool PointInDeadlock = false;
        if (!ShowProcess)
        {
            Console.Clear();
            Console.WriteLine("loading create map computer");
        }
            
        for (int i = 4; i >= 1; i--)
        {
            for (int j = 1; j <= this.create_fleet[i]; j++)
            {
                if (ShowProcess)
                {
                    Console.Clear();
                    this.ShowMode();
                    this.ShowUserName();
                    Console.WriteLine("step: Create map user");
                    this.DisplayMap(mapplayer);
                }
                
                for (; ; )
                {
                    if (i != 1)
                        text_first = "first";
                    else
                        text_first = "";
                    if (ShowProcess)
                        Console.WriteLine("enter " + text_first + " point coordinates {0}x ship, count:{1}/{2}", i, j, create_fleet[i]);

                    this.CreateFirstPointInitMap(UserOrComputer, out first_x, out first_y, ShowProcess);

                    if (i != 1)
                    {
                        if (ShowProcess)
                            Console.WriteLine("enter second point coordinates {0}x ship, count:{1}/{2}", i, j, create_fleet[i]);

                        if (!this.CreateSecondPointInitMap(UserOrComputer, first_x, first_y, i, out second_x,
                            out second_y, ShowProcess))
                        {
                            //first point in deadlock
                            PointInDeadlock = true;
                        }
                    }
                    else
                    {
                        second_x = first_x;
                        second_y = first_y;
                    }

                    if (!PointInDeadlock & this.CheckingTwoPoint(first_x, first_y, second_x, second_y, i, out ansvererror))
                    {
                        this.InstalShipTwoPoints(first_x, first_y, second_x, second_y);
                        this.BlockingAdjacentPoints(first_x, first_y, second_x, second_y);
                        if (UserOrComputer == 2)
                            this.UpdateAvailablePoint();
                        break;
                    }
                    else
                    {
                        if (!PointInDeadlock & ShowProcess)
                            Console.WriteLine("Error:'" + ansvererror + "'. Try again.\n\tfor exit: exit");
                        else if (PointInDeadlock)
                        {
                            Console.WriteLine("Error:Point In Deadlock. cancel first point. Try again.\n\tfor exit: exit");
                            PointInDeadlock = false;
                        }
                    }
                }
            }
        }
    }
    private void CreateFirstPointInitMap(int UserOrComputer, out int first_x, out int first_y, bool ShowProcess)
    {
        first_x = -5;
        first_y = -5;
        if (UserOrComputer == 1)
        {
            this.ReadCoordinates(out first_x, out first_y);
        }
        else if (UserOrComputer == 2)
        {
            this.RandomPoints(out first_x, out first_y);
            if (ShowProcess)
                Console.WriteLine("random point: " + this.horizontal_axis[first_x] + first_y);
            System.Threading.Thread.Sleep(300);
        }
    }
    private bool CreateSecondPointInitMap(int UserOrComputer, int first_x, int first_y, int length_ship, out int second_x, out int second_y, bool ShowProcess)
    {
        second_x = -5;
        second_y = -5;
        if (UserOrComputer == 1)
        {
            this.ReadCoordinates(out second_x, out second_y);
            return true;
        }
        else if (UserOrComputer == 2)
        {
            if(!this.RandomSecondPoint(first_x, first_y, length_ship, out second_x, out second_y))
            {
                return false;
            }
            if (ShowProcess)
                Console.WriteLine("random point: " + this.horizontal_axis[second_x] + second_y);
            System.Threading.Thread.Sleep(300);
            return true;
        }
        return true;
    }
    private void RandomPoints(out int x, out int y)
    {
        int random;
        string randomcoordinates;

        Random rnd = new Random();
        random = rnd.Next(availablepointmapplayer.Length);
        randomcoordinates = availablepointmapplayer[random].ToString("D2");
        y = Convert.ToInt32(randomcoordinates[0].ToString());
        x = Convert.ToInt32(randomcoordinates[1].ToString());
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
    public bool InitPlayerShootToEnemy(Player pl, Player pl_enemy, out char MissWoundedKilled, bool ShowProcess)
    {
        bool IsHumen = false;
        if (pl is Humen)
            IsHumen = true;
        
        int x, y;
        bool? res;
        if (ShowProcess)
        {
            if(pl.LastMissWoundedKilled=='m')
                Console.WriteLine("You are miss");
            if (pl.LastMissWoundedKilled == 'w')
                Console.WriteLine("You are wounded");
            if (pl.LastMissWoundedKilled == 'k')
                Console.WriteLine("You are killed");
            Console.WriteLine("enter point coordinates shoot to enemy");
        }
            
        for (;;)
        {
            if(IsHumen)
                this.ReadCoordinates(out x, out y);
            else
            {
                this.RandomPointsToShootEnemy(out x, out y,pl,pl_enemy);
                if (ShowProcess)
                    Console.WriteLine("Random point: "+y+this.horizontal_axis[x]);
            }
            res = this.PlayerShootToEnemy(x, y, pl_enemy, out MissWoundedKilled);
            if (!IsHumen)
            {
                //mind computer shoot
                this.MindCmputerShoot(x, y,pl, MissWoundedKilled);
            }
            pl.LastMissWoundedKilled = MissWoundedKilled;

            if (res != null)
                break;
            else
            {
                if(ShowProcess)
                    Console.WriteLine("wrong coordinates. Point already shoot. Try again\n\tfor exit: exit");
            }
                
        }
        if (res == true)
            return true;
        else
            return false;
    }
    private void MindCmputerShoot(int x, int y,Player pl, char MissWoundedKilled)
    {

        if (pl.IsNullRandomWay == true && MissWoundedKilled == 'w')
        {
            pl.RandomWay = new int?[] {1, 1, 1, 1};
            pl.LastShootPoint = new[] { y, x };
            pl.IsNullRandomWay = false;

        }
        else if (MissWoundedKilled == 'k')
        {
            pl.RandomWay = null;
            pl.IsNullRandomWay = true;
        }
        else if (pl.IsNullRandomWay ==false && (MissWoundedKilled == 'w' || MissWoundedKilled == 'm'))
        {
            if (MissWoundedKilled == 'w')
            {
                if (pl.LastShootWay == 0)
                {
                    pl.RandomWay[2] =pl.RandomWay[3]= null;
                }
                else if (pl.LastShootWay == 1)
                {
                    pl.RandomWay[2] = pl.RandomWay[3] = null;
                }
                else if (pl.LastShootWay == 2)
                {
                    pl.RandomWay[0] = pl.RandomWay[1] = null;
                }
                else if (pl.LastShootWay == 3)
                {
                    pl.RandomWay[0] = pl.RandomWay[1] = null;
                }
            }
            if (MissWoundedKilled == 'm')
            {
                if (pl.LastShootWay == 0)
                {
                    pl.RandomWay[0] = null;
                }
                else if (pl.LastShootWay == 1)
                {
                    pl.RandomWay[1] = null;
                }
                else if (pl.LastShootWay == 2)
                {
                    pl.RandomWay[2] = null;
                }
                else if (pl.LastShootWay == 3)
                {
                    pl.RandomWay[3] = null;
                }
            }
        }
    }
    private void RandomPointsToShootEnemy(out int x, out int y,Player pl, Player pl_enemy)
    {
        x = -5;
        y = -5;
        //Console.WriteLine("pl.RandomWay:" + pl.RandomWay);
        if (pl.IsNullRandomWay == true)
        {
            int[][] availablepointmapplayer;
            availablepointmapplayer = this.AvailablePointToShootEnemy(pl_enemy);

            int[] point;
            int random;
            Random rnd = new Random();
            random = rnd.Next(availablepointmapplayer.Length);
            point = availablepointmapplayer[random];
            y = point[0];
            x = point[1];
        }
        else if (pl.IsNullRandomWay == false)
        {
            for (;;)
            {
                int random;
                int Way;

                int[] random_array = new int[0];
                for (int i = 0; i <= 3; i++)
                {
                    if (pl.RandomWay[i] != null)
                    {
                        Array.Resize(ref random_array, random_array.Length + 1);
                        random_array[random_array.Length - 1] = i;
                    }
                }
                Random rnd = new Random();
                //Console.WriteLine("random_array.Length:" + random_array.Length);
                random = rnd.Next(random_array.Length);
                Way = random_array[random];
                pl.LastShootWay = Way;

                if (Way == 0) //x+
                {
                    if (pl.LastShootPoint[1] + (int) pl.RandomWay[0] <= 9 )
                    {
                        x = pl.LastShootPoint[1] + (int) pl.RandomWay[0];
                        y = pl.LastShootPoint[0];
                        if (pl_enemy.mapplayer[y, x] == 'e' || pl_enemy.mapplayer[y, x] == 'm')
                            pl.RandomWay[0] = null;
                        else
                        {
                            pl.RandomWay[0]++;
                            break;    
                        }
                    }
                    else
                        pl.RandomWay[0] = null;
                }
                else if (Way == 1) //x-
                {
                    if (pl.LastShootPoint[1] - (int)pl.RandomWay[1] >=0)
                    {
                        x = pl.LastShootPoint[1] - (int)pl.RandomWay[1];
                        y = pl.LastShootPoint[0];
                        if (pl_enemy.mapplayer[y, x] == 'e' || pl_enemy.mapplayer[y, x] == 'm')
                            pl.RandomWay[1] = null;
                        else
                        {
                            pl.RandomWay[1]++;
                            break;
                        }
                    }
                    else
                        pl.RandomWay[1] = null;
                }
                else if (Way == 2) //y+
                {
                    if (pl.LastShootPoint[0] + (int)pl.RandomWay[2] <= 9)
                    {
                        x = pl.LastShootPoint[1];
                        y = pl.LastShootPoint[0] + (int) pl.RandomWay[2];
                        if (pl_enemy.mapplayer[y, x] == 'e' || pl_enemy.mapplayer[y, x] == 'm')
                            pl.RandomWay[2] = null;
                        else
                        {
                            pl.RandomWay[2]++;
                            break;
                        }
                    }
                    else
                        pl.RandomWay[2] = null;
                }
                else if (Way == 3) //y-
                {
                    if (pl.LastShootPoint[0] - (int)pl.RandomWay[3] >=0)
                    {
                        x = pl.LastShootPoint[1];
                        y = pl.LastShootPoint[0] - (int)pl.RandomWay[3];
                        if (pl_enemy.mapplayer[y, x] == 'e' || pl_enemy.mapplayer[y, x] == 'm')
                            pl.RandomWay[3] = null;
                        else
                        {
                            pl.RandomWay[3]++;
                            break;
                        }
                    }
                    else
                        pl.RandomWay[3] = null;
                }
            }
        }
    }
    private int[][] AvailablePointToShootEnemy(Player pl_enemy)
    {
       int[][] availablepointmapplayer=new int[0][];;

        for (int i = 0; i <= 9; i++)
            for (int j = 0; j <= 9; j++)
            {
                if (pl_enemy.mapplayer[j, i] != 'm' &&
                    pl_enemy.mapplayer[j, i] != 'k' &&
                    pl_enemy.mapplayer[j, i] != 'e' )
                {
                    Array.Resize(ref availablepointmapplayer, availablepointmapplayer.Length + 1);
                    availablepointmapplayer[availablepointmapplayer.Length - 1] = new int[] { j, i };
                }

            }
        return availablepointmapplayer;
    }
    private bool? PlayerShootToEnemy(int x, int y, Player pl_enemy, out char MissWoundedKilled)
    {
        MissWoundedKilled = '0';
        int[][] NeighboringPoints;
        int Neighboring_x,
            Neighboring_y;
        int variable=0;
        bool? MarkSetEmpty =null;
        int[] pointCoord;
        bool? EventPlus = null;

        int[][] NeighboringPoinsSetEmpty=new int[0][];;

        if (pl_enemy.mapplayer[y, x] == '0' || pl_enemy.mapplayer[y, x] == 'b')
        {
            pl_enemy.mapplayer[y, x] = 'm';
            MissWoundedKilled = 'm';
            return false;
        }
        else if (pl_enemy.mapplayer[y, x] == '1')
        {
            pl_enemy.mapplayer[y, x] = 'k';
            MissWoundedKilled = 'w';
            //cheked kill one ship? if yes when circle point set empty (if they 0,b)
            if (!this.CheckCharNeighboringPoints(x, y, '1', pl_enemy, out NeighboringPoints))
            {
                if (this.CheckCharNeighboringPoints(x, y, 'k', pl_enemy, out NeighboringPoints))
                {
                    Array.Resize(ref NeighboringPoinsSetEmpty, NeighboringPoinsSetEmpty.Length + 1);
                    NeighboringPoinsSetEmpty[NeighboringPoinsSetEmpty.Length - 1] = new int[] {y, x};

                    foreach (var point in NeighboringPoints)
                    {
                        if (MarkSetEmpty == false)
                            break;

                        Neighboring_x = point[1];
                        Neighboring_y = point[0];

                        pointCoord = new int[2];
                        if (x != Neighboring_x)
                        {
                            variable = Neighboring_x;
                            pointCoord = new[] {y, variable};
                            if (Neighboring_x > x)
                                EventPlus = true;
                            else if (Neighboring_x < x)
                                EventPlus = false;
                        }
                        if (y != Neighboring_y)
                        {
                            variable = Neighboring_y;
                            pointCoord = new[] {variable, x};
                            if (Neighboring_y > y)
                                EventPlus = true;
                            else if (Neighboring_y < y)
                                EventPlus = false;
                        }

                        for (;;)
                        {
                            if (variable > 9 || variable < 0)
                            {
                                MarkSetEmpty = true;
                                break;
                            }

                            if (pl_enemy.mapplayer[pointCoord[0], pointCoord[1]] == 'k')
                            {
                                Array.Resize(ref NeighboringPoinsSetEmpty, NeighboringPoinsSetEmpty.Length + 1);
                                NeighboringPoinsSetEmpty[NeighboringPoinsSetEmpty.Length - 1] = new int[]
                                {pointCoord[0], pointCoord[1]};
                            }

                            if (pl_enemy.mapplayer[pointCoord[0], pointCoord[1]] == '1')
                            {
                                MarkSetEmpty = false;
                                break;
                            }
                            else if (pl_enemy.mapplayer[pointCoord[0], pointCoord[1]] != '1' &&
                                     pl_enemy.mapplayer[pointCoord[0], pointCoord[1]] != 'k')
                            {
                                MarkSetEmpty = true;
                                break;
                            }

                            if (EventPlus == true)
                                variable++;
                            else if (EventPlus == false)
                                variable--;

                            if (x != Neighboring_x)
                                pointCoord = new[] {y, variable};
                            else if (y != Neighboring_y)
                                pointCoord = new[] {variable, x};
                        }
                    }

                    if (MarkSetEmpty == true)
                    {
                        MissWoundedKilled = 'k';
                        foreach (var  point in NeighboringPoinsSetEmpty)
                        {
                            this.SetEmptyCharNeighboringPoints(point[1], point[0], pl_enemy);
                        }
                    }
                    return true;
                }
                else
                {
                    MissWoundedKilled = 'k';
                    this.SetEmptyCharNeighboringPoints(x, y, pl_enemy);
                }
            }
            return true;
        }
        else
            return null;
    }
    private void SetEmptyCharNeighboringPoints(int x, int y, Player pl_enemy)
    {
        for (int i =x - 1; i <= x + 1; i++)
            for (int j = y - 1; j <= y + 1; j++)
            {
                if ((i >= 0 && i <= 9) && (j >= 0 && j <= 9))
                    if (pl_enemy.mapplayer[j, i] == '0' || pl_enemy.mapplayer[j, i] == 'b')
                        pl_enemy.mapplayer[j, i] = 'e';
            }
    }
    private bool CheckCharNeighboringPoints(int x, int y, char ch, Player pl_enemy, out int[][] NeighboringPoints)
    {
         NeighboringPoints = new int[0][];

        bool mark_have = false;
        if (x - 1 >= 0)
        {
            if (pl_enemy.mapplayer[y, x - 1] == ch)
            {
                Array.Resize(ref NeighboringPoints, NeighboringPoints.Length + 1);
                NeighboringPoints[NeighboringPoints.Length - 1] = new int[] { y, x-1 };
            }
        }
        if (x + 1 <= 9)
        {
            if (pl_enemy.mapplayer[y, x + 1] == ch)
            {
                Array.Resize(ref NeighboringPoints, NeighboringPoints.Length + 1);
                NeighboringPoints[NeighboringPoints.Length - 1] = new int[] { y, x + 1 };
            }
        }
        if (y - 1 >= 0)
        {
            if (pl_enemy.mapplayer[y-1,x] == ch)
            {
                Array.Resize(ref NeighboringPoints, NeighboringPoints.Length + 1);
                NeighboringPoints[NeighboringPoints.Length - 1] = new int[] { y-1, x};
            }
        }
        if (y + 1 <= 9)
        {
            if (pl_enemy.mapplayer[y+1, x] == ch)
            {
                Array.Resize(ref NeighboringPoints, NeighboringPoints.Length + 1);
                NeighboringPoints[NeighboringPoints.Length - 1] = new int[] { y + 1, x };
            }
        }

        if (NeighboringPoints.Length > 0)
            mark_have = true;

        return mark_have;
    }
    
}