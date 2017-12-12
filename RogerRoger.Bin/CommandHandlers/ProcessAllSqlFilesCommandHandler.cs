using System.Threading.Tasks;
using NCore.Base.Commands;
using NLog;

namespace RogerRoger.Bin.Commands
{
  public class ProcessAllSqlFilesCommandHandler : ICommandHandler<ProcessAllSqlFilesCommand>
  {
    public Task Execute(ProcessAllSqlFilesCommand command)
    {
      var logger = LogManager.GetCurrentClassLogger();

      logger.Info($"Connection: {command.Connection}");
      logger.Info($"Folder: {command.Folder}");

      throw new System.NotImplementedException();
    }
  }
}