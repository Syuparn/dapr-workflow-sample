using System.Threading.Tasks;
using Dapr.Workflow;
using worker.Workflows.Models;

namespace worker.Workflows.Activities {
    class CancelFlightActivity : WorkflowActivity<CancelFlightRequest, object> {
        public override async Task<object> RunAsync(WorkflowActivityContext context, CancelFlightRequest req)
        {
            Console.WriteLine("CancelflightActivity started");

            await Task.Delay(5000);

            Console.WriteLine("CancelflightActivity completed");

            return null;
        }
    }
}
