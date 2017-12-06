using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace RogerRoger.IO
{
  public class FileFinder
  {
    private readonly IFileSystem _fileSystem;

    public FileFinder(IFileSystem fileSystem)
    {
      _fileSystem = fileSystem;
    }

    public IEnumerable<string> Walk(string root, Func<string, bool> matches)
    {
      var pending = new Queue<string>();
      pending.Enqueue(root);
      while (pending.Any())
      {
        var here = pending.Dequeue();
        foreach (var filePath in _fileSystem.Directory.GetFiles(here))
        {
          if (matches(filePath))
          {
            yield return filePath;
          }
        }
        foreach (var folderPath in _fileSystem.Directory.GetDirectories(here))
        {
          pending.Enqueue(folderPath);
        }
      }
    }
  }
}