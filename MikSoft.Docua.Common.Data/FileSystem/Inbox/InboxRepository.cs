namespace MikSoft.Docua.Common.Data.FileSystem.Inbox
{
    using MikSoft.Docua.Common.Data.FileSystem.Common;

    internal class InboxRepository : RepositoryBase<InboxEntry>
    {
        protected override void SaveNewEntry(InboxEntry entry)
        {
            // do nothing
        }

        protected override InboxEntry CreateEntry(string content)
        {
            return new InboxEntry();
        }
    }
}