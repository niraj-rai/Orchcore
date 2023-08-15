// See https://aka.ms/new-console-template for more information

using Orchcore;
using Orchcore.CLI.CodedOrchestrators;

//var data =  new { a = 1, b = 2 };
//var a = false;

//var state3 = new OrchestratorStateBase("state3", () => null);
//var state2 = new OrchestratorStateBase("state2", () => state3);
//var state1 = new OrchestratorStateBase("state1", () => a==true? state2 : state3 );

IOrchestrator orch = new SequentialOrchestratorTest();

//IOrchestrator orch = new WorkflowOrchestratorTest("Test Workflow Orchestrator");
orch.StateChanged += (result) =>
{
    Console.WriteLine($"State change success: ({result.PreviousState} -> {result.StateName})");
};
orch.StateChangeFailed += (result) => { Console.WriteLine($"State change failed: ({result.PreviousState} -> {result.StateName}) \nError : {result.Output}"); };

Console.WriteLine($"Running orchestrator: {orch}");
var result = await orch.RunAsync(stopOnError: false);
Console.WriteLine($"Finished running orchestrator: {orch}");

Console.ReadKey();
