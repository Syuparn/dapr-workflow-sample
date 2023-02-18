using System;
using System.Threading.Tasks;
using Dapr.Workflow;
using worker.Workflows.Models;

namespace worker.Workflows.Activities {
    class BookHotelActivity : WorkflowActivity<BookHotelRequest, BookHotelResult> {
        public override async Task<BookHotelResult> RunAsync(WorkflowActivityContext context, BookHotelRequest req)
        {
            Console.WriteLine("BookHotelActivity started: city={0} day={1} person={2}", req.City, req.Day, req.Person);

            await Task.Delay(5000);
            int price;
            try {
                price = this.Price(req.Day) * req.Person;
            } catch (Exception e) {
                Console.WriteLine("BookHotelActivity failed: {0}", e);
                return new BookHotelResult(/* success: */ false, 0);
            }

            Console.WriteLine("BookHotelActivity completed: price={2}", price);

            return new BookHotelResult(/* success: */ true, price);
        }

        private int Price(string day) {
            switch (day)
            {
                case "Friday":
                    throw new Exception("No Vacancy!");
                case "Saturday":
                    return 30000;
                default:
                    return 15000;
            }
        }
    }
}
