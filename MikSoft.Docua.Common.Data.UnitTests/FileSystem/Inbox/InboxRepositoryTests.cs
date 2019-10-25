namespace MikSoft.Docua.Common.Data.UnitTests.FileSystem.Inbox
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using AutoFixture.NUnit3;

    using FakeItEasy;

    using MikSoft.Docua.Common.Data.FileSystem.Common;
    using MikSoft.Docua.Common.Data.FileSystem.Inbox;
    using MikSoft.Docua.Common.TestWrappers;

    using NUnit.Framework;

    [TestFixture]
    internal class InboxRepositoryTests
    {
        [Test, AutoData]
        public void GetAll_AddFiles_ReturnPaths(string path, string searchPattern, SearchOption searchOption, string[] files)
        {
            // arrange
            var directoryTestWrapper = A.Fake<DirectoryTestWrapper>();
            A.CallTo(() => directoryTestWrapper.GetFiles(path, searchPattern, searchOption)).Returns(files);

            var fileTestWrapper = A.Fake<FileTestWrapper>();

            var sut = new InboxRepository
                          {
                              DirectoryTestWrapper = directoryTestWrapper,
                              FileTestWrapper = fileTestWrapper
                          };

            sut.StartMonitoring(path, searchPattern, searchOption);
            sut.StopMonitoring();

            // act
            var result = sut.GetAll().Select(x => x.FileName);

            // assert
            Assert.That(result, Is.EquivalentTo(files));
        }

        [Test, AutoData]
        public void Remove_DeleteFile_CallDelete(InboxEntry inboxEntry)
        {
            // arrange
            var fileTestWrapper = A.Fake<FileTestWrapper>();
            var sut = new InboxRepository
                          {
                              FileTestWrapper = fileTestWrapper
                          };

            // act
            sut.Remove(inboxEntry);

            // assert
            A.CallTo(() => fileTestWrapper.Delete(inboxEntry.FileName)).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void RepositoryChanged_AddFiles_ExecuteEvent(string path, string searchPattern, SearchOption searchOption, string[] files)
        {
            // arrange
            var directoryTestWrapper = A.Fake<DirectoryTestWrapper>();
            A.CallTo(() => directoryTestWrapper.GetFiles(path, searchPattern, searchOption)).Returns(files);

            var fileTestWrapper = A.Fake<FileTestWrapper>();

            var eventHandler = A.Fake<EventHandler<RepositoryChangedEventArgs<InboxEntry>>>();

            var sut = new InboxRepository
                          {
                              DirectoryTestWrapper = directoryTestWrapper,
                              FileTestWrapper = fileTestWrapper
                          };

            sut.RepositoryChanged += eventHandler;

            // act
            sut.StartMonitoring(path, searchPattern, searchOption);
            sut.StopMonitoring();

            // assert
            // Check if event is raised
            A.CallTo(
                () => eventHandler.Invoke(
                    A<object>._,
                    A<RepositoryChangedEventArgs<InboxEntry>>.That.Matches(
                        x => x.Action == RepositoryChangedEventArgs<InboxEntry>.RepositoryAction.Add))).MustHaveHappened();
        }

        [Test, AutoData]
        public void RepositoryChanged_RemoveFiles_ExecuteEvent(string path, string searchPattern, SearchOption searchOption, string[] files)
        {
            // arrange
            var directoryTestWrapper = A.Fake<DirectoryTestWrapper>();
            A.CallTo(() => directoryTestWrapper.GetFiles(path, searchPattern, searchOption)).Returns(files);

            var fileTestWrapper = A.Fake<FileTestWrapper>();

            var eventHandler = A.Fake<EventHandler<RepositoryChangedEventArgs<InboxEntry>>>();

            var sut = new InboxRepository
                          {
                              DirectoryTestWrapper = directoryTestWrapper,
                              FileTestWrapper = fileTestWrapper
                          };

            sut.RepositoryChanged += eventHandler;

            sut.StartMonitoring(path, searchPattern, searchOption);
            sut.StopMonitoring();
            A.CallTo(() => directoryTestWrapper.GetFiles(path, searchPattern, searchOption)).Returns(
                new[]
                    {
                        files[0]
                    });

            // act
            sut.StartMonitoring(path, searchPattern, searchOption);
            sut.StopMonitoring();

            // assert
            A.CallTo(
                () => eventHandler.Invoke(
                    A<object>._,
                    A<RepositoryChangedEventArgs<InboxEntry>>.That.Matches(
                        x => x.Action == RepositoryChangedEventArgs<InboxEntry>.RepositoryAction.Remove))).MustHaveHappened();
        }

        [Test, AutoData]
        public void Start_StartupBehavior_CallsGetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            // arrange
            var directoryTestWrapper = A.Fake<DirectoryTestWrapper>();
            var sut = new InboxRepository
                          {
                              DirectoryTestWrapper = directoryTestWrapper
                          };

            // act
            sut.StartMonitoring(path, searchPattern, searchOption);
            sut.StopMonitoring();

            // assert
            A.CallTo(() => directoryTestWrapper.GetFiles(path, searchPattern, searchOption)).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void Start_Timer_CheckTimerElapsed(string path, string searchPattern, SearchOption searchOption, string file)
        {
            // arrange
            var directoryTestWrapper = A.Fake<DirectoryTestWrapper>();
            A.CallTo(() => directoryTestWrapper.GetFiles(path, searchPattern, searchOption)).Returns(
                new[]
                    {
                        file
                    });

            var fileTestWrapper = A.Fake<FileTestWrapper>();

            var sut = new InboxRepository
                          {
                              DirectoryTestWrapper = directoryTestWrapper,
                              FileTestWrapper = fileTestWrapper
                          };

            var eventHandler = A.Fake<EventHandler<RepositoryChangedEventArgs<InboxEntry>>>();
            sut.RepositoryChanged += eventHandler;

            // act
            sut.StartMonitoring(path, searchPattern, searchOption, 200);

            A.CallTo(() => directoryTestWrapper.GetFiles(path, searchPattern, searchOption)).Returns(new string[0]);
            Thread.Sleep(250);
            sut.StopMonitoring();

            // assert
            A.CallTo(
                () => eventHandler.Invoke(
                    A<object>._,
                    A<RepositoryChangedEventArgs<InboxEntry>>.That.Matches(
                        x => x.Action == RepositoryChangedEventArgs<InboxEntry>.RepositoryAction.Remove))).MustHaveHappenedOnceExactly();
        }
    }
}