namespace MikSoft.Docua.Common.Data.FileSystem.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Timers;

    using MikSoft.Docua.Common.TestWrappers;

    internal abstract class RepositoryBase<T> : IRepository<T>
        where T : IFileInfo

    {
        private readonly List<T> _itemsFromLastScan;

        private readonly Timer _timer;

        protected RepositoryBase()
        {
            DirectoryTestWrapper = new DirectoryTestWrapper();
            FileTestWrapper = new FileTestWrapper();

            _timer = new Timer
                         {
                             AutoReset = false
                         };

            _itemsFromLastScan = new List<T>();
        }

        internal DirectoryTestWrapper DirectoryTestWrapper { get; set; }

        public FileTestWrapper FileTestWrapper { get; set; }

        public void StartMonitoring(string path, string searchPattern, SearchOption searchOption, double interval = 5000)
        {
            _timer.Interval = interval;
            _timer.Elapsed += (o, args) => TimerElapsed(path, searchPattern, searchOption);

            ScanForFiles(path, searchPattern, searchOption);
            _timer.Start();
        }

        public event EventHandler<RepositoryChangedEventArgs<T>> RepositoryChanged;

        public List<T> GetAll()
        {
            return _itemsFromLastScan;
        }

        public void StopMonitoring()
        {
            _timer.Stop();
        }

        public void Remove(T entry)
        {
            FileTestWrapper.Delete(entry.FileName);
        }

        protected abstract void SaveNewEntry(T entry);

        public void Add(T entry)
        {
            if (string.IsNullOrEmpty(entry.FileName))
            {
                throw new ArgumentException("The FileName property of the entry must have a value.");
            }

            SaveNewEntry(entry);
        }

        protected abstract T CreateEntry(string content);

        private void ScanForFiles(string path, string searchPattern, SearchOption searchOption)
        {
            var filesFromLastScan = _itemsFromLastScan;

            var filesFromDirectory = DirectoryTestWrapper.GetFiles(path, searchPattern, searchOption);
            var existingFiles = filesFromDirectory.Where(x => _itemsFromLastScan.Any(y => y.FileName == x)).ToList();
            var deletedFiles = filesFromLastScan.Select(x => x.FileName).Except(existingFiles).ToList();

            foreach (var deletedFile in deletedFiles)
            {
                var itemToRemove = filesFromLastScan.Single(x => x.FileName == deletedFile);
                _itemsFromLastScan.Remove(itemToRemove);

                OnRepositoryChanged(new RepositoryChangedEventArgs<T>(itemToRemove, RepositoryChangedEventArgs<T>.RepositoryAction.Remove));
            }

            var newFiles = filesFromDirectory.Except(existingFiles).ToList();

            foreach (var newFile in newFiles)
            {
                var content = FileTestWrapper.ReadAllText(newFile);

                var newItem = CreateEntry(content);
                newItem.FileName = newFile;
                _itemsFromLastScan.Add(newItem);

                OnRepositoryChanged(new RepositoryChangedEventArgs<T>(newItem, RepositoryChangedEventArgs<T>.RepositoryAction.Add));
            }
        }

        private void TimerElapsed(string path, string searchPattern, SearchOption searchOption)
        {
            ScanForFiles(path, searchPattern, searchOption);
            _timer.Start();
        }

        protected virtual void OnRepositoryChanged(RepositoryChangedEventArgs<T> e)
        {
            RepositoryChanged?.Invoke(this, e);
        }
    }
}