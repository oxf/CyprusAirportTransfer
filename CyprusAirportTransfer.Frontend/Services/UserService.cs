using CyprusAirportTransfer.App.Features.Users.Queries.GetAllUsers;
using CyprusAirportTransfer.Core.Entities;
using CyprusAirportTransfer.Shared;
using CyprusAirportTransfer.Shared.Interfaces;
using System.Net.Http.Json;

namespace CyprusAirportTransfer.Frontend.Services
{
    public class UserService
    {
        private readonly HttpClient httpClient;

        public UserService()
        {
            this.httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7193/api/User");
        }

        public async Task<Result<List<GetAllUsersDto>>> GetUsersAsync()
        {
            return await httpClient.GetFromJsonAsync<Result<List<GetAllUsersDto>>>(httpClient.BaseAddress);
        }
    }
}
