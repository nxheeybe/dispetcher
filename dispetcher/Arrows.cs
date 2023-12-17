using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dispetcher
{
    internal class Arrows
    {
        private static ConsoleKeyInfo key = Console.ReadKey();
        public static int max = 0;
        private static int min = 4;
        public static int pozition = 0;
        public static void Arrow()
        {
            key = Console.ReadKey();
            pozition = 0;
            while (key.Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.UpArrow)
                {
                    pozition--;
                    if (pozition < min)
                    {
                        pozition = max;
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    pozition++;
                    if (pozition > max)
                    {
                        pozition = min;
                    }
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                Console.SetCursorPosition(0, pozition - 1);
                Console.WriteLine("   ");
                Console.SetCursorPosition(0, pozition);
                Console.WriteLine("==>");
                Console.SetCursorPosition(0, pozition + 1);
                Console.WriteLine("   ");
                key = Console.ReadKey();
            }
            pozition -= 4;
            Console.Clear();
        }
    }

}