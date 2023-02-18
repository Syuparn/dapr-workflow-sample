using Dapr.Workflow;
using DurableTask.Core.Exceptions;
using worker.Workflows.Activities;
using worker.Workflows.Models;

namespace worker.Workflows
{
    public class BookingTripWorkflow : Workflow<BookPayload, BookResult>
    {
        public override async Task<BookResult> RunAsync(WorkflowContext context, BookPayload payload)
        {
            string id = context.InstanceId;

            // STEP1: book flights
            BookFlightRequest bookFlightRequest = new BookFlightRequest(payload.City, payload.Day, payload.Person);
            BookFlightResult bookFlightResult = await context.CallActivityAsync<BookFlightResult>(
                nameof(BookFlightActivity), bookFlightRequest);
            if (!bookFlightResult.Success) {
                return new BookResult(/* processed: */ false, 0);
            }

            // STEP2: book a hotel
            BookHotelRequest bookHotelRequest = new BookHotelRequest(payload.City, payload.Day, payload.Person);
            BookHotelResult bookHotelResult = await context.CallActivityAsync<BookHotelResult>(
                nameof(BookHotelActivity), bookHotelRequest);
            if (!bookFlightResult.Success) {
                // conpensating transactions
                CancelFlightRequest cancelFlightRequest = new CancelFlightRequest(bookFlightResult);
                await context.CallActivityAsync(nameof(CancelFlightActivity), cancelFlightRequest);
                return new BookResult(/* processed: */ false, 0);
            }

            return new BookResult(/* processed: */ true, bookFlightResult.Price + bookHotelResult.Price);
        }
    }
}
