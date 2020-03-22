/*
This file is part of "mio's Game Of Life".

"mio's Game Of Life" is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

"mio's Game Of Life" is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with "mio's Game Of Life".  If not, see<https://www.gnu.org/licenses/>
*/

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
