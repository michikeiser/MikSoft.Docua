namespace MikSoft.Docua.Common.TestWrappers
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    [ExcludeFromCodeCoverage]
    public class PathTestWrapper
    {
        public virtual string Combine(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }
    }
}