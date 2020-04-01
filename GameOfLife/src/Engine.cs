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
using System.Threading;
using System.IO;

namespace GameOfLife
{

    /// <summary>
    /// The Engine loads board data and implements the rules of Conway's 
    /// Game of life. An Engine instance is always bound to a single board.
    /// </summary>
    public class Engine
    {
        // immutables
        static readonly int deltaTick = 120;             //delay in milliseconds
        static readonly int thresholdBirth = 3;
        static readonly int thresholdMalnourished = 2;
        static readonly int thresholdOverpopulation = 3;

        private Board board;

       

        public Engine(int dim_x, int dim_y, string data)
        {
            board = new Board(dim_x, dim_y, data);
        }

        //private string load(string name) => File.ReadAllText(Directory.GetCurrentDirectory() + "\\boards\\" + name + ".board");
        public string load(string fileName) => File.ReadAllText(fileName);
        public ref Board getBoard() => ref board; // I think this creates a copy.. can't swap buffers from outside of engine..

        int countSiblings(int x, int y)
        {
            int siblingCount = (-1) * board.getCell(x, y);
            for (int v = -1; v <= 1; ++v)
                for (int w = -1; w <= 1; ++w)
                    siblingCount += board.getCell(x + v, y + w);
            return siblingCount;
        }

        ///<summary>
        /// step executes conway's game of life algorithm onto the board's active buffer.
        /// The rules (copied from en.wikipedia) are as follows:
        ///    1. Any live cell with two or three neighbors survives.
        ///    2. Any dead cell with three live neighbors becomes a live cell.
        ///    3. All other live cells die in the next generation.Similarly, all other dead cells stay dead.
        /// effectively we only have two cases (which in turn have two subcases, I guess), 
        /// since cells are necessarily dead or alive.
        ///</summary>
        public void step()
        {
            for (int x = 0; x < board.getDim_x(); ++x)
                for (int y = 0; y < board.getDim_y(); ++y) {
                    int c = countSiblings(x, y);
                    if (board.isAlive(x, y))
                        board.setCell(x, y, (c == thresholdMalnourished || c == thresholdOverpopulation));
                    else
                        board.setCell(x, y, (c == thresholdBirth));
                }
        }

        public void step(int steps)
        {
            while (0 < steps--) {
                board.swapBuffers();
                step();
            }
        }

        public void loop()
        {
            step();
            board.swapBuffers();
            board.draw();
            Thread.Sleep(deltaTick);
        }

        public Board getMainBoard()
        {
            Database db = new Database();

            CouchResponse<Board> response = db.get<Board>(new Board("main", "1"));

            return response.docs[0];
        }

    }

}
