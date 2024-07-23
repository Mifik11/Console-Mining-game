using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Zaverecna_prace
{
    class Start
    {
        public string name;
        public static string score = "0";
        public Start (string name)
        {
            this.name = name;
        }
        public void create()
        {
            DateTime today = DateTime.Now;
            string[] file = new string[] { this.name, score, today.Year.ToString(), today.Month.ToString(), today.Day.ToString() };
            File.WriteAllLines(this.name+".txt",file);
        }     
    }
    class Load
    {
        
    }
    class Mine
    {
        public int osa_x = 0;
        public int osa_y = 0;
        public int score = 0;
        public int step = 100;
        static int[,] array = new int[20, 75];
        public Mine(int osa_x, int osa_y, int score, int step)
        {
            this.osa_x = osa_x;
            this.osa_y = osa_y;
            this.score = score;
            this.step = step;
        }
        public void draw_gold() //vykreslování zlata
        {
            Random random = new Random();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int d = 0; d < array.GetLength(1); d++)
                {
                    array[i, d] = 1;
                }
            }
            for (int i = 0; i < 15; i++)
            {
                array[random.Next(10, 20), random.Next(0, 75)] = 2;
            }
        }
        public void draw_mine() //vykreslování mapy
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int d = 0; d < array.GetLength(1); d++)
                {
                    if ((i == this.osa_y) && (d == this.osa_x))
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(" ");
                        Console.ResetColor();
                    }
                    else if (i == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write(" ");
                        Console.ResetColor();
                        array[i, d] = 0;
                    }
                    else if (array[i, d] == 2)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write(" ");
                        Console.ResetColor();
                    }
                    else if(array[i,d]==1)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write(" ");
                        Console.ResetColor();
                    }                    
                }
                Console.WriteLine();
            }
        }
        public void points()
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int d = 0; d < array.GetLength(1); d++)
                {
                    if ((i == this.osa_y && d == this.osa_x) && ((array[i, d] == 2)))
                    {
                        this.score += 5;
                        this.step -= 1;
                        array[i, d] = 1;
                    }
                    else if ((i == this.osa_y && d == this.osa_x) && ((array[i, d] == 1)))
                    {
                        this.step -= 1;
                    }
                }
            }
        }
    }
    class Town
    {
        public int osa_x = 0;
        public string[] town_array = new string[20];
        public void town()
        {
            for (int i = 0; i < town_array.Length; i++)
            {
                if (i == this.osa_x)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write(" ");
                    Console.ResetColor();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(" ");
                    Console.ResetColor();
                }
            }
        }
        public Town(int osa_x_t)
        {
            this.osa_x = osa_x_t;
        }
    }
    //class Shop : Town
    //{

    //}
    class Program
    {
        static void potvrzeni()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("~~~~~~~~~~~~~~~\n~~~POTVRZENÍ~~~\n~~~~~~~~~~~~~~~\n");
            Console.ResetColor();
            Console.WriteLine("napište ano/ne: \n");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("počítá se pouze odpověď ano! při zadání jiné hodnoty program vas vráti do menu");
            Console.ResetColor();
            Console.CursorTop = 4;
            Console.CursorLeft = 17;
        }
        static void okraj()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < 75; i++)
            {
                Console.CursorTop = 20;
                Console.Write("-");
            }
            Console.Write("+");
            for (int i = 0; i < 20; i++)
            {
                Console.CursorTop = i;
                Console.CursorLeft = 75;
                Console.WriteLine("|");
            }
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            Console.ResetColor();
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            string answer;
            bool start = true;
            while (start)
            {
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                okraj();
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("~~~DOLY~~~\n");
                Console.ResetColor();
                Console.WriteLine(" New Game\n Load Game\n Exit");
                Console.CursorTop = 21;
                Console.WriteLine("s - new game\nl - load game\nESC - exit");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                ConsoleKey key = keyInfo.Key;
                if (key == ConsoleKey.S)
                {
                    okraj();
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("~~~~~~~~~~~~~~~\n~~~NOVÁ  HRA~~~\n~~~~~~~~~~~~~~~\n");
                    Console.ResetColor();
                    Console.Write("Napište název světa: ");
                    string name = Console.ReadLine();
                    Start start1 = new Start(name);
                    start1.create();
                    int osa_x_t = 0;
                    int osa_x = 0;
                    int osa_y = 0;
                    int score = 0;
                    int step = 100;
                    int control_mine = 1;
                    bool start2 = true;
                    while (start2)
                    {
                        Mine Mine = new Mine(osa_x, osa_y, score, step);
                        okraj();
                        if (osa_x_t >= 20)
                        {
                            if (osa_x > 74)
                            {
                                Mine.osa_x = 74;
                                osa_x = 74;
                            }
                            else if (osa_x < 0)
                            {
                                Mine.osa_x = 0;
                                osa_x = 0;
                            }
                            else if (osa_y > 19)
                            {
                                Mine.osa_y = 19;
                                osa_y = 19;
                            }
                            else if (osa_y < 0)
                            {
                                Mine.osa_y = 0;
                                osa_y = 0;
                            }
                            if (control_mine == 1)
                            {
                                Mine.draw_gold();
                                control_mine = 0;
                            }
                            Mine.points();
                            Mine.draw_mine();
                            Console.CursorTop = 22;
                            Console.CursorLeft = 0;
                            Console.WriteLine(" ← pohyb do leva\n → pohyb do prava\n ↑ pohyb na horu\n ↓ pohyb dolu\n ESC - exit");
                            Console.CursorTop = 1;
                            Console.CursorLeft = 77;
                            score = Mine.score;
                            Console.Write("score {0}", score);
                            Console.CursorTop = 2;
                            Console.CursorLeft = 77;
                            step = Mine.step;
                            Console.Write("kroky {0}", step);
                            ConsoleKeyInfo KeyInfo = Console.ReadKey();
                            ConsoleKey Key = KeyInfo.Key;
                            DateTime date = DateTime.Now;
                            string[] file = new string[] { name, score.ToString(), date.Year.ToString(), date.Month.ToString(), date.Day.ToString() };
                            if (Key == ConsoleKey.LeftArrow)
                                osa_x--;
                            else if (Key == ConsoleKey.RightArrow)
                                osa_x++;
                            else if (Key == ConsoleKey.UpArrow)
                                osa_y--;
                            else if (Key == ConsoleKey.DownArrow)
                                osa_y++;
                            else if (Key == ConsoleKey.Escape)
                            {
                                File.WriteAllLines(name+".txt",file);
                                start2 = false;
                            }
                            else if (Key == ConsoleKey.E)
                                if (osa_y == 0)
                                {
                                    osa_x_t = 19;
                                    control_mine = 1;
                                }
                        }
                        else
                        {
                            step = 100;
                            if (osa_x_t < 0)
                                osa_x_t = 0;
                            else if (osa_x_t > 19)
                                osa_x = 0;
                            Town town = new Town(osa_x_t);
                            town.town();
                            Console.CursorTop = 22;
                            Console.CursorLeft = 0;
                            Console.WriteLine(" ← do leva\n → do prava\n ESC - exit");
                            ConsoleKeyInfo Keyinfo = Console.ReadKey();
                            ConsoleKey Key2 = Keyinfo.Key;
                            if (Key2 == ConsoleKey.LeftArrow)
                                osa_x_t--;
                            else if (Key2 == ConsoleKey.RightArrow)
                                osa_x_t++;
                            else if (Key2 == ConsoleKey.Escape)
                                start2 = false;
                        }
                        if (step == 0)
                        {
                            File.Delete(name);
                            start2 = false;
                        }
                    }
                }   
                else if (key == ConsoleKey.L)
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("+---------------+\n|  NAČTENÍ HRY  |\n+---------------+\n");
                    Console.ResetColor();
                    Console.Write("Napište název světa: ");
                    string txt_exist = Console.ReadLine();
                    if (File.Exists(txt_exist+".txt"))
                    {
                        string[] load = File.ReadAllLines(txt_exist+".txt");
                        string name = load[0];
                        int osa_x_t = 0;
                        int osa_x = 0;
                        int osa_y = 0;
                        int score = Convert.ToInt32(load[1]);
                        int step = 100;
                        int control_mine = 1;
                        bool start2 = true;
                        while (start2)
                        {
                            Mine Mine = new Mine(osa_x, osa_y, score, step);
                            okraj();
                            if (osa_x_t >= 20)
                            {
                                if (osa_x > 74)
                                {
                                    Mine.osa_x = 74;
                                    osa_x = 74;
                                }
                                else if (osa_x < 0)
                                {
                                    Mine.osa_x = 0;
                                    osa_x = 0;
                                }
                                else if (osa_y > 19)
                                {
                                    Mine.osa_y = 19;
                                    osa_y = 19;
                                }
                                else if (osa_y < 0)
                                {
                                    Mine.osa_y = 0;
                                    osa_y = 0;
                                }
                                if (control_mine == 1)
                                {
                                    Mine.draw_gold();
                                    control_mine = 0;
                                }
                                Mine.points();
                                Mine.draw_mine();
                                Console.CursorTop = 22;
                                Console.CursorLeft = 0;
                                Console.WriteLine(" ← pohyb do leva\n → pohyb do prava\n ↑ pohyb na horu\n ↓ pohyb dolu\n E - odejít (pouze v zelené zóně)\n ESC - exit");
                                Console.CursorTop = 1;
                                Console.CursorLeft = 77;
                                score = Mine.score;
                                Console.Write("score {0}", score);
                                Console.CursorTop = 2;
                                Console.CursorLeft = 77;
                                step = Mine.step;
                                Console.Write("kroky {0}", step);
                                ConsoleKeyInfo KeyInfo = Console.ReadKey();
                                ConsoleKey Key = KeyInfo.Key;
                                DateTime date = DateTime.Now;
                                string[] file = new string[] { name, score.ToString(), date.Year.ToString(), date.Month.ToString(), date.Day.ToString() };
                                if (Key == ConsoleKey.LeftArrow)
                                    osa_x--;
                                else if (Key == ConsoleKey.RightArrow)
                                    osa_x++;
                                else if (Key == ConsoleKey.UpArrow)
                                    osa_y--;
                                else if (Key == ConsoleKey.DownArrow)
                                    osa_y++;
                                else if (Key == ConsoleKey.Escape)
                                {
                                    File.WriteAllLines(name + ".txt", file);
                                    start2 = false;
                                }
                                else if (Key == ConsoleKey.E)
                                    if (osa_y == 0)
                                    {
                                        osa_x_t = 19;
                                        control_mine = 1;
                                    }
                            }
                            else
                            {
                                step = 100;
                                if (osa_x_t < 0)
                                    osa_x_t = 0;
                                else if (osa_x_t > 19)
                                    osa_x = 0;
                                Town town = new Town(osa_x_t);
                                town.town();
                                Console.CursorTop = 22;
                                Console.CursorLeft = 0;
                                Console.WriteLine(" ← do leva\n → do prava\n ESC - exit");
                                ConsoleKeyInfo Keyinfo = Console.ReadKey();
                                ConsoleKey Key2 = Keyinfo.Key;
                                if (Key2 == ConsoleKey.LeftArrow)
                                    osa_x_t--;
                                else if (Key2 == ConsoleKey.RightArrow)
                                    osa_x_t++;
                                else if (Key2 == ConsoleKey.Escape)
                                    start2 = false;
                            }
                            if (step == 0)
                            {
                                File.Delete(name);
                                start2 = false;
                            }
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("soubor ne existuje!");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                }            
                else if (key == ConsoleKey.Escape)
                {
                    potvrzeni();
                    answer = Console.ReadLine();
                    if (answer == "ano")                    
                        start = false;                   
                    else
                        Console.Clear();
                }
            }
        }
    }
}
