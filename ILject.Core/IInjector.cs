namespace ILject.Core
{
    public interface IInjector
    {
        // This number determines when this injector gets run
        // The higher the execution priority, the earlier it runs.
        int ExecutionPriority { get; set; }
    }
}
