using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Snake
{
    enum Condition { Normal, Over, Clear, GotNode }
    class Program
    {
        static Program instance;
        Queue<int> px = new Queue<int> { };
        Queue<int> py = new Queue<int> { };

        int fx;
        int fy;
        int[] eatable = new int[] { };
        int[] past = new int[] { };
        char[,] field = new char[20, 20];
        int randomX;
        int randomY;
        static void Main(string[] args)
        {
            instance = new Program();
            instance.Init();
            instance.Game();
        }
        void Init()
        {
            fx = 0;
            fy = 1;
            px.Enqueue(9);
            py.Enqueue(9);
            field = FieldInit(field);
            Random random = new Random();
            randomX = random.Next(1, 18);
            randomY = random.Next(1, 18);
            field[randomX, randomY] = '@';
            DisplayWindow("Press any key", "..to start");
            Console.ReadKey();
        }
        void Game()
        {
            //MakeNewNode();

            while (true)
            {
                GetKey(ref fx, ref fy);
                px.Enqueue(px.Last() + fx);
                py.Enqueue(py.Last() + fy);
                field[py.Dequeue(), px.Dequeue()] = ' ';
                //Check game over condition
                switch (CheckCondition(px.Last(), py.Last()))
                {
                    case Condition.Normal:
                        break;
                    case Condition.GotNode:
                        Console.WriteLine("Got Node!");
                        Thread.Sleep(500);
                        break;
                    case Condition.Clear:
                    case Condition.Over:
                        return;
                }
                for (int k = 0; k < py.Count(); k++)
                {
                    field[py.ElementAt(k), px.ElementAt(k)] = '#';
                }
                DisplayWindow();
                Thread.Sleep(250);
            }
        }
        Condition CheckCondition(int x, int y)
        {
            if (px.Count == 324)
            {
                Terminate(Condition.Clear);
                return Condition.Clear;
            }
            else if (px.Last() == 19 || px.Last() == 0 || py.Last() == 0 || py.Last() == 19)
            {
                Terminate(Condition.Over);
                return Condition.Over;
            }
            else
            {
                switch (field[y, x])
                {
                    case '@':
                        MakeNewNode();
                        return Condition.GotNode;
                    case '#':
                        Terminate(Condition.Over);
                        return Condition.Over;
                }
            }
            return Condition.Normal;
        }
        void DisplayWindow(params string[] str)
        {
            Console.Clear();
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {

                    Console.Write(field[i, j]);
                }
                Console.WriteLine();
                if (i == 8)
                {
                    for (int k = 0; k < str.Length; k++)
                    {
                        int padSize = (18 - str[k].Length) / 2;
                        str[k] = str[k].PadLeft(18 - padSize);
                        str[k] = str[k].PadRight(18);
                        Console.WriteLine("#" + str[k] + "#");
                        i++;
                    }
                }
            }
        }
        void DisplayWindow()
        {
            Console.Clear();
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {

                    Console.Write(field[i, j]);

                }
                Console.WriteLine();
            }
        }
        void Terminate(Condition isClear)
        {
            if (isClear == Condition.Clear)
            {
                Console.Clear();
                DisplayWindow("Congratulation!","***YOU WON***",  "Press Any Key");
                Console.ReadKey();
            }
            else if (isClear == Condition.Over)
            {
                Console.Clear();
                DisplayWindow("Game Over", "Press Any Key");
                Console.ReadKey();
                return;
            }
        }
        void GetKey(ref int x, ref int y)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo a = Console.ReadKey(true);
                while (Console.KeyAvailable)
                {
                    a = Console.ReadKey(true);
                }
                switch (a.Key)
                {
                    case ConsoleKey.UpArrow:
                        x = 0;
                        y = -1;
                        break;
                    case ConsoleKey.DownArrow:
                        x = 0;
                        y = 1;
                        break;
                    case ConsoleKey.RightArrow:
                        x = 1;
                        y = 0;
                        break;
                    case ConsoleKey.LeftArrow:
                        x = -1;
                        y = 0;
                        break;
                }

            }
        }
        char[,] FieldInit(char[,] field)
        {

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if ((i == 0) || (i == 19) || (j == 0) || (j == 19))
                    {
                        field[i, j] = '#';
                    }
                    else field[i, j] = ' ';
                }
            }
            return field;
        }
        void MakeNewNode()
        {
            px.Enqueue(px.Last());
            py.Enqueue(py.Last());
            do
            {
                Random random = new Random();
                randomX = random.Next(1, 18);
                randomY = random.Next(1, 18);
            }
            while (field[randomX, randomY] == '#');
            field[randomX, randomY] = '@';
        }
    }
    class Functions
    {

    }
}
