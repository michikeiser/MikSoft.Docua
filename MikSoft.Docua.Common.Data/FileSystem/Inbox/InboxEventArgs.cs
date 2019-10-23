namespace MikSoft.Docua.Common.Data.FileSystem.Inbox
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class InboxEventArgs : EventArgs
    {
        public enum FileAction
        {
            Add,

            Remove
        }

        public InboxEventArgs(string file, FileAction fileAction)
        {
            File = file;
            Action = fileAction;
        }

        public string File { get; private set; }

        public FileAction Action { get; private set; }
    }
}