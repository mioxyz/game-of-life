
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
