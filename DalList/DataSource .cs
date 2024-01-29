
using DO;

namespace Dal;

internal static class DataSource
{

    internal static class Config
    {
        internal const int startTaskId = 1;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }

        internal const int startDependenceId = 1;
        private static int nextDependenceId = startDependenceId;
        internal static int NextDependenceId { get => nextDependenceId++; }
    }

    internal static List<Dependence> Dependences { get; } = new();
    internal static List<Engineer> Engineers { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();
}
