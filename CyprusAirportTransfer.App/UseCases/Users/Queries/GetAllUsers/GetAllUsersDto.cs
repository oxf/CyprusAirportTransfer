using CyprusAirportTransfer.App.Common.Mappings;
using CyprusAirportTransfer.Core.Entities;

namespace CyprusAirportTransfer.App.UseCases.Users.Queries.GetAllUsers
{
    public class GetAllUsersDto : IMapFrom<User>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
