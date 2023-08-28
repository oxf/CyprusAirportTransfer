using CyprusAirportTransfer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyprusAirportTransfer.App.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public User GetUserByUsername(string username);

        public User GetUserByEmail(string email);

        public User GetOtherUserByUsername(string username, int userId);

        public User GetOtherUserByEmail(string email, int userId);
    }
}
