using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace Core;

public class PipelineConfig
{
    public ReadOnlyCollection<Stage> Pipeline { get; }

    public PipelineConfig()
    {
        Pipeline = new ReadOnlyCollection<Stage>([]);
    }

    public PipelineConfig(IEnumerable<Stage> stages)
    {
        Pipeline = new ReadOnlyCollection<Stage>(stages is List<Stage> list ? list : [.. stages]);
    }
}

public class Stage
{
    public string Name { get; set; } = "";
    public string Command { get; set; } = "";
    public string Args { get; set; } = "";
    public bool StopOnFailure { get; set; }
}