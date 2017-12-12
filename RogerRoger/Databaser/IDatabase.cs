using System.Threading.Tasks;

namespace RogerRoger.Databaser
{
  public interface IDatabase
  {
    Task<string> Execute(string rawSql);
  }
}