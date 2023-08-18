using CyprusAirportTransfer.App.UseCases.Users.Queries.GetUserById;
using CyprusAirportTransfer.App.UseCases.Users.Commands.UpdateUser;
using CyprusAirportTransfer.App.UseCases.Users.Queries.GetAllUsers;
using CyprusAirportTransfer.Shared;
using System.Net.Http.Json;
using CyprusAirportTransfer.App.UseCases.Users.Commands.CreateUser;

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

        public async Task<Result<int>?> UpdateUser(UpdateUserCommand updateUserCommand)
        {
            var response = await httpClient.PutAsJsonAsync<UpdateUserCommand>($"{httpClient.BaseAddress}/{updateUserCommand.Id}", updateUserCommand);
            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content to get the Result<int> value
                var result = await response.Content.ReadFromJsonAsync<Result<int>>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<HttpResponseMessage> CreateUser(CreateUserCommand createUserCommand)
        {
            return await httpClient.PostAsJsonAsync<CreateUserCommand>($"{httpClient.BaseAddress}", createUserCommand);
        }

        public async Task<Result<int>?> DeleteUser(int id)
        {
            var response = await httpClient.DeleteAsync($"{httpClient.BaseAddress}/{id}");
            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content to get the Result<int> value
                var result = await response.Content.ReadFromJsonAsync<Result<int>>();
                return result;
            }
            else
            {
                return null;
            }

        }
    }
}
