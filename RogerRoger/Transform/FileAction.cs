using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Abstractions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using RogerRoger.Transform.Standard;

namespace RogerRoger.IO
{
  public class FileAction : IFileTransform
  {
    private readonly Func<string, bool> _matches;
    private readonly Action<string> _success;
    private readonly Action<string> _failure;
    private readonly List<IFileProcessTask> _tasks;

    public FileAction(IFileSystem fileSystem, Func<string, bool> matches, Action<string> success, Action<string> failure)
    {
      _matches = matches;
      _success = success;
      _failure = failure;
      _tasks = new List<IFileProcessTask>();
      Add(new ReadFileTask(fileSystem));
    }

    public FileAction Add(IFileProcessTask task)
    {
      _tasks.Add(task);
      return this;
    }

    public async Task<bool> Process(string path)
    {
      var data = path;
      foreach (var task in _tasks)
      {
        data = await task.Process(data);
        if (data == null) return false;
      }

      Debug.WriteLine($"Finished with path: {path}");
      return true;
    }

    public bool Matches(string path)
    {
      return _matches(path);
    }

    public void Success(string path)
    {
      _success(path);
    }

    public void Failed(string path)
    {
      _failure(path);
    }
  }
}