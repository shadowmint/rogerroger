using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace RogerRoger.IO
{
  public class FileTransformer
  {
    private readonly IFileSystem _fileSystem;
    private readonly ILogger _logger;

    public FileTransformer(IFileSystem fileSystem, ILogger logger)
    {
      _fileSystem = fileSystem;
      _logger = logger;
    }

    public async Task Transform(string root, IFileTransform transformation)
    {
      var walker = new FileFinder(_fileSystem);
      var tasks = walker.Walk(root, transformation.Matches).Select(path => ProcessFile(path, transformation)).ToList();
      await Task.WhenAll(tasks);
    }

    private async Task ProcessFile(string path, IFileTransform transformation)
    {
      try
      {
        if (await transformation.Process(path))
        {
          transformation.Success(path);
        }
        else
        {
          transformation.Failed(path);
        }
      }
      catch (Exception error)
      {
        _logger.Error(error);
        transformation.Failed(path);
      }
    }
  }
}