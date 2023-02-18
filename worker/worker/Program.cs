using Dapr.Client;
using Dapr.Workflow;
using worker.Workflows.Activities;
using worker.Workflows;

// prepare workflow worker
// The workflow host is a background service that connects to the sidecar over gRPC
var builder = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddDaprWorkflow(options =>
    {
        options.RegisterWorkflow<BookingTripWorkflow>();
        // These are the activities that get invoked by the workflow(s).
        options.RegisterActivity<BookFlightActivity>();
        options.RegisterActivity<BookHotelActivity>();
        options.RegisterActivity<CancelFlightActivity>();
    });
});

// k8s Dapr sidecar uses port 50001
Environment.SetEnvironmentVariable("DAPR_GRPC_PORT", "50001");

// Start the app - this is the point where we connect to the Dapr sidecar to
// listen for workflow work-items to execute.
using var host = builder.Build();
host.Start();

using var daprClient = new DaprClientBuilder().Build();

// Wait for the sidecar to become available
while (!await daprClient.CheckHealthAsync())
{
    Console.WriteLine("wait for sidecar...");
    Thread.Sleep(TimeSpan.FromSeconds(5));
}
Console.WriteLine("connected to sidecar");


while (true) {
    // loop infinitely to wait workflows
}
