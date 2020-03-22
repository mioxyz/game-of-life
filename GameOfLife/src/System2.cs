
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

namespace GameOfLife
{
    public static class WorkingDirectory
    {
        //public static readonly string VisualStudioProject = System.IO.Path.GetFullPath(System.IO.Path.Combine(System.AppContext.BaseDirectory, "..\\..\\"));
        public static readonly string VisualStudioSolution = "C:\\dev\\GOL\\";
        //public static readonly string Executable = System.AppContext.BaseDirectory;

        public static string ReadFile(string fileName) => System.IO.File.ReadAllText(VisualStudioSolution + fileName.Replace("/", "\\"));
    }
}
