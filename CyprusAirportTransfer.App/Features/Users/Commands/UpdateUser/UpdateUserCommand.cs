using AutoMapper;
using CyprusAirportTransfer.Shared;
using CyprusAirportTransfer.App.Interfaces;
using CyprusAirportTransfer.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CyprusAirportTransfer.App.Features.Users.Commands.UpdateUser
{
    public record UpdateUserCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(command.Id);
            if (user != null)
            {
                user.UserName = command.UserName;
                user.Email = command.Email;
                user.UpdatedBy = 0;
                user.UpdatedDate = DateTime.Now;

                await _unitOfWork.Repository<User>().UpdateAsync(user);
                user.AddDomainEvent(new UserUpdatedEvent(user));

                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(user.Id, "User Updated.");
            }
            else
            {
                return await Result<int>.FailureAsync("User Not Found.");
            }
        }
    }
}
