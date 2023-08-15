using System.Runtime.Serialization;

namespace Orchcore.Models
{
    public class OrchestratorExecutionResult : ModelBase
    {
        public OrchestratorExecutionResult(string orchestratorId, string orchestratorName, dynamic output, List<StateExecutionResult> stateExecutionResults)
        {
            OrchestratorId = orchestratorId;
            OrchestratorName = orchestratorName;
            Output = output;
            StateExecutionResults = stateExecutionResults;
        }

        public string OrchestratorId { get; }
        public string OrchestratorName { get; }
        public dynamic Output { get; }
        public List<StateExecutionResult> StateExecutionResults { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("OrchestratorId", OrchestratorId);
            info.AddValue("OrchestratorName", OrchestratorName);
            info.AddValue("Output", Output);
        }
    }

}
