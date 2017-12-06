using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using Moq;
using NLog;
using RogerRoger.IO;
using RogerRoger.Tests.Fixtures;
using Xunit;

namespace RogerRoger.Tests
{
  public class TestFileTransformer
  {
    [Fact]
    public async Task TestAllTransformationStepsAreInvoked()
    {
      var fs = MockFileSystem.MockFileSystemSimple();
      var logger = new Mock<ILogger>();
      var transformer = new FileTransformer(fs, logger.Object);
      var collector = new MockCollectOutput();
      var action = new FileAction(fs, (_) => true, (_) => { }, (_) => { }).Add(collector);

      await transformer.Transform(@"C:\home\foo\tests", action);

      Assert.Equal(6, collector.Inputs.Count);
    }

    [Fact]
    public async Task TestSuccess()
    {
      var fs = MockFileSystem.MockFileSystemSimple();
      var logger = new Mock<ILogger>();
      var transformer = new FileTransformer(fs, logger.Object);
      var collector = new ConcurrentBag<string>();
      var action = new FileAction(fs, (_) => true, (path) => { collector.Add(path); }, (_) => { });

      await transformer.Transform(@"C:\home\foo\tests", action);

      Assert.Equal(6, collector.Count);
    }
  }
}