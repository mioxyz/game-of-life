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
using System.Collections.Generic;
namespace GameOfLife
{
    /// <summary>
    /// the Board objects can be thought of 2d strings.
    /// It contains two canvases between we can switch, such that we can
    /// read from one and write into the other (buffered).
    /// </summary>
    public class Board
    {
        // mutables
        private bool[,,] raw;

        int buffer = 0; // buffer <- [0,1]
        private int dim_x;
        private int dim_y;

        public Board(int x, int y)
        {
            dim_x = x;
            dim_y = y;
            raw = new bool[2 + x * 2, 2 + y * 2, 2];
        }

        public Board(int x, int y, string data)
        {
            dim_x = x;
            dim_y = y;
            raw = new bool[2 + x * 2, 2 + y *2, 2];
            parseData(data);
        }

        private void parseData(string data)
        {
            int x = 0;
            int y = 0;

            foreach (char c in data) switch (c) {
                case '\n': x = 0; ++y;             break;
                case  '#': setCell(x++, y, true);  break;
                default  : setCell(x++, y, false); break;
            }
            
            if (x > dim_x) throw new System.ArgumentException("data in x-axis is out of pre-defined range");
            if (y > dim_y) throw new System.ArgumentException("data in y-axis is out of pre-defined range");
        }

        public int getDim_x() => dim_x;
        public int getDim_y() => dim_y;

        /// <summary> maps an integer number k <- [..,-1,0,1,2,..] onto some range [0, b] linearly </summary>
        private int normalize(int k, int b) => (k >= 0) ? k % b : normalize(k + b, b);

        //returns board cell value, normalized and all, as an integer.
        public int getCell(int x, int y)
        {
            if (raw[normalize(x, dim_x), normalize(y, dim_y), buffer]) return 1;
            return 0;
        }

        public bool isAlive(int x, int y) => getCell(x, y) == 1;


        public void setCell(int x, int y, bool state)
        {
            raw[normalize(x, dim_x), normalize(y, dim_y), (buffer == 0) ? 1 : 0] = state;
        }

        public int swapBuffers()
        {
            buffer = (buffer == 0) ? 1 : 0;
            return buffer;
        }

        public void draw()
        {
            Console.Clear();
            for (int y = 0; y < dim_y; ++y) {
                string line = "";
                for (int x = 0; x < dim_x; ++x) line += (raw[x, y, buffer]) ? "#" : " ";
                Console.WriteLine(line);
            }
        }

        public void populateRandom()
        {
            Random rnd = new Random();
            for (int x = 0; x < getDim_x(); ++x)
                for (int y = 0; y < getDim_y(); ++y)
                    setCell(x, y, ((rnd.Next() % 2) == 0));
            swapBuffers();
        }

        public string toString()
        {
            string ret = "";
            for (int x = 0; x < dim_x; ++x) {
                for (int y = 0; y < dim_y; ++y) ret += raw[x, y, buffer] ? "#" : " ";
                ret += Environment.NewLine;
            }
            return ret;
        }

        private static Board crop(Board host, int x, int y, int dim_x, int dim_y)
        {
            Board substr = new Board(dim_x, dim_y);
            for (int j = 0; j < host.getDim_x(); ++j)
                for (int k = 0; k < host.getDim_y(); ++k)
                    substr.setCell(j, k, host.getCell(x + j, y + k) == 1);
            return substr;
        }

        public static bool operator ==(Board v, Board w)
        {
            if (v.getDim_x() != w.getDim_x() || v.getDim_y() != w.getDim_y())
                return false;

            for (int x = 0; x < v.getDim_x(); ++x)
                for (int y = 0; y < v.getDim_y(); ++y)
                    if (v.getCell(x, y) != w.getCell(x, y)) 
                        return false;
           
            return true;
        }
        public static bool operator !=(Board v, Board w) => !(v == w);


        //contains operator
        public static bool operator <(Board v, Board w)
        {
            if (v.getDim_y() > w.getDim_y()) return false;
            if (v.getDim_x() > w.getDim_x()) return false;

            for (int x = 0; x < w.getDim_x() - v.getDim_x(); ++x)
                for (int y = 0; y < w.getDim_y() - v.getDim_y(); ++y)
                    if (v == crop(w, x, y, v.dim_x, v.dim_y)) return true;

            return false;
        }

        public static bool operator >(Board v, Board w) => w < v;

    };

}
