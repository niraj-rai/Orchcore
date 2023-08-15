namespace Orchcore.CLI.CodedOrchestrators
{
    public class WorkflowOrchestratorTest : WorkflowOrchestrator
    {
        bool b = true;
        public WorkflowOrchestratorTest(string name = nameof(WorkflowOrchestratorTest))
            : base(name, null)
        {

            var state1 = new WorkflowState(nameof(State1), State1, inputData: "Initial State Data");
            var state2 = new WorkflowState("state2", State2);
            var state3 = new WorkflowState("state3", (input) => ToFinalState());

            Initialize(state1, state2, state3);

        }

        private Task<StateTransition> State1(dynamic input)
        {
            var nextState = b == true ? "state2" : "state3";
            return ToState(nextState, "Date from state1");
        }

        private Task<StateTransition> State2(dynamic input)
        {
            throw new Exception();
            //return ToState("state3");
        }

        private IOrchestratorState State3()
        {
            return null;
        }
    }
}
