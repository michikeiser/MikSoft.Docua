namespace MikSoft.Docua.Common.Data.UnitTests.FileSystem.Inbox
{
    using System;
    using System.Threading;

    using AutoFixture.NUnit3;

    using FakeItEasy;

    using MikSoft.Docua.Common.Data.FileSystem.Inbox;
    using MikSoft.Docua.Common.TestWrappers;

    using NUnit.Framework;

    [TestFixture]
    internal class InboxRepositoryTests
    {
        [Test, AutoData]
        public void GetAll_AddFiles_ReturnPaths(string path, string[] files)
        {
            // arrange
            var directoryTestWrapper = A.Fake<DirectoryTestWrapper>();
            A.CallTo(() => directoryTestWrapper.GetFiles(path)).Returns(files);

            var sut = new InboxRepository
                          {
                              DirectoryTestWrapper = directoryTestWrapper
                          };

            sut.Start(path);
            sut.Stop();

            // act
            var result = sut.GetAll();

            // assert
            Assert.That(result, Is.EquivalentTo(files));
        }

        [Test, AutoData]
        public void Remove_DeleteFile_CallDelete(string path)
        {
            // arrange
            var fileTestWrapper = A.Fake<FileTestWrapper>();
            var sut = new InboxRepository
                          {
                              FileTestWrapper = fileTestWrapper
                          };

            // act
            sut.Remove(path);

            // assert
            A.CallTo(() => fileTestWrapper.Delete(path)).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void RepositoryChanged_AddFiles_ExecuteEvent(string path, string[] files)
        {
            // arrange
            var directoryTestWrapper = A.Fake<DirectoryTestWrapper>();
            A.CallTo(() => directoryTestWrapper.GetFiles(path)).Returns(files);

            var eventHandler = A.Fake<EventHandler<InboxEventArgs>>();

            var sut = new InboxRepository
                          {
                              DirectoryTestWrapper = directoryTestWrapper
                          };

            sut.RepositoryChanged += eventHandler;

            // act
            sut.Start(path);
            sut.Stop();

            // assert
            // Check if event is raised
            A.CallTo(() => eventHandler.Invoke(A<object>._, A<InboxEventArgs>.That.Matches(x => x.Action == InboxEventArgs.FileAction.Add)))
                .MustHaveHappened();
        }

        [Test, AutoData]
        public void RepositoryChanged_RemoveFiles_ExecuteEvent(string path, string[] files)
        {
            // arrange
            var directoryTestWrapper = A.Fake<DirectoryTestWrapper>();
            A.CallTo(() => directoryTestWrapper.GetFiles(path)).Returns(files);

            var eventHandler = A.Fake<EventHandler<InboxEventArgs>>();

            var sut = new InboxRepository
                          {
                              DirectoryTestWrapper = directoryTestWrapper
                          };

            sut.RepositoryChanged += eventHandler;

            sut.Start(path);
            sut.Stop();
            A.CallTo(() => directoryTestWrapper.GetFiles(path)).Returns(
                new[]
                    {
                        files[0]
                    });

            // act
            sut.Start(path);
            sut.Stop();

            // assert
            // Check if event is raised
            A.CallTo(() => eventHandler.Invoke(A<object>._, A<InboxEventArgs>.That.Matches(x => x.Action == InboxEventArgs.FileAction.Remove)))
                .MustHaveHappened();
        }

        [Test, AutoData]
        public void Start_StartupBehavior_CallsGetFiles(string path)
        {
            // arrange
            var directoryTestWrapper = A.Fake<DirectoryTestWrapper>();
            var sut = new InboxRepository
                          {
                              DirectoryTestWrapper = directoryTestWrapper
                          };

            // act
            sut.Start(path);
            sut.Stop();

            // assert
            A.CallTo(() => directoryTestWrapper.GetFiles(path)).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void Start_Timer_CheckTimerElapsed(string path, string file)
        {
            // arrange
            var directoryTestWrapper = A.Fake<DirectoryTestWrapper>();
            A.CallTo(() => directoryTestWrapper.GetFiles(path)).Returns(
                new[]
                    {
                        file
                    });

            var eventHandler = A.Fake<EventHandler<InboxEventArgs>>();

            var sut = new InboxRepository
                          {
                              DirectoryTestWrapper = directoryTestWrapper
                          };

            sut.RepositoryChanged += eventHandler;

            // act
            sut.Start(path);

            A.CallTo(() => directoryTestWrapper.GetFiles(path)).Returns(new string[0]);
            Thread.Sleep(6000);
            sut.Stop();

            // assert
            // Check if event is raised
            A.CallTo(() => eventHandler.Invoke(A<object>._, A<InboxEventArgs>.That.Matches(x => x.Action == InboxEventArgs.FileAction.Remove)))
                .MustHaveHappenedOnceExactly();
        }
    }
}