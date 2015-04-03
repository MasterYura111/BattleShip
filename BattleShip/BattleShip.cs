using System;


    class BattleShip
    {
        public int mode;
        private string[] modename;
        protected int[] create_fleet;
        protected char[] horizontal_axis;


        public BattleShip()
        {
            horizontal_axis = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };

            modename = new[] { "", "human vs computer", "human vs human", "computer vs computer" };

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


        private string ShowCharMap(char ch)
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

        protected void InitMap(char[,] map)
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
        protected void ShowMode()
        {
            Console.WriteLine("Mode: " + modename[mode] + "mode: " + mode);
        }
    }
