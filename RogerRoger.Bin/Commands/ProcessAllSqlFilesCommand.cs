using NCore.Base.Commands;

namespace RogerRoger.Bin.Commands
{
  public class ProcessAllSqlFilesCommand : ICommand
  {
    public string Folder { get; set; }
    public string Connection { get; set; }
  }
}