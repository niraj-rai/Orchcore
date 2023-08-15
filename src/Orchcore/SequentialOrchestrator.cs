using Orchcore.Models;

namespace Orchcore
{
    public class SequentialOrchestrator : OrchestratorBase
    {
        #region Events
        public override event Action<StateExecutionResult> StateChanged;
        public override event Action<StateExecutionResult> StateChangeFailed;
        #endregion

        #region Constructors
        public SequentialOrchestrator(string name, List<IOrchestratorState> states)
            : base(name, states)
        {
        }
        #endregion

        #region Public Methods
        public override async Task<StateExecutionResult> RunAsync(bool force = false, bool stopOnError = true)
        {
            StateExecutionResult stateExecutionResult = null;

            StateTransition result = (null, NextState.InputData);

            var index = 0;

            while (NextState != null)
            {
                var state = NextState;
                if (NextState != null)
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine($"--- Started transition to state: {state}");
                        result = await state.TransitionFunc.Invoke(result.inputData);
                        stateExecutionResult = new StateExecutionResult(stateId: state.Id, stateName: state.Name, output: null, _currentState?.Name);
                        StateChanged?.Invoke(stateExecutionResult);
                        System.Diagnostics.Debug.WriteLine($"--- Finished transition to state: {state}");
                    }
                    catch (Exception ex)
                    {
                        var error = $"error : {ex.Message}, stack trace: {ex.StackTrace}";
                        System.Diagnostics.Debug.WriteLine($"--- Failed transition to state: {state} with error: {error}");
                        stateExecutionResult = new StateExecutionResult(stateId: state.Id, stateName: state.Name, output: error, _currentState?.Name);
                        StateChangeFailed?.Invoke(stateExecutionResult);
                        if (stopOnError) break;
                    }
                    _currentState = NextState;
                    index += 1;
                    _nextState = _states.ElementAtOrDefault(index);
                    if (_nextState != null && NextState.Id == CurrentState.Id)
                        _nextState = null;
                }
            }

            return await Task.FromResult(stateExecutionResult);
        }
        #endregion
    }
}
