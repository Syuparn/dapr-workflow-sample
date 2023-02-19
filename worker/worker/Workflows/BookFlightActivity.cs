using System.Threading.Tasks;
using Dapr.Workflow;
using worker.Workflows.Models;

namespace worker.Workflows.Activities {
    public class BookFlightActivity : WorkflowActivity<BookFlightRequest, BookFlightResult> {
        public override async Task<BookFlightResult> RunAsync(WorkflowActivityContext context, BookFlightRequest req)
        {
            Console.WriteLine("BookflightActivity started: city={0} day={1} person={2}", req.City, req.Day, req.Person);

            await Task.Delay(500);
            int price = this.TicketPrice(req.City) * req.Person;

            Console.WriteLine("BookflightActivity completed: price={0}", price);

            return new BookFlightResult(/* success: */ true, price);
        }

        private int TicketPrice(string city) {
            switch (city)
            {
                case "Tokyo":
                    return 0;
                case "Seoul":
                    return 50000;
                default:
                    return 100000;
            }
        } 
    }
}
