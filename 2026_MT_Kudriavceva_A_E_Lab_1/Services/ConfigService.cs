using System.IO;
using System.Text.Json;

namespace Core;
public class ConfigService
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public PipelineConfig Load(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException();

        var json = File.ReadAllText(path);

        var config = JsonSerializer.Deserialize<PipelineConfig>(json, _jsonOptions);

        if (config == null || config.Pipeline == null || config.Pipeline.Count == 0)
            throw new InvalidDataException();

        return config;
    }
}