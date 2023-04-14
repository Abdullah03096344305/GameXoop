using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZInput;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GameVer1
{
    class Program
    {
        public class LastDay
        {
            public int playerX;
            public int playerY;
            public int zombie1X = 90;
            public int zombie1Y = 10;
            // 2nd Zombie Coordinates----------------------------
            public int zombie2X = 60;
            public int zombie2Y = 25;
            // 3rd Zombie Coordinates----------------------------
            public int zombie3X = 8;
            public int zombie3Y = 3;
            //Player Score=====================
            public int score = 0;
            //Player Health=================
            public int playerhealth = 10;
            //Zombies Health==============
            public int zombie1health = 5;
            public int zombie2health = 5;
            public int zombie3health = 5;
        }
        static void Main(string[] args)
        {         
            LastDay lastDay = new LastDay();

            //Player Coordinates-------------------------------------
            lastDay.playerX = 50;
            lastDay.playerY = 27;
            // 1st Zombie Coordinates----------------------------
            lastDay.zombie1X = 90;
            lastDay.zombie1Y = 10;
            // 2nd Zombie Coordinates----------------------------
            lastDay.zombie2X = 60;
            lastDay.zombie2Y = 25;
            // 3rd Zombie Coordinates----------------------------
            lastDay.zombie3X = 8;
            lastDay.zombie3Y = 3;
            //Zombies Directions-------------------------
            string direction = "up";
            string direction2 = "right";
            string direction3 = "up";
           /* string direction4 = "right";*/
            char previous = ' ';
            //Player Score=====================
            lastDay.score = 0;
            //Player Health=================
            lastDay.playerhealth = 10;
            //Zombies Health==============
            lastDay.zombie1health = 5;
            lastDay.zombie2health = 5;
            lastDay.zombie3health = 5;
            //For Better Speed +++++++++
            int timer = 3;
            //Player Right Bullet Settings=============
            int[] bulletX = new int[100];
            int[] bulletY = new int[100];
            bool[] isBulletActive = new bool[100];
            int bulletCount = 0;
            //Player Left Bullet Settings=============
            int[] bulletLeftX = new int[100];
            int[] bulletLeftY = new int[100];
            int bulletLeftCount = 0;
            bool[] isBulletLeftActive = new bool[100];
            char[,] maze = new char[39, 132];

            ReadData(maze);

            Console.SetCursorPosition(lastDay.playerX, lastDay.playerY);
            PrintPlayer(lastDay);
            Console.SetCursorPosition(lastDay.zombie1X, lastDay.zombie1Y);
            PrintZombie(lastDay);

            int option;
            Console.Clear(); 
            TopHeader();
            Console.WriteLine(); 
            Console.WriteLine();
            Console.WriteLine();
            ConsoleColor originalColor = Console.ForegroundColor; // Store the original console text color
            Console.ForegroundColor = ConsoleColor.Red; // Set the console text color to red
            Console.WriteLine(" 1:       Start Game");
            Console.WriteLine(" 2:       Options");
            Console.WriteLine(" 3:       Resume");
            Console.WriteLine(" 4:       Exit");
            Console.ForegroundColor = originalColor; // Restore the original console text color
            // Read user input
            string input = Console.ReadLine();
            if (int.TryParse(input, out option))
            {
              if(option == 1)
                {
                    PrintMaze(maze);
                    while (true)
                    {
                        if (direction3 == "up")
                        {
                            char next = maze[lastDay.zombie3Y - 1, lastDay.zombie3X - 3];
                            if (next == '*' || next == '%' || next == '|' || next == '#')
                            {
                                direction3 = "down";
                            }
                            else if (next == ' ' || next == '.')
                            {
                                EraseZombie3(lastDay);
                                lastDay.zombie3Y = lastDay.zombie3Y - 1;
                                lastDay.zombie3X = lastDay.zombie3X - 1;
                                previous = next;
                                PrintZombie3(lastDay);
                               
                            }
                        }
                        if (direction3 == "down")
                        {
                            char next = maze[lastDay.zombie3Y + 3, lastDay.zombie3X + 3];
                            if (next == '*' || next == '%' || next == '|')
                            {
                                direction3 = "up";
                            }
                            else if (next == ' ' || next == '.')
                            {
                                EraseZombie3(lastDay);
                                lastDay.zombie3Y = lastDay.zombie3Y + 1;
                                lastDay.zombie3X = lastDay.zombie3X + 1;
                                previous = next;
                                PrintZombie3(lastDay);
                               
                            }
                        }
                        if (direction == "up")
                        {
                            char next = maze[lastDay.zombie1Y - 1, lastDay.zombie1X];
                            if (next == '*' || next == '%' || next == '|')
                            {
                                direction = "down";
                            }
                            else if (next == ' ' || next == '.')
                            {
                                EraseZombie(lastDay);
                                lastDay.zombie1Y = lastDay.zombie1Y - 1;
                                previous = next;
                                PrintZombie(lastDay);                               
                            }
                        }
                        if (direction == "down")
                        {
                            char next = maze[lastDay.zombie1Y + 3, lastDay.zombie1X];
                            if (next == '*' || next == '%' || next == '|')
                            {
                                direction = "up";
                            }
                            else if (next == ' ' || next == '.')
                            {
                                EraseZombie(lastDay);
                                lastDay.zombie1Y = lastDay.zombie1Y + 1;
                                previous = next;
                                PrintZombie(lastDay);
                               /* Thread.Sleep(20);*/
                            }
                        }
                        if (direction2 == "left")
                        {
                            char next = maze[lastDay.zombie2Y, lastDay.zombie2X - 1];
                            if (next == '*' || next == '%' || next == '|')
                            {
                                direction2 = "right";
                            }
                            else if (next == ' ' || next == '.')
                            {
                                EraseZombie2(lastDay);
                                lastDay.zombie2X = lastDay.zombie2X - 1;
                                previous = next;
                                PrintZombie2(lastDay);
                            }
                        }
                        if (direction2 == "right")
                        {
                            char next = maze[lastDay.zombie2Y, lastDay.zombie2X + 3];
                            if (next == '*' || next == '%' || next == '|')
                            {
                                direction2 = "left";
                            }
                            else if (next == ' ' || next == '.')
                            {
                                EraseZombie2(lastDay);
                                lastDay.zombie2X = lastDay.zombie2X + 1;
                                previous = next;
                                PrintZombie2(lastDay);
                            }
                        }
                        Thread.Sleep(90);
                        PrintScore(lastDay);
                        if (Keyboard.IsKeyPressed(Key.UpArrow))
                        {
                            /*movePacManUp(maze, ref pacmanX, ref pacmanY);*/
                            MovePlayerUp(maze, lastDay);
                        }
                        if (Keyboard.IsKeyPressed(Key.DownArrow))
                        {
                            MovePlayerDown( maze, lastDay);
                        }
                        if (Keyboard.IsKeyPressed(Key.LeftArrow))
                        {
                            /* movePacManLeft(maze, ref pacmanX, ref pacmanY);*/
                            MovePlayerLeft( maze, lastDay);
                        }
                        if (Keyboard.IsKeyPressed(Key.RightArrow))
                        {
                            /*movePacManRight(maze, ref pacmanX, ref pacmanY);*/
                            MovePlayerRight( maze, lastDay);
                        }
                        if (Keyboard.IsKeyPressed(Key.Escape))
                        {
                            break;
                        }
                        if (Keyboard.IsKeyPressed(Key.R))
                        {                           
                            GenerateBullet(lastDay, ref bulletCount, bulletX, bulletY, isBulletActive);
                        }
                        if (Keyboard.IsKeyPressed(Key.L))
                        {                           
                            GenerateBulletLeft(lastDay, ref bulletLeftCount, isBulletLeftActive,bulletLeftX, bulletLeftY);
                        }
                        MoveBullet(ref bulletCount,  bulletX,  bulletY, isBulletActive, maze);
                        MoveBulletLeft(ref bulletLeftCount,  bulletLeftX,  bulletLeftY,  isBulletLeftActive,  maze);
                        BulletCollisionWithzombie1(ref bulletCount, isBulletActive,  bulletX,  bulletY, lastDay);
                        BulletCollisionWithzombie2(ref bulletCount, isBulletActive, bulletX, bulletY, lastDay);
                        BulletCollisionWithzombie3(ref bulletCount, isBulletActive, bulletX, bulletY, lastDay);
                        BulletLeftCollisionWithzombie1(ref bulletLeftCount, isBulletLeftActive, bulletLeftX, bulletLeftY, lastDay);
                        BulletLeftCollisionWithzombie2(ref bulletLeftCount, isBulletLeftActive, bulletLeftX, bulletLeftY, lastDay);
                        BulletLeftCollisionWithzombie3(ref bulletLeftCount, isBulletLeftActive,  bulletLeftX,bulletLeftY, lastDay);
                        timer++;
                    }

                }
              if(option == 2)
                {                
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Clear();
                    TopHeader();             
                    Console.WriteLine(" KEYS ");
                    Console.WriteLine("-------------------------------------                      ");
                    Console.WriteLine("  1:             UP                             Go UP     ");
                    Console.WriteLine("  2:             DOWN                           Go DOWN     ");
                    Console.WriteLine("  3:             LEFT                           Go LEFT     ");
                    Console.WriteLine("  4:             RIGHT                          Go RIGHT     ");
                    Console.WriteLine("  5:             L                              FIRE USER     ");
                    Console.WriteLine("  6:             R                              FIRE USER     ");
                    Console.WriteLine("  7:             ESC                            END GAME     ");
                    Console.WriteLine("+++++++++ + +      PRESS ANY KEY TO CONTINUE       + + ++++++++");
                    Console.ReadKey();                    
                }
                if (option == 3)
                {

                }
                if (option == 4)
                {
                    
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
                Console.ReadKey();
            }
        }
        static void TopHeader()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(".----------------.  .----------------.  .----------------.       .----------------.  .----------------.  .----------------.  .----------------.   ");
            Console.WriteLine("| .--------------. || .--------------. || .--------------. |     | .--------------. || .--------------. || .--------------. || .--------------. | ");
            Console.WriteLine("| |  _________   | || |  ____  ____  | || |  _________   | |     | |   _____      | || |      __      | || |    _______   | || |  _________   | | ");
            Console.WriteLine("| | |  _   _  |  | || | |_   ||   _| | || | |_   ___  |  | |     | |  |_   _|     | || |     /  \\     | || |   /  ___  |  | || | |  _   _  |  | | ");
            Console.WriteLine("| | |_/ | | \\_|  | || |   | |__| |   | || |   | |_  \\_|  | |     | |    | |       | || |    / /\\ \\    | || |  |  (__ \\_|  | || | |_/ | | \\_|  | | ");
            Console.WriteLine("| |     | |      | || |   |  __  |   | || |   |  _|  _   | |     | |    | |   _   | || |   / ____ \\   | || |   '.___`-.   | || |     | |      | | ");
            Console.WriteLine("| |    _| |_     | || |  _| |  | |_  | || |  _| |___/ |  | |     | |   _| |__/ |  | || | _/ /    \\ \\_ | || |  |`\\____) |  | || |    _| |_     | | ");
            Console.WriteLine("| |   |_____|    | || | |____||____| | || | |_________|  | |     | |  |________|  | || ||____|  |____|| || |  |_______.'  | || |   |_____|    | | ");
            Console.WriteLine("| |              | || |              | || |              | |     | |              | || |              | || |              | || |              | | ");
            Console.WriteLine("| '--------------' || '--------------' || '--------------' |     | '--------------' || '--------------' || '--------------' || '--------------' | ");
            Console.WriteLine("'----------------'  '----------------'  '----------------'       '----------------'  '----------------'  '----------------'  '----------------'  ");
            Console.WriteLine();
            Console.WriteLine("                                           .----------------.  .----------------.  .----------------. ");
            Console.WriteLine("                                          | .--------------. || .--------------. || .--------------. |");
            Console.WriteLine("                                          | |  ________    | || |      __      | || |  ____  ____  | |");
            Console.WriteLine("                                          | | |_   ___ `.  | || |     /  \\     | || | |_  _||_  _| | |");
            Console.WriteLine("                                          | |   | |   `. \\ | || |    / /\\ \\    | || |   \\ \\  / /   | |");
            Console.WriteLine("                                          | |   | |    | | | || |   / ____ \\   | || |    \\ \\/ /    | |");
            Console.WriteLine("                                          | |  _| |___.' / | || | _/ /    \\ \\_ | || |    _|  |_    | |");
            Console.WriteLine("                                          | | |________.'  | || ||____|  |____|| || |   |______|   | |");
            Console.WriteLine("                                          | |              | || |              | || |              | |");
            Console.WriteLine("                                          | '--------------' || '--------------' || '--------------' |");
            Console.WriteLine("                                           '----------------'  '----------------'  '----------------' ");
            Console.WriteLine();
            Console.WriteLine(" .----------------.  .-----------------.     .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  ");
            Console.WriteLine("| .--------------. || .--------------. |    | .--------------. || .--------------. || .--------------. || .--------------. || .--------------. | ");
            Console.WriteLine("| |     ____     | || | ____  _____  | |    | |  _________   | || |      __      | || |  _______     | || |  _________   | || |  ____  ____  | | ");
            Console.WriteLine("| |   .'    `.   | || ||_   \\|_   _| | |    | | |_   ___  |  | || |     /  \\     | || | |_   __ \\    | || | |  _   _  |  | || | |_   ||   _| | | ");
            Console.WriteLine("| |  /  .--.  \\  | || |  |   \\ | |   | |    | |   | |_  \\_|  | || |    / /\\ \\    | || |   | |__) |   | || | |_/ | | \\_|  | || |   | |__| |   | | ");
            Console.WriteLine("| |  | |    | |  | || |  | |\\ \\| |   | |    | |   |  _|  _   | || |   / ____ \\   | || |   |  __ /    | || |     | |      | || |   |  __  |   | | ");
            Console.WriteLine("| |  \\  `--'  /  | || | _| |_\\   |_  | |    | |  _| |___/ |  | || | _/ /    \\ \\_ | || |  _| |  \\ \\_  | || |    _| |_     | || |  _| |  | |_  | | ");
            Console.WriteLine("| |   `.____.'   | || ||_____|\\____| | |    | | |_________|  | || ||____|  |____|| || | |____| |___| | || |   |_____|    | || | |____||____| | | ");
            Console.WriteLine("| |              | || |              | |    | |              | || |              | || |              | || |              | || |              | | ");
            Console.WriteLine("| '--------------' || '--------------' |    | '--------------' || '--------------' || '--------------' || '--------------' || '--------------' | ");
            Console.WriteLine("'----------------'  '----------------'      '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  ");
        }
        static void MovePlayerUp(char[,] maze,LastDay lastDay)
        {            
            char next = maze[lastDay.playerY - 1, lastDay.playerX];
            char next1 = maze[lastDay.playerY - 1, lastDay.playerX + 1];
            char next2 = maze[lastDay.playerY - 1, lastDay.playerX + 2];
            if ((next == ' ' || next == '.') && (next1 == '.' || next1 == ' ') && (next2 == '.' || next2 == ' '))
            {
                ErasePlayer(lastDay);
                lastDay.playerY--;
                PrintPlayer(lastDay);
            }
            if ((next == '.') || (next1 == '.') || (next2 == '.'))
            {
                lastDay.score = lastDay.score + 5;
                lastDay.playerhealth--;
            }
        }
        static void MovePlayerLeft(char[,] maze,LastDay lastDay)
        {
            char next = maze[lastDay.playerY, lastDay.playerX - 1];
            char next1 = maze[lastDay.playerY + 1, lastDay.playerX - 1];
            char next2 = maze[lastDay.playerY + 2, lastDay.playerX - 1];
            if ((next == ' ' || next == '.') && (next1 == '.' || next1 == ' ') && (next2 == '.' || next2 == ' '))
            {
                ErasePlayer(lastDay);
                lastDay.playerX--;
                PrintPlayer(lastDay);
            }
            if ((next == '.') || (next1 == '.') || (next2 == '.'))
            {
                lastDay.score = lastDay.score + 5;
            }
        }
        static void MovePlayerRight(char[,] maze,LastDay lastDay)
        {
            char next = maze[lastDay.playerY, lastDay.playerX + 3];
            char next1 = maze[lastDay.playerY + 1, lastDay.playerX + 3];
            char next2 = maze[lastDay.playerY + 2, lastDay.playerX + 3];
            if ((next == ' ' || next == '.') && (next1 == '.' || next1 == ' ') && (next2 == '.' || next2 == ' '))
            {
                ErasePlayer(lastDay);
                lastDay.playerX++;
                PrintPlayer(lastDay);
            }
            if ((next == '.') || (next1 == '.') || (next2 == '.'))
            {
                lastDay.score = lastDay.score + 5;
            }
        }
        static void MovePlayerDown(char[,] maze,LastDay lastDay)
        {
            char next = maze[lastDay.playerY + 3, lastDay.playerX];
            char next1 = maze[lastDay.playerY + 3, lastDay.playerX + 1];
            char next2 = maze[lastDay.playerY + 3, lastDay.playerX + 2];

            if ((next == ' ' || next == '.') && (next1 == '.' || next1 == ' ') && (next2 == '.' || next2 == ' '))
            {
                ErasePlayer(lastDay);
                lastDay.playerY++;
                PrintPlayer(lastDay);
            }
            if ((next == '.') || (next1 == '.') || (next2 == '.'))
            {
                lastDay.score = lastDay.score + 5;
            }
        }
        static void PrintPlayer(LastDay lastDay)
        {
            int beta = 254;          
            char tank = (char)beta;

            char[] b = { tank, ' ', tank };
            char[] c = { tank, tank, tank };
            char[] d = { tank, tank, tank };           
           
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(lastDay.playerX + i, lastDay.playerY);
                Console.Write(b[i]);
                Console.SetCursorPosition(lastDay.playerX + i, lastDay.playerY + 1);
                Console.Write(c[i]);
                Console.SetCursorPosition(lastDay.playerX + i, lastDay.playerY + 2);
                Console.Write(d[i]);
            }
        }
        static void ErasePlayer(LastDay lastDay)
        {
           
            for(int i = 0;i<3;i++)
            {
                Console.SetCursorPosition(lastDay.playerX + i, lastDay.playerY);
                Console.WriteLine(' ');
                Console.SetCursorPosition(lastDay.playerX + i, lastDay.playerY + 1);
                Console.WriteLine(' ');
                Console.SetCursorPosition(lastDay.playerX + i, lastDay.playerY + 2);
                Console.WriteLine(' ');
            }
        }
        static void PrintZombie(LastDay lastDay)
        {
            int a = 30;
            int b = 31;
            int c = 475;
            char up = (char)a;
            char down = (char)b;
            char block = (char)c;
            if (lastDay.zombie1health != 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    Console.SetCursorPosition(lastDay.zombie1X, lastDay.zombie1Y);
                    Console.WriteLine(up);
                    Console.SetCursorPosition(lastDay.zombie1X, lastDay.zombie1Y + 1);
                    Console.WriteLine(block);
                    Console.SetCursorPosition(lastDay.zombie1X, lastDay.zombie1Y + 2);
                    Console.WriteLine(down);
                }
            }
        }
        static void EraseZombie(LastDay lastDay)
        { 
            for (int i = 0; i < 1; i++)
            {
                Console.SetCursorPosition(lastDay.zombie1X, lastDay.zombie1Y);
                Console.WriteLine(' ');
                Console.SetCursorPosition(lastDay.zombie1X, lastDay.zombie1Y + 1);
                Console.WriteLine(' ');
                Console.SetCursorPosition(lastDay.zombie1X, lastDay.zombie1Y + 2);
                Console.WriteLine(' ');
            }
        }
        static void PrintZombie2(LastDay lastDay)
        { 
            int a = 273;
            int c = 272;
            char left = (char)a;
            char right = (char)c;

            if (lastDay.zombie2health != 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    Console.SetCursorPosition(lastDay.zombie2X, lastDay.zombie2Y);
                    Console.Write(left);
                    Console.SetCursorPosition(lastDay.zombie2X + 1, lastDay.zombie2Y);
                    Console.Write(left);
                    Console.SetCursorPosition(lastDay.zombie2X + 2, lastDay.zombie2Y);
                    Console.Write(right);
                    Console.SetCursorPosition(lastDay.zombie2X + 3, lastDay.zombie2Y);
                    Console.Write(right);
                }
            }
        }
        static void EraseZombie2(LastDay lastDay)
        {
            for (int i = 0; i < 1; i++)
            {
                Console.SetCursorPosition(lastDay.zombie2X, lastDay.zombie2Y);
                Console.Write(' ');
                Console.SetCursorPosition(lastDay.zombie2X + 1, lastDay.zombie2Y);
                Console.Write(' ');
                Console.SetCursorPosition(lastDay.zombie2X + 2, lastDay.zombie2Y);
                Console.Write(' ');
                Console.SetCursorPosition(lastDay.zombie2X + 3, lastDay.zombie2Y);
                Console.Write(' ');
            }
        }
        static void PrintZombie3(LastDay lastDay)
        {
            int a = 286;
            int b = 254;
            int c = 273;
            int d = 272;
            int e = 287;
            char up = (char)a;
            char box = (char)b;
            char left = (char)c;
            char right = (char)d;
            char down = (char)e;

            if (lastDay.zombie3health != 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    Console.SetCursorPosition(lastDay.zombie3X + 2, lastDay.zombie3Y);
                    Console.Write(up);
                    Console.SetCursorPosition(lastDay.zombie3X, lastDay.zombie3Y + 1);
                    Console.Write(left);
                    Console.SetCursorPosition(lastDay.zombie3X + 2, lastDay.zombie3Y + 1);
                    Console.Write(box);
                    Console.SetCursorPosition(lastDay.zombie3X + 4, lastDay.zombie3Y + 1);
                    Console.Write(right);
                    Console.SetCursorPosition(lastDay.zombie3X + 2, lastDay.zombie3Y + 2);
                    Console.Write(down);
                }
            }
        }
        static void EraseZombie3(LastDay lastDay)
        {  
            for (int i = 0; i < 1; i++)
            {
                Console.SetCursorPosition(lastDay.zombie3X + 2, lastDay.zombie3Y);
                Console.Write(' ');
                Console.SetCursorPosition(lastDay.zombie3X, lastDay.zombie3Y + 1);
                Console.Write(' ');
                Console.SetCursorPosition(lastDay.zombie3X + 2, lastDay.zombie3Y + 1);
                Console.Write(' ');
                Console.SetCursorPosition(lastDay.zombie3X + 4, lastDay.zombie3Y + 1);
                Console.Write(' ');
                Console.SetCursorPosition(lastDay.zombie3X + 2, lastDay.zombie3Y + 2);
                Console.Write(' ');
            }
        }
        static void GenerateBullet(LastDay lastDay, ref int bulletCount,int[] bulletX,int[] bulletY,bool[] isBulletActive)
        {
            bulletX[bulletCount] = lastDay.playerX + 4;
            bulletY[bulletCount] = lastDay.playerY;
            isBulletActive[bulletCount] = true;
            Console.SetCursorPosition(lastDay.playerX + 4, lastDay.playerY);
            Console.Write("-");
            bulletCount++;
        }
        static void GenerateBulletLeft(LastDay lastDay, ref int bulletLeftCount,bool[] isBulletLeftActive,int[] bulletLeftX,int[] bulletLeftY)
        {
            bulletLeftX[bulletLeftCount] = lastDay.playerX - 2;
            bulletLeftY[bulletLeftCount] = lastDay.playerY;
            isBulletLeftActive[bulletLeftCount] = true;
            Console.SetCursorPosition(lastDay.playerX - 2, lastDay.playerY);
            Console.Write("-");
            bulletLeftCount++;
        }
        static void MakeBulletLeftInactive(ref int index, bool[] isBulletLeftActive)
        {
            isBulletLeftActive[index] = false;
        }
        static void MoveBulletLeft(ref int bulletLeftCount, int[] bulletLeftX, int[] bulletLeftY, bool[] isBulletLeftActive, char[,] maze)
        {
            for (int x = 0; x < bulletLeftCount; x++)
            {
                char next = maze[bulletLeftY[x], bulletLeftX[x] - 1]; 
                if (next != ' ')
                {
                    EraseLeftBullet(bulletLeftX[x], bulletLeftY[x]);
                    MakeBulletLeftInactive(ref x,  isBulletLeftActive);
                }
                else
                {
                    EraseLeftBullet(bulletLeftX[x], bulletLeftY[x]);
                    bulletLeftX[x] = bulletLeftX[x] - 1;
                    PrintLeftBullet(ref bulletLeftX[x],ref bulletLeftY[x]);
                }
            }
        }
        static void PrintLeftBullet(ref int x,ref int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("-");
        }
        static void EraseLeftBullet(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }
        static void PrintBullet(ref int x,ref int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("-");
        }
        static void EraseBullet(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }
        static void MakeBulletInactive(ref int index, bool[] isBulletActive)
        {
            isBulletActive[index] = false;
        }       
        static void MoveBullet(ref int bulletCount, int[] bulletX, int[] bulletY, bool[] isBulletActive, char[,] maze)
        {
            for (int x = 0; x < bulletCount; x++)
            {
                char next = maze[bulletY[x], bulletX[x] + 1];
                if (next != ' ')
                {
                    EraseBullet(bulletX[x], bulletY[x]);
                    MakeBulletInactive(ref x, isBulletActive);
                }
                else
                {
                    EraseBullet(bulletX[x], bulletY[x]);
                    bulletX[x] = bulletX[x] + 1;
                    PrintBullet(ref bulletX[x], ref bulletY[x]);
                }
            }
        }
        static void BulletCollisionWithzombie1(ref int bulletCount,bool[] isBulletActive,int[] bulletX,int[] bulletY,LastDay lastDay)
        {
            for (int x = 0; x < bulletCount; x++)
            {
                if (isBulletActive[x] == true)
                {
                    if (bulletX[x] - 1 == lastDay.zombie1X && (bulletY[x] == lastDay.zombie1Y || bulletY[x] == lastDay.zombie1Y + 2 || bulletY[x] == lastDay.zombie1Y + 3))
                    {
                        AddScore(ref lastDay.score);
                        lastDay.zombie1health--;
                        if (lastDay.zombie1health < 0)
                        {
                            lastDay.zombie1health = 0;
                            if (lastDay.zombie1health == 0)
                            {
                                EraseZombie(lastDay);
                            }
                        }
                    }
                    if (lastDay.zombie1X - 1 == bulletX[x] && lastDay.zombie1Y + 1 == bulletY[x])
                    {
                        AddScore(ref lastDay.score);
                        lastDay.zombie1health--;
                        if (lastDay.zombie1health < 0)
                        {
                            lastDay.zombie1health = 0;
                            if (lastDay.zombie1health == 0)
                            {
                                EraseZombie(lastDay);
                            }
                        }
                    }
                }
            }
        }
        static void BulletLeftCollisionWithzombie1(ref int bulletLeftCount, bool[] isBulletLeftActive, int[] bulletLeftX, int[] bulletLeftY, LastDay lastDay)
        {
            for (int x = 0; x < bulletLeftCount; x++)
            {
                if (isBulletLeftActive[x] == true)
                {
                    if (bulletLeftX[x] + 1 == lastDay.zombie1X && (bulletLeftY[x] == lastDay.zombie1Y || bulletLeftY[x] == lastDay.zombie1Y + 2 || bulletLeftY[x] == lastDay.zombie1Y + 3))
                    {
                        AddScore(ref lastDay.score);
                        lastDay.zombie1health--;
                        if (lastDay.zombie1health < 0)
                        {
                            lastDay.zombie1health = 0;
                            if (lastDay.zombie1health == 0)
                            {
                                EraseZombie(lastDay);
                            }
                        }
                    }
                    if (lastDay.zombie1X - 1 == bulletLeftX[x] && lastDay.zombie1Y + 1 == bulletLeftY[x])
                    {
                        AddScore(ref lastDay.score);
                        lastDay.zombie1health--;
                        if (lastDay.zombie1health < 0)
                        {
                            lastDay.zombie1health = 0;
                            if (lastDay.zombie1health == 0)
                            {
                                EraseZombie(lastDay);
                            }
                        }
                    }
                }
            }
        }
        static void BulletCollisionWithzombie2(ref int bulletCount, bool[] isBulletActive, int[] bulletX, int[] bulletY,  LastDay lastDay)
        {
            for (int x = 0; x < bulletCount; x++)
            {
                if (isBulletActive[x] == true)
                {
                    if (bulletX[x] + 1 == lastDay.zombie2X && (bulletY[x] == lastDay.zombie2Y))
                    {
                        AddScore(ref lastDay.score);
                        lastDay.zombie2health--;
                        if (lastDay.zombie2health < 0)
                        {
                            lastDay.zombie2health = 0;
                            if (lastDay.zombie2health == 0)
                            {
                                EraseZombie2(lastDay);
                            }
                        }
                    }
                    if (lastDay.zombie2X + 1 == bulletX[x] && lastDay.zombie2Y - 1 == bulletY[x])
                    {
                        AddScore(ref lastDay.score);
                        lastDay.zombie2health--;
                        if (lastDay.zombie2health < 0)
                        {
                            lastDay.zombie2health = 0;
                            if (lastDay.zombie2health == 0)
                            {
                                EraseZombie2(lastDay);
                            }
                        }
                    }
                }
            }
        }
        static void BulletLeftCollisionWithzombie2(ref int bulletLeftCount, bool[] isBulletLeftActive, int[] bulletLeftX, int[] bulletLeftY, LastDay lastDay)
        {
            for (int x = 0; x < bulletLeftCount; x++)
            {
                if (isBulletLeftActive[x] == true)
                {
                    if (bulletLeftX[x] - 1 == lastDay.zombie2X && (bulletLeftY[x] == lastDay.zombie2Y))
                    {
                        AddScore(ref lastDay.score);
                        lastDay.zombie2health--;
                        if (lastDay.zombie2health < 0)
                        {
                            lastDay.zombie2health = 0;
                            if (lastDay.zombie2health == 0)
                            {
                                EraseZombie2(lastDay);
                            }
                        }
                    }
                    if (lastDay.zombie2X + 1 == bulletLeftX[x] && lastDay.zombie2Y - 1 == bulletLeftY[x])
                    {
                        AddScore(ref lastDay.score);
                        lastDay.zombie2health--;
                        if (lastDay.zombie2health < 0)
                        {
                            lastDay.zombie2health = 0;
                            if (lastDay.zombie2health == 0)
                            {
                                EraseZombie2(lastDay);
                            }
                        }
                    }
                }
            }
        }
        static void BulletCollisionWithzombie3(ref int bulletCount, bool[] isBulletActive, int[] bulletX, int[] bulletY, LastDay lastDay)
        {
            for (int x = 0; x < bulletCount; x++)
            {
                if (isBulletActive[x] == true)
                {
                    if (bulletX[x] + 1 == lastDay.zombie3X && (bulletY[x] == lastDay.zombie3Y))
                    {
                        AddScore(ref lastDay.score);
                        lastDay.zombie3health--;
                        if (lastDay.zombie3health < 0)
                        {
                            lastDay.zombie3health = 0;
                            if (lastDay.zombie3health == 0)
                            {
                                EraseZombie3(lastDay);
                            }
                        }
                    }
                    if (lastDay.zombie3X - 1 == bulletX[x] && lastDay.zombie3Y + 1 == bulletY[x])
                    {
                        AddScore(ref lastDay.score);
                        lastDay.zombie3health--;

                        if (lastDay.zombie3health < 0)
                        {
                            lastDay.zombie3health = 0;
                            if (lastDay.zombie3health == 0)
                            {
                                EraseZombie3(lastDay);
                            }
                        }
                    }
                }
            }
        }
        static void BulletLeftCollisionWithzombie3(ref int bulletLeftCount, bool[] isBulletLeftActive, int[] bulletLeftX, int[] bulletLeftY, LastDay lastDay)
        {
            for (int x = 0; x < bulletLeftCount; x++)
            {
                if (isBulletLeftActive[x] == true)
                {
                    if (bulletLeftX[x] - 1 == lastDay.zombie3X && (bulletLeftY[x] == lastDay.zombie3Y || bulletLeftY[x] == lastDay.zombie3Y + 2 || bulletLeftY[x] == lastDay.zombie3Y + 3))
                    {
                        AddScore(ref lastDay.score);
                        lastDay.zombie3health--;
                        if (lastDay.zombie3health < 0)
                        {
                            lastDay.zombie3health = 0;
                            if (lastDay.zombie3health == 0)
                            {
                                EraseZombie3(lastDay);
                            }
                        }
                    }
                    if (lastDay.zombie3X - 1 == bulletLeftX[x] && lastDay.zombie3Y + 1 == bulletLeftY[x])
                    {
                        AddScore(ref lastDay.score);
                        lastDay.zombie3health--;

                        if (lastDay.zombie3health < 0)
                        {
                            lastDay.zombie3health = 0;
                            if (lastDay.zombie3health == 0)
                            {
                                EraseZombie3(lastDay);
                            }
                        }
                    }
                }
            }
        }

        static void AddScore(ref int score)
        {
            score++;
        }
        static void PrintScore(LastDay lastDay)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(135, 10);
            Console.WriteLine("******************************");
            Console.SetCursorPosition(135, 11);
            Console.WriteLine("*     SCORE:  " + lastDay.score);
            Console.SetCursorPosition(135, 12);
            Console.WriteLine("1st Zombie health: " + lastDay.zombie1health);
            Console.SetCursorPosition(135, 13);
            Console.WriteLine("2nd Zombie health: " + lastDay.zombie2health);
            Console.SetCursorPosition(135, 14);
            Console.WriteLine("3rd Zombie health: " + lastDay.zombie3health);
            Console.SetCursorPosition(135, 15);
            Console.WriteLine("Player Health:  " + lastDay.playerhealth);
            Console.SetCursorPosition(135, 16);
            Console.WriteLine("******************************");
            Console.SetCursorPosition(135, 21);
            Console.WriteLine(" CONTROLS");
            Console.SetCursorPosition(135, 22);
            Console.WriteLine("-----------");
            Console.SetCursorPosition(135, 23);
            Console.WriteLine(" Up        Key    -   Go Up");
            Console.SetCursorPosition(135, 24);
            Console.WriteLine(" Down      Key    -   Go Down");
            Console.SetCursorPosition(135, 25);
            Console.WriteLine(" Left      Key    -   Go Left");
            Console.SetCursorPosition(135, 26);
            Console.WriteLine(" Right     Key    -   Go Right");
            Console.SetCursorPosition(135, 27);
            Console.WriteLine(" Escape    Key    -   Exit");
            Console.SetCursorPosition(135, 28);
            Console.WriteLine(" Press R to Shoot Right");
            Console.SetCursorPosition(135, 29);
            Console.WriteLine(" Press L to Shoot Right");
        }
        static void PrintMaze(char[,] maze)
        {
            Console.Clear();
            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    Console.Write(maze[x, y]);
                }
                Console.WriteLine();              
            }           
        }
        static void ReadData(char[,] maze)
        {
            StreamReader fp = new StreamReader("maze.txt");
            string record;
            int row = 0;
            while ((record = fp.ReadLine()) != null)
            {
                for (int x = 0; x < 132; x++)
                {
                    maze[row, x] = record[x];
                }
                row++;
            }

            fp.Close();
        }

    }
    
}
