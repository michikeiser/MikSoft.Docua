namespace MikSoft.Docua.Common.Data.FileSystem.Common
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class RepositoryChangedEventArgs<T> : EventArgs
    {
        public enum RepositoryAction
        {
            Add,

            Remove
        }

        public RepositoryChangedEventArgs(T item, RepositoryAction action)
        {
            Item = item;
            Action = action;
        }

        public T Item { get; set; }

        public RepositoryAction Action { get; set; }
    }
}