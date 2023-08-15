using Orchcore.Models;

namespace Orchcore
{
    public abstract class OrchestratorBase : IOrchestrator
    {
        #region Members
        private readonly string _id;
        private readonly string _name;
        protected List<IOrchestratorState> _states;
        protected IOrchestratorState _currentState;
        protected IOrchestratorState _nextState;
        #endregion

        #region Constructors
        protected OrchestratorBase(string name, List<IOrchestratorState> states)
        {
            _id = Guid.NewGuid().ToString();
            _name = name ?? GetType().Name;
            _states = states;
            _nextState = _states?.First();
            _currentState = new InitialState("Initial State", _nextState?.TransitionFunc);
        }
        #endregion

        #region Properties
        public string Id => _id;
        public string Name => _name;
        public object Context => null;
        public IOrchestratorState CurrentState => _currentState;
        public IOrchestratorState NextState => _nextState;

        public int Version => throw new NotImplementedException();
        #endregion

        #region Events
        public abstract event Action<StateExecutionResult> StateChanged;
        public abstract event Action<StateExecutionResult> StateChangeFailed;
        #endregion

        #region Public Methods
        protected virtual void Initialize(params IOrchestratorState[] states)
        {
            _states = states?.ToList() ?? throw new ArgumentNullException(nameof(states));
            _currentState = new InitialState("Initial State", _nextState?.TransitionFunc);
            _nextState = _states?.First();
        }
        protected virtual Task<bool> AcquireLock()
        {
            System.Diagnostics.Debug.WriteLine("Acquiring Lock");
            return Task.FromResult(true);
        }

        public abstract Task<StateExecutionResult> RunAsync(bool force = false, bool stopOnError = true);

        public async Task<IReadOnlyCollection<IOrchestratorState>> GetStatesAsync()
        {
            return await Task.FromResult(_states.AsReadOnly());
        }
        public IOrchestratorState GetState(string name)
        {
            return _states?.FirstOrDefault(s => s.Name == name);
        }

        public override string ToString()
        {
            return $"Orchestrator(id = {_id}, name={_name})";
        }
        #endregion

        protected virtual Task<StateTransition> ToState(string name, dynamic data = null)
        {
            return Task.FromResult<StateTransition>((GetState(name), data));
        }

        protected virtual Task<StateTransition> ToFinalState(dynamic data = null)
        {
            return Task.FromResult<StateTransition>((null, data));
        }

        public Task<bool> AcquireLock(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public Task ReleaseLock()
        {
            throw new NotImplementedException();
        }

        #region Helper methods
        private List<IOrchestratorState> LoadStates() { return null; }
        #endregion
    }
}
