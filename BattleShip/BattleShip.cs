using System;


    class BattleShip
    {
        protected int mode;
        private string[] modename;
        protected int[] create_fleet;
        protected char[] horizontal_axis;
        protected BattleShip()
        {
            this.InitValueBattleship();
        }
        private void InitValueBattleship()
        {
            horizontal_axis = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };

            modename = new[] { "", "human vs computer", "human vs human", "computer vs computer" };

            create_fleet = new int[5];
            create_fleet[1] = 4;
            create_fleet[2] = 3;
            create_fleet[3] = 2;
            create_fleet[4] = 1;
        }
        protected void InitMode()
        {
            string readline;

            Console.Write("Choose game mode: ");
            for (int i = 1; i <= 3; i++)
            {
                Console.Write("\n\t" + modename[i] + " \tmode: " + i);
            }
            Console.WriteLine();

            for (; ; )
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
        protected void InitOngoingGame(Player pl1, Player pl2)
        {
            Player pl;
            Player en;
            bool finish = false;
            bool ShowProcess = true;
            bool start = true;
            char MissWoundedKilled;
            pl = pl1;
            en = pl2;

            for (; ; )    //shooting
            {
                if (finish)
                    break;

                if (!start) //exchange players
                {
                    if (pl == pl1)
                    {
                        pl = pl2;
                        en = pl1;
                    }
                    else if (pl == pl2)
                    {
                        pl = pl1;
                        en = pl2;
                    }
                }

                for (; ; )    //Repeat if got shot
                {
                    if (pl.mode == 1 && pl is Humen)
                        ShowProcess = true;
                    else if (pl.mode == 2)
                        ShowProcess = true;
                    else if (pl.mode == 3 && pl.user_id == 1)
                        ShowProcess = true;
                    else
                        ShowProcess = false;

                    Console.Clear();

                    if (ShowProcess)
                    {
                        pl.ShowMode();
                        pl.ShowUserName();
                        Console.WriteLine("step: Play");
                        pl1.DisplayTwoMapOneRow(pl.mapplayer, en.mapplayer);
                    }
                    if(pl.mode==3 & ShowProcess)
                        System.Threading.Thread.Sleep(500);
                    if (pl.InitPlayerShootToEnemy(pl, en, out MissWoundedKilled, ShowProcess))
                    {
                        if (this.EnemyIsDead(en))
                        {
                            finish = true;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                
                start = false;
            }

            Console.Clear();

            pl.ShowMode();
            pl.ShowUserName();
            Console.WriteLine("step: Finish");
            pl.DisplayTwoMapOneRow(pl.mapplayer, en.mapplayer);
            Console.WriteLine();
            Console.WriteLine("Victory.Congratulations");
            pl.ShowUserName();
        }
        protected void ShowMode()
        {
            Console.WriteLine("Mode: " + modename[mode]);
        }
        protected void DisplayTwoMapOneRow(char[,] mapPlayer, char[,] MapEnemy)
        {
            Console.WriteLine();
            Console.WriteLine("Map Player \t\t\tMap Enemy");

            for (int count = 1; count <= 2; count++)
            {
                if (count == 2)
                    Console.Write("\t\t");
                Console.Write(" | ");

                for (int j = 0; j <= 9; j++)
                {
                    Console.Write(horizontal_axis[j] + " ");
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            
            for (int i = 0; i <= 9; i++)
            {

                for (int count = 1; count <= 2; count++)
                {
                    if (count == 2)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\t\t");
                    }
                    if (i % 2 == 0)
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                    else
                        Console.BackgroundColor = ConsoleColor.DarkGray;

                    Console.Write(i + "| ");
                    for (int j = 0; j <= 9; j++)
                    {
                        if (count == 1)
                        {
                            if (ShowCharMap(mapPlayer[i, j], 2).Equals("X"))
                                Console.ForegroundColor = ConsoleColor.Blue;
                            else if (ShowCharMap(mapPlayer[i, j], 2).Equals("K"))
                                Console.ForegroundColor = ConsoleColor.Red;
                            else
                                Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        else if (count == 2)
                        {
                            if (ShowCharMap(MapEnemy[i, j], 3).Equals("X"))
                                Console.ForegroundColor = ConsoleColor.Blue;
                            else if (ShowCharMap(MapEnemy[i, j], 3).Equals("K"))
                                Console.ForegroundColor = ConsoleColor.Red;
                            else
                                Console.ForegroundColor = ConsoleColor.Gray;
                        }

                        if (count == 1)
                            Console.Write(ShowCharMap(mapPlayer[i, j], 2) + " ");
                        else if (count == 2)
                            Console.Write(ShowCharMap(MapEnemy[i, j], 3) + " ");

                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor();
            Console.WriteLine();
        }
        private string ShowCharMap(char ch,int CreateOrPlay_player_or_enemy)
        {
            if (CreateOrPlay_player_or_enemy == 1)
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
            else if (CreateOrPlay_player_or_enemy == 2)
            {
                if (ch == '0') return " ";
                else if (ch == 'b') return " ";
                else if (ch == '1') return "X";
                else if (ch == 'm') return "M";
                else if (ch == 'k') return "K";
                else if (ch == 'e') return "e";
                else
                {
                    return "";
                }
            }
            else if (CreateOrPlay_player_or_enemy == 3)
            {
                if (ch == '0') return " ";
                else if (ch == 'b') return " ";
                else if (ch == '1') return " ";
                else if (ch == 'm') return "M";
                else if (ch == 'k') return "K";
                else if (ch == 'e') return "e";
                else
                {
                    return "";
                }
            }
            else return "";
        }
        protected void DisplayMap(char[,] map)
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
                    if (ShowCharMap(map[i, j], 1).Equals("X"))
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else
                        Console.ForegroundColor = ConsoleColor.Gray;

                    Console.Write(ShowCharMap(map[i, j], 1) + " ");

                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine();
            }
            Console.ResetColor();
            Console.WriteLine();
        }
        private bool EnemyIsDead(Player pl)
        {
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    if (pl.mapplayer[j, i] == '1')
                        return false;
                }
            }
            return true;
        }

    }

interface IPlayer
{
    void InitMapPlayer(bool ShowProcess);
    void InitUserName();
}