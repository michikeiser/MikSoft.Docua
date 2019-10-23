namespace MikSoft.Docua.Common.Data.FileSystem.Inbox
{
    using System;
    using System.Collections.Generic;

    public interface IInboxRepository
    {
        void Start(string inboxPath);

        event EventHandler<InboxEventArgs> RepositoryChanged;

        IEnumerable<string> GetAll();

        void Remove(string inboxPath);

        void Stop();
    }
}