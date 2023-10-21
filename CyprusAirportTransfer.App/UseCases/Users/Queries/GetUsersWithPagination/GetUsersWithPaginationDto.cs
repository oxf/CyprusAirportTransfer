using CyprusAirportTransfer.App.Common.Mappings;
using CyprusAirportTransfer.Core.Entities;

namespace CyprusAirportTransfer.App.UseCases.Users.Queries.GetUserWithPagination
{
    public class GetUsersWithPaginationDto : IMapFrom<User>
    {
        public int Id { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public int DisplayOrder { get; init; }

        public GetUsersWithPaginationDto()
        {

        }
    }
}
