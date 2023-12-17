using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace dispetcher
{
    internal class ProcessUs
    {
        public static Process[] process = Process.GetProcesses().OrderBy(x => x.ProcessName).ToArray();
        public static void Menu(int pozition = 0)
        {
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("Диспетчер");
            Console.WriteLine("-------------------------------------");
            Console.SetCursorPosition(4, 2);
            Console.WriteLine("Название");
            Console.SetCursorPosition(45, 2);
            Console.WriteLine("| Приоритет");
            Console.SetCursorPosition(56, 2);
            Console.WriteLine("| Память");
            Console.SetCursorPosition(70, 2);
            Console.WriteLine("| Диск");
            Console.WriteLine("-------------------------------------");
            if (pozition == 0)
            {
                for (int i = 0; i < Process.GetProcesses().Length; i++)
                {
                    /*Process[] process = Process.GetProcesses().OrderBy(x => x.ProcessName).ToArray();*/
                    Console.SetCursorPosition(4, 4 + i);
                    Console.WriteLine (process[i].ProcessName);
                    Console.SetCursorPosition(45, 4 + i);
                    Console.WriteLine($"|{process[i].BasePriority}");
                    Console.SetCursorPosition(56, 4 + i);
                    Console.WriteLine($"|{Math.Round(process[i].PagedMemorySize64 / Math.Pow(2, 20), 1)}, МБ");
                    Console.SetCursorPosition(70, 4 + i);
                    Console.WriteLine($"|{Math.Round(process[i].WorkingSet64 / Math.Pow(2, 20), 1)}, МБ");
                    Arrows.max = i + 2;
                }
            }
            else
            {
                DopMenu(pozition);
            }
        }
        public static void DopMenu(int pozition = 0)
        {
            Console.Clear();
            try
            {
                Process myProcess = process[pozition];
                while (true)
                {

                    if (!myProcess.HasExited)
                    {
                        myProcess.Refresh();
                        Console.WriteLine($"{myProcess} -");
                        Console.WriteLine("-------------------------------------------------------------------");
                        Console.WriteLine($"  Использование диска                 : {Math.Round(myProcess.WorkingSet64 / Math.Pow(2, 20), 1)}, МБ");
                        Console.WriteLine($"  Приоритет                           : {myProcess.BasePriority}");
                        Console.WriteLine($"  Класс приоритета                    : {myProcess.PriorityClass}");
                        Console.WriteLine($"  Время использования процесса        : {myProcess.UserProcessorTime}");
                        Console.WriteLine($"  Все время использования             : {myProcess.TotalProcessorTime}");
                        Console.WriteLine($"  Использование оперативной памяти    : {Math.Round(myProcess.PagedMemorySize64 / Math.Pow(2, 20), 1)}, МБ");

                        if (myProcess.Responding)
                        {
                            Console.WriteLine("-------------------------------------------------------------------");
                            Console.WriteLine("Статус = Запущено");
                            Console.WriteLine("-------------------------------------------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("-------------------------------------------------------------------");
                            Console.WriteLine("Статус = Незапущено");
                            Console.WriteLine("-------------------------------------------------------------------");
                        }
                        Console.WriteLine("     Нажмите D  чтобы остановить текуший процесс");
                        Console.WriteLine("     Нажмите DELETE  чтоюы остановить все процессы с данным именем");
                        Console.WriteLine("     Нажмите BACKSPACE чтобы вернуться к списку процессов");
                        ConsoleKeyInfo key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Backspace)
                        {
                            break;
                        }
                        if (key.Key == ConsoleKey.D)
                        {
                            myProcess.Kill();
                            Console.Clear();
                            break;
                        }
                        if (key.Key == ConsoleKey.Delete)
                        {
                            foreach (var process in Process.GetProcessesByName(myProcess.ProcessName))
                            {
                                process.Kill();
                                Console.Clear();
                                break;
                            }
                        }
                        Console.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Process myProcess = process[pozition];
                Console.WriteLine(ex.Message);
                while (true)
                {
                    myProcess.Refresh();
                    Console.WriteLine($"{myProcess} -");
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine("Процесс недоступен для просмотраа");
                    Console.WriteLine($"Причина: {ex.Message}");

                    if (myProcess.Responding)
                    {
                        Console.WriteLine("-------------------------------------------------------------------");
                        Console.WriteLine("Статус = Запущено");
                        Console.WriteLine("-------------------------------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("-------------------------------------------------------------------");
                        Console.WriteLine("Статус = Незапущено");
                        Console.WriteLine("-------------------------------------------------------------------");
                    }
                    Console.WriteLine("BACK чтобы вернуться");
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Backspace)
                    {
                        break;
                    }
                    Console.Clear();
                }
            }
            finally
            {
                Console.Clear();
                Zpusk();
            }
        }
        public static void Zpusk()
        {
            Menu();
            Arrows.Arrow();
            Menu(Arrows.pozition);
        }
    }
}