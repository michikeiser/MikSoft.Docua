namespace MikSoft.Docua.Common.TestWrappers
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    [ExcludeFromCodeCoverage]
    public class DirectoryTestWrapper
    {
        public virtual bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public virtual DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }
    }
}