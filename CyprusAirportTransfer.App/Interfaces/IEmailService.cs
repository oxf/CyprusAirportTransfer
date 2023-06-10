
using CyprusAirportTransfer.App.DTOs.Email;

namespace CyprusAirportTransfer.App.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequestDto request);
    }
}
