using System.Diagnostics.Contracts;

namespace Orchcore
{
    public interface IOrchestratorBuilder
    {
        IOrchestratorBuilder Name(string name);
        IOrchestratorBuilder InitialState(InitialState state);
        IOrchestratorBuilder InitialState(string name, NextStateTransition stateDelegate);
        IOrchestratorBuilder NextState(IOrchestratorState state);
        IOrchestratorBuilder NextState(string name, NextStateTransition stateDelegate);
        IOrchestrator Build(bool sequential = true);
    }

    public class OrchestratorBuilder : IOrchestratorBuilder
    {
        protected string OrchestratorName { get; set; }
        protected Func<object, object> stateDelegate;
        protected IOrchestratorState initialState;
        protected IOrchestrator orchestrator;
        protected List<IOrchestratorState> States { get; set; }

        public IOrchestratorBuilder InitialState(InitialState state)
        {
            initialState = state;
            return this;
        }

        public IOrchestratorBuilder InitialState(string name, NextStateTransition stateDelegate)
        {
            initialState = new InitialState(name, stateDelegate);
            return this;
        }

        public IOrchestratorBuilder Name(string name)
        {
            OrchestratorName = name;
            return this;
        }

        public IOrchestratorBuilder NextState(IOrchestratorState state)
        {
            throw new NotImplementedException();
        }

        public IOrchestratorBuilder NextState(string name, NextStateTransition stateDelegate)
        {
            States.Add(new SequentialState(name, stateDelegate));
            return this;
        }

        public IOrchestrator Build(bool sequential = true)
        {
            IOrchestrator orch = sequential
                ? new SequentialOrchestrator(OrchestratorName, States)
                : new WorkflowOrchestrator(OrchestratorName, States);

            return orch;
        }
    }
}
