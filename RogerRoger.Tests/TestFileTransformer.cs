using System;
using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Moq;
using NLog;
using RogerRoger.IO;
using RogerRoger.Tests.Fixtures;
using RogerRoger.Tests.Helpers;
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

    [Fact]
    public async Task TestMatch()
    {
      var fs = MockFileSystem.MockFileSystemSimple();
      var logger = new Mock<ILogger>();
      var transformer = new FileTransformer(fs, logger.Object);
      var collector = new ConcurrentBag<string>();
      var action = new FileAction(fs, (path) => path.ToLowerInvariant().EndsWith(".sql"), (path) => { collector.Add(path); }, (_) => { });

      await transformer.Transform(@"C:\home\foo\tests", action);

      Assert.Equal(4, collector.Count);
    }

    [Fact]
    public async Task TestFailed()
    {
      var fs = MockFileSystem.MockFileSystemSimple();
      var logger = new Mock<ILogger>();
      var transformer = new FileTransformer(fs, logger.Object);
      var success = new ConcurrentBag<string>();
      var reject = new ConcurrentBag<string>();
      var action = new FileAction(
          fs,
          (path) => path.ToLowerInvariant().EndsWith(".sql"),
          (path) => { success.Add(path); }, (path) => { reject.Add(path); })
        .Add(new MockRejectContent("..."));

      await transformer.Transform(@"C:\home\foo\tests", action);

      Assert.Equal(2, success.Count);
      Assert.Equal(2, reject.Count);
    }
  }
}