namespace worker.Workflows.Models
{
    // for workflows
    public record BookPayload(string City, string Day, int Person = 1);
    public record BookResult(bool Processed, int price);

    // for activities
    public record BookFlightRequest(string City, string Day, int Person = 1);
    public record BookFlightResult(bool Success, int Price);
    public record BookHotelRequest(string City, string Day, int Person = 1);
    public record BookHotelResult(bool Success, int Price);
    public record CancelFlightRequest(BookFlightResult Book);
}
