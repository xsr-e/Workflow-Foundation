using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace _03_WorkflowApplication
{

    class Program
    {

        static void Main(string[] args)
        {

            Activity workflowToRun = new SimpleWorkflow();

            WorkflowApplication wfApp = new WorkflowApplication(workflowToRun);

            wfApp.Completed = (e) =>
            {
                switch (e.CompletionState)
                {
                    case ActivityInstanceState.Canceled:
                        Console.WriteLine("Workflow {0} Canceled.", e.InstanceId);
                        break;
                    case ActivityInstanceState.Closed:
                        Console.WriteLine("Workflow {0} Closed.", e.InstanceId);
                        break;
                    case ActivityInstanceState.Executing:
                        Console.WriteLine("Workflow {0} Executing.", e.InstanceId);
                        break;
                    case ActivityInstanceState.Faulted:
                        Console.WriteLine("Workflow {0} Terminated.", e.InstanceId);
                        Console.WriteLine("Exception: {0}\n{1}",
                            e.TerminationException.GetType().FullName,
                            e.TerminationException.Message);
                        break;
                    default:
                        Console.WriteLine("Workflow {0} Completed.", e.InstanceId);
                        break;
                }
            };

            wfApp.Aborted = (e) =>
            {
                Console.WriteLine("Workflow {0} Aborted.", e.InstanceId);
                Console.WriteLine("Exception: {0}\n{1}",
                    e.Reason.GetType().FullName,
                    e.Reason.Message);
            };

            wfApp.Idle = (e) =>
            {
                Console.WriteLine("Workflow {0} Idle.", e.InstanceId);
            };

            wfApp.PersistableIdle = (e) =>
            {
                return PersistableIdleAction.Unload;
            };

            wfApp.Unloaded = (e) =>
            {
                Console.WriteLine("Workflow {0} Unloaded.", e.InstanceId);
            };

            wfApp.OnUnhandledException = (e) =>
            {

                Console.WriteLine("OnUnhandledException in Workflow {0}\n{1}",
                    e.InstanceId, e.UnhandledException.Message);

                Console.WriteLine("ExceptionSource: {0} - {1}",
                    e.ExceptionSource.DisplayName, e.ExceptionSourceInstanceId);

                return UnhandledExceptionAction.Cancel;
            };


            wfApp.Run();
            
            Console.ReadLine();
        }
    }
}
