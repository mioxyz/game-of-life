// Author: Mark Otto, 20.3.2020
using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            //Console.WriteLine(System.IO.Path.GetFullPath(System.IO.Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\")));
            //Console.ReadKey();

            int k = 0;
            bool run = true;

            Engine engine = new Engine(5, 5,
                System.IO.File.ReadAllText(
                    WorkingDirectory.VisualStudioSolution + "\\data\\board\\blinker.board"
                )
            );

            Console.Write("Game Of Dank Memes");
            //engine.populateRandom();

            while (run)
            {
                engine.loop();

                if (k > 500)
                {
                    k = 0;
                    if (Console.ReadKey().Key == ConsoleKey.Y) run = false;
                }
            }
            Console.Write("finished.");
        }
    }
}
