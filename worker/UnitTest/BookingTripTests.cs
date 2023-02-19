using System.Threading.Tasks;
using Dapr.Workflow;
using Microsoft.DurableTask;
using Moq;
using worker.Workflows.Activities;
using worker.Workflows.Models;
using worker.Workflows;
using Xunit;

namespace WorkflowUnitTest
{
    [Trait("Example", "true")]
    public class BookingTripTests
    {
        [Fact]
        public async Task TestSuccess()
        {
            // Test payloads
            BookPayload payload = new(City: "Seoul", Day: "Saturday", Person: 2);
            Mock<WorkflowContext> mockContext = new();

            mockContext
                .Setup(ctx => ctx.CallActivityAsync<BookFlightResult>(nameof(BookFlightActivity), It.IsAny<BookFlightRequest>(), It.IsAny<TaskOptions>()))
                .Returns(Task.FromResult(new BookFlightResult(true, 100000)));
            mockContext
                .Setup(ctx => ctx.CallActivityAsync<BookHotelResult>(nameof(BookHotelActivity), It.IsAny<BookHotelRequest>(), It.IsAny<TaskOptions>()))
                .Returns(Task.FromResult(new BookHotelResult(true, 60000)));

            // Run the workflow directly
            BookResult result = await new BookingTripWorkflow().RunAsync(mockContext.Object, payload);
            
            // Verify that workflow result matches what we expect
            Assert.NotNull(result);
            Assert.True(result.Processed);
            Assert.Equal(160000, result.Price);
        }
    }
}
