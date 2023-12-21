namespace Orleans.Utilities.Prometheus;

public class GrainMetricsConfiguration
{
    public HashSet<string> GrainDotMethodsBlackList { get; set; } = new HashSet<string>();
    public HashSet<string> GrainBlackList { get; set; } = new HashSet<string>();
    public HashSet<string> MethodBlackList { get; set; } = new HashSet<string>();
}