namespace Orchcore
{
    public interface IOrchestratorState
    {
        string Id { get; }
        string Name { get; }
        NextStateTransition TransitionFunc { get; }
        bool IsFinal { get; }
        dynamic InputData { get; }
    }
}
