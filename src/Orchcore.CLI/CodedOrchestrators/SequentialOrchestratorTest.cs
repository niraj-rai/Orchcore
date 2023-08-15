namespace Orchcore.CLI.CodedOrchestrators
{
    public class SequentialOrchestratorTest : SequentialOrchestrator
    {
        bool b = true;
        public SequentialOrchestratorTest(string name = nameof(SequentialOrchestratorTest))
            : base(name, null)
        {

            var state1 = new SequentialState(nameof(State1), State1, inputData: "Initial State Data");
            var state2 = new SequentialState("state2", State2);
            var state3 = new SequentialState("state3", (input) => ToFinalState());

            Initialize(state1, state2, state3);

        }

        private Task<StateTransition> State1(dynamic input)
        {
            var nextState = b == true ? "state2" : "state3";
            return ToState(nextState, "Data from state 1");
        }

        private Task<StateTransition> State2(dynamic input)
        {
            throw new Exception();
            // return ToFinalState();
        }

        private IOrchestratorState State3()
        {
            return null;
        }
    }
}
