namespace MikSoft.Docua.Common.TestWrappers
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    [ExcludeFromCodeCoverage]
    public class FileTestWrapper
    {
        public virtual bool Exists(string path)
        {
            return File.Exists(path);
        }

        public virtual string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public virtual void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }

        public virtual void Delete(string path)
        {
            File.Delete(path);
        }
    }
}