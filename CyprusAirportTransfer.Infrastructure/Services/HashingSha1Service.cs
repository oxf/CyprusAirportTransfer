using CyprusAirportTransfer.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CyprusAirportTransfer.Infrastructure.Services
{
    internal class HashingService : IHashingService
    {
        Rfc2898DeriveBytes _pbkdf2 = null;
        public HashingService() { 
        }

        public async Task<byte[]> Encrypt(string stringToEncrypt, byte[] salt, CancellationToken cancellationToken)
        {
            if(_pbkdf2 != null)
            {
                _pbkdf2.Reset();
            }
            _pbkdf2 = new Rfc2898DeriveBytes(Encoding.ASCII.GetBytes(stringToEncrypt), salt, iterations: 5000);
            return _pbkdf2.GetBytes(20); //20 bytes length is 160 bits
        }

        public async Task<byte[]> GenerateSalt()
        {
            byte[] salt = new byte[32];
            Random r = new Random();
            r.NextBytes(salt);
            return salt;
        }
    }
}
