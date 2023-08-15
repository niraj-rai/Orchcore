using System.Runtime.Serialization;

namespace Orchcore.Models
{
    public class StateExecutionResult : ModelBase
    {
        public StateExecutionResult(string stateId, string stateName, dynamic output, string previousState)
        {
            StateId = stateId;
            StateName = stateName;
            Output = output;
            PreviousState = previousState;
        }

        public string StateId { get; }
        public string StateName { get; }
        public dynamic Output { get; }
        public string PreviousState { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("StateId", StateId);
            info.AddValue("StateName", StateName);
            info.AddValue("Output", Output);
            info.AddValue("PreviousState", PreviousState);
        }
    }

}
