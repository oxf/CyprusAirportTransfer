using CyprusAirportTransfer.App.Interfaces;

namespace CyprusAirportTransfer.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}