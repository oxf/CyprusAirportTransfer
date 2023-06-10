using CyprusAirportTransfer.App.Features.Users.Commands.CreateUser;
using CyprusAirportTransfer.App.Features.Users.Commands.DeleteUser;
using CyprusAirportTransfer.App.Features.Users.Commands.UpdateUser;
using CyprusAirportTransfer.App.Features.Users.Queries.GetAllUsers;
using CyprusAirportTransfer.App.Features.Users.Queries.GetUserById;
using CyprusAirportTransfer.App.Features.Users.Queries.GetUsersWithPagination;
using CyprusAirportTransfer.App.Features.Users.Queries.GetUserWithPagination;
using CyprusAirportTransfer.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CyprusAirportTransfer.Web.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<GetAllUsersDto>>>> Get()
        {
            return await _mediator.Send(new GetAllUsersQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<GetUserByIdDto>>> GetUsersById(int id)
        {
            return await _mediator.Send(new GetUserByIdQuery(id));
        }

        [HttpGet]
        [Route("paged")]
        public async Task<ActionResult<PaginatedResult<GetUsersWithPaginationDto>>> GetUsersWithPagination([FromQuery]  GetUsersWithPaginationQuery query)
        {
            var validator = new GetUsersWithPaginationValidator();

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = validator.Validate(query);

            if (result.IsValid)
            {
                return await _mediator.Send(query);
            }

            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessages);
        }

        [HttpPost]
        public async Task<ActionResult<Result<int>>> Create(CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<int>>> Update(int id, UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<int>>> Delete(int id)
        {
            return await _mediator.Send(new DeleteUserCommand(id));
        }
    }
}
