using System.Reflection;

namespace Hafnia.Integrations.Tests.Shared.Helpers;

public static class DataFileHelper
{
    public static string GetText(string path)
    {
        string workingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string filePath = Path.Combine(workingDirectory, path);

        return File.ReadAllText(filePath);
    }
}
