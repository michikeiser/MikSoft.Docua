namespace MikSoft.Docua.Common.Data.FileSystem.Inbox
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Timers;

    using MikSoft.Docua.Common.TestWrappers;

    internal class InboxRepository : IInboxRepository
    {
        private List<string> _lastScan = new List<string>();

        private Timer _timer;

        public InboxRepository()
        {
            FileTestWrapper = new FileTestWrapper();
            DirectoryTestWrapper = new DirectoryTestWrapper();
        }

        internal FileTestWrapper FileTestWrapper { get; set; }

        internal DirectoryTestWrapper DirectoryTestWrapper { get; set; }

        public void Start(string inboxPath)
        {
            _timer = new Timer
                         {
                             Interval = 5000
                         };
            _timer.Elapsed += (o, args) => TimerElapsed(inboxPath, _timer);

            Scan(inboxPath);
            _timer.Start();
        }

        public event EventHandler<InboxEventArgs> RepositoryChanged;

        public IEnumerable<string> GetAll()
        {
            return _lastScan;
        }

        public void Remove(string file)
        {
            FileTestWrapper.Delete(file);
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void TimerElapsed(string inboxPath, Timer timer)
        {
            Scan(inboxPath);
            timer.Start();
        }

        private void Scan(string inboxPath)
        {
            var newScan = GetFiles(inboxPath).ToList();
            var lastScan = _lastScan;

            var existing = newScan.Where(x => _lastScan.Any(y => y == x)).ToList();

            var deletedItems = lastScan.Except(existing).ToList();

            foreach (var deletedItem in deletedItems)
            {
                ProcessChange(deletedItem, InboxEventArgs.FileAction.Remove);
            }

            var newItems = newScan.Except(existing);

            foreach (var newItem in newItems)
            {
                ProcessChange(newItem, InboxEventArgs.FileAction.Add);
            }

            _lastScan = newScan.ToList();
        }

        private void ProcessChange(string file, InboxEventArgs.FileAction fileAction)
        {
            OnRepositoryChanged(new InboxEventArgs(file, fileAction));
        }

        private IEnumerable<string> GetFiles(string inboxPath)
        {
            var files = DirectoryTestWrapper.GetFiles(inboxPath);
            return files;
        }

        protected virtual void OnRepositoryChanged(InboxEventArgs e)
        {
            RepositoryChanged?.Invoke(this, e);
        }
    }
}