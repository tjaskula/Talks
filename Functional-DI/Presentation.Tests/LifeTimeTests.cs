using System;
using Infrastructure;
using Presentation.CommandHandlers;
using Xunit;

namespace Presentation.Tests
{
    public class LifeTimeTests
    {
        [Fact]
        public void PerThreadShouldResolveOncePerThread()
        {
            Func<FakeRepository> repositoryFactory = () => new FakeRepository();

            using (var lifeTime = new LifeTime())
            {
                var fakeRepo1 = lifeTime.PerThread(repositoryFactory);
                var fakeRepo2 = lifeTime.PerThread(repositoryFactory);
                Assert.Same(fakeRepo1, fakeRepo2);
            }
        }
    }

    public class FakeRepository
    {
        public FakeRepository()
        {
            InitializedTimes++;
        }

        public int InitializedTimes { get; private set; }
    }
}