namespace Orchcore
{
    public delegate Task<StateTransition> NextStateTransition(dynamic data);

    public abstract class OrchestratorStateBase : IOrchestratorState
    {
        private readonly string _stateId;
        private readonly string _stateName;
        protected readonly NextStateTransition _transitionFunc;
        private readonly bool _isFinalState;
        private readonly dynamic _inputData;
        public OrchestratorStateBase(string stateName, NextStateTransition transitionFunc, dynamic inputData = null, bool isFinalState = false)
        {
            _stateId = Guid.NewGuid().ToString();
            _stateName = stateName;
            _transitionFunc = transitionFunc;
            _inputData = inputData;
            _isFinalState = isFinalState;
        }

        public string Id => _stateId;
        public string Name => _stateName;
        public virtual NextStateTransition TransitionFunc => _transitionFunc;
        public bool IsFinal => _isFinalState;
        public dynamic InputData => _inputData;
        public override string ToString()
        {
            return $"State(id = {_stateId}, name={_stateName})";
        }
    }

    public class SequentialState : OrchestratorStateBase
    {
        public SequentialState(string stateName, NextStateTransition transitionFunc, object inputData = null, bool isFinalState = false)
            : base(stateName, transitionFunc, inputData, isFinalState)
        {
        }
    }

    public class WorkflowState : OrchestratorStateBase
    {
        public WorkflowState(string stateName, NextStateTransition transitionFunc, object inputData = null, bool isFinalState = false)
            : base(stateName, transitionFunc, inputData, isFinalState)
        {
        }
    }

    public class InitialState : OrchestratorStateBase
    {
        public InitialState(string stateName, NextStateTransition transitionFunc)
                    : base(stateName, transitionFunc)
        {
        }
    }

    public record struct StateTransition(IOrchestratorState nextState, dynamic inputData)
    {
        public static implicit operator (IOrchestratorState nextState, dynamic inputData)(StateTransition value)
        {
            return (value.nextState, value.inputData);
        }

        public static implicit operator StateTransition((IOrchestratorState nextState, dynamic inputData) value)
        {
            return new StateTransition(value.nextState, value.inputData);
        }
    }
}
