using CyprusAirportTransfer.App.Common.Mappings;
using CyprusAirportTransfer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CyprusAirportTransfer.App.Features.Users.Queries.GetUserWithPagination
{
    public class GetUsersWithPaginationDto : IMapFrom<User>
    {
        public int Id { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public int DisplayOrder { get; init; }
    }
}
