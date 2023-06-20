using CyprusAirportTransfer.App.Features.Login.Commands;
using CyprusAirportTransfer.App.Features.Users.Commands.CreateUser;
using CyprusAirportTransfer.Shared;
using CyprusAirportTransfer.Web.Controllers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CyprusAirportTransfer.API.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<Result<int>>> Login(PostLoginCommand command)
        {

            return await _mediator.Send(command);
        }
    }
}
