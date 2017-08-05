using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace _02_Basic_Code_Workflow
{

    class Program
    {
        static void Main(string[] args)
        {
            Activity workflowToRun = new Sequence
            {
                Activities =
                {
                    new WriteLine {
                        Text = "Starting the workflow."
                    }
                   ,new Delay{
                        Duration = TimeSpan.FromSeconds(5)
                    }
                    ,new WriteLine{
                        Text = "Ending the workflow."
                    }
                }
                
            };
            
            WorkflowInvoker.Invoke(workflowToRun);
        }

    }
}
