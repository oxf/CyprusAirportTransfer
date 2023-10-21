using CyprusAirportTransfer.App.UseCases.Users.Commands.DeleteUser;
using CyprusAirportTransfer.App.UseCases.Users.Commands.CreateUser;
using CyprusAirportTransfer.App.UseCases.Users.Commands.UpdateUser;
using CyprusAirportTransfer.App.UseCases.Users.Queries.GetAllUsers;
using CyprusAirportTransfer.App.UseCases.Users.Queries.GetUserById;
using CyprusAirportTransfer.App.UseCases.Users.Queries.GetUsersWithPagination;
using CyprusAirportTransfer.App.UseCases.Users.Queries.GetUserWithPagination;
using CyprusAirportTransfer.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CyprusAirportTransfer.Web.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateUserCommand> _createUserValidator;

        public UserController(IMediator mediator, IValidator<CreateUserCommand> createUserValidator)
        {
            _mediator = mediator;
            _createUserValidator = createUserValidator;
        }

        [HttpGet]
        public async Task<Result<List<GetAllUsersDto>>> Get()
        {
            return await _mediator.Send(new GetAllUsersQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<GetUserByIdDto>>> GetUsersById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            if (result.Data == null)
            {
                return NotFound();
            }
            else return result;
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
                return Ok(new Result<PaginatedResult<GetUsersWithPaginationDto>> { Succeeded = true, Data = await _mediator.Send(query)});
            }

            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessages);
        }

        [HttpPost]
        public async Task<ActionResult<Result<int>>> Create(CreateUserCommand command)
        {

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = _createUserValidator.Validate(command);

            if (result.IsValid)
            {
                return await _mediator.Send(command);
            }

            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessages);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<int>>> Update(int id, UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var res = await _mediator.Send(command);
            return res;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<int>>> Delete(int id)
        {
            return await _mediator.Send(new DeleteUserCommand(id));
        }
    }
}
