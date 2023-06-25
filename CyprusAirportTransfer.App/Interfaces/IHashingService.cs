
using CyprusAirportTransfer.App.DTOs.Email;

namespace CyprusAirportTransfer.App.Interfaces
{
    public interface IHashingService
    {
        public Task<byte[]> Encrypt(string stringToEncrypt, byte[] salt, CancellationToken cancellationToken);
        public Task<byte[]> GenerateSalt();
    }
}
