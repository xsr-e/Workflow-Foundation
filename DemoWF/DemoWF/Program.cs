using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace DemoWF
{

    class Program
    {
        static void Main(string[] args)
        {
            Activity workflowToRun = new SimpleWorkflow();
            WorkflowInvoker.Invoke(workflowToRun);
        }
    }
}
