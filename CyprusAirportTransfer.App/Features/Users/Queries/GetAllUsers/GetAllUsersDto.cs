using CyprusAirportTransfer.App.Common.Mappings;
using CyprusAirportTransfer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyprusAirportTransfer.App.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersDto : IMapFrom<User>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
