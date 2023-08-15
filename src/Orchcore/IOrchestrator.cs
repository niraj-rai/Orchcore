using Orchcore.Models;

namespace Orchcore
{
    public interface IOrchestrator
    {
        #region Properties
        string Id { get; }
        string Name { get; }
        object Context { get; }
        int Version { get; }

        IOrchestratorState CurrentState { get; }
        IOrchestratorState NextState { get; }
        #endregion

        #region Events
        event Action<StateExecutionResult> StateChanged;
        event Action<StateExecutionResult> StateChangeFailed;
        #endregion

        #region Methods
        Task<StateExecutionResult> RunAsync(bool force = false, bool stopOnError = true);
        Task<IReadOnlyCollection<IOrchestratorState>> GetStatesAsync();
        IOrchestratorState GetState(string stateName);
        Task<bool> AcquireLock(TimeSpan timeout);
        Task ReleaseLock();
        #endregion
    }
}
