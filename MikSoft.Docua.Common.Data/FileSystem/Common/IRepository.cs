namespace MikSoft.Docua.Common.Data.FileSystem.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public interface IRepository<T>
        where T : IFileInfo
    {
        void StartMonitoring(string path, string searchPattern, SearchOption searchOption, double interval = 5000);

        event EventHandler<RepositoryChangedEventArgs<T>> RepositoryChanged;

        List<T> GetAll();

        void StopMonitoring();

        void Remove(T entry);

        void Add(T entry);
    }
}