// Author: Mark Otto, 20.3.2020
using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Game Of Dank Memes");
            Console.ReadKey();

            int k = 0;
            bool run = true;

            Engine engine = new Engine(17, 17, WorkingDirectory.ReadFile("data/board/pulsar.board"));
            engine.getBoard().swapBuffers();

            while (run) {
                engine.loop();

                if (k > 500) {
                    k = 0;
                    if (Console.ReadKey().Key == ConsoleKey.Y) run = false;
                }
            }
            Console.Write("finished.");
        }
    }
}
