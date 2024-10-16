using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Text;

namespace Lubricentro25.Services;

public class JsonConfigReader
{
    readonly string dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lubricentro25/Resources/Data");

    public Stream ReadJsonFile(string fileName)
    {
        string filePath = Path.Combine(dataDirectory, fileName);

        if(!Directory.Exists(dataDirectory))
        {
            Directory.CreateDirectory(dataDirectory);
        }

        if(File.Exists(filePath))
        {
            UpdateFile(fileName);
        }
        else
        {
            CreateFile(fileName);
        }

        string json = File.ReadAllText(filePath);
        return new MemoryStream(Encoding.UTF8.GetBytes(json));
    }

    private void UpdateFile(string fileName)
    {
        string destinationFilePath = Path.Combine(dataDirectory, fileName);
        string executingDirectory = Path.GetDirectoryName(AppContext.BaseDirectory)!;
        string sourceFilePath = Path.Combine(executingDirectory, "Resources/Data", fileName);

        JObject originalJson = JObject.Parse(File.ReadAllText(sourceFilePath));
        JObject destinationJson = JObject.Parse(File.ReadAllText(destinationFilePath));

        MergeJson(destinationJson, originalJson);

        File.WriteAllText(destinationFilePath, destinationJson.ToString());
    }
    private void CreateFile(string fileName)
    {
        string destinationFilePath = Path.Combine(dataDirectory, fileName);

        string executingDirectory = Path.GetDirectoryName(AppContext.BaseDirectory)!;

        string sourceFilePath = Path.Combine(executingDirectory,"Resources/Data",fileName);
        File.Copy(sourceFilePath, destinationFilePath, true);
    }

    private static void MergeJson(JObject destination, JObject source)
    {
        foreach (var property in source.Properties())
        {
            if (destination[property.Name] == null)
            {
                destination[property.Name] = property.Value;
            }
            else if (property.Value.Type == JTokenType.Object)
            {
                MergeJson((JObject)destination[property.Name]!, (JObject)property.Value);
            }
        }
    }
}
