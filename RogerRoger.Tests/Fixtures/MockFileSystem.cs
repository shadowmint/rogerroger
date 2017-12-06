using System.IO.Abstractions;

namespace RogerRoger.Tests.Fixtures
{
  public class MockFileSystem
  {
    public static IFileSystem MockFileSystemSimple()
    {
      var rtn = new System.IO.Abstractions.TestingHelpers.MockFileSystem();
      rtn.Directory.CreateDirectory(@"C:\home\foo\tests");
      rtn.Directory.CreateDirectory(@"C:\home\foo\tests\sub1");
      rtn.File.WriteAllText(@"C:\home\foo\tests\test1.sql", "SELECT * FROM FOO");
      rtn.File.WriteAllText(@"C:\home\foo\tests\test2.sql", "SELECT * FROM BAR");
      rtn.File.WriteAllText(@"C:\home\foo\tests\test1.txt", "...");
      rtn.File.WriteAllText(@"C:\home\foo\tests\test2.txt", "...");
      rtn.File.WriteAllText(@"C:\home\foo\tests\sub1\test1.sql", "...");
      rtn.File.WriteAllText(@"C:\home\foo\tests\sub1\test2.sql", "...");
      return rtn;
    }
  }
}