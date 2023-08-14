using CyprusAirportTransfer.App.UseCases.Users.Queries.GetUserById;
using CyprusAirportTransfer.App.UseCases.Users.Commands.UpdateUser;
using CyprusAirportTransfer.App.UseCases.Users.Queries.GetAllUsers;
using CyprusAirportTransfer.Shared;
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

        public async Task<Result<GetUserByIdDto>> GetUserByIdAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<Result<GetUserByIdDto>>($"{httpClient.BaseAddress}/{id}");
        }

        public async Task<HttpResponseMessage> UpdateUser(UpdateUserCommand updateUserCommand)
        {
            return await httpClient.PutAsJsonAsync<UpdateUserCommand>($"{httpClient.BaseAddress}/{updateUserCommand.Id}", updateUserCommand);
        }
    }
}
