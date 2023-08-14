﻿using AutoMapper;
using CyprusAirportTransfer.App.Common.Mappings;
using CyprusAirportTransfer.App.Interfaces;
using CyprusAirportTransfer.Core.Entities;
using CyprusAirportTransfer.Shared;
using MediatR;

namespace CyprusAirportTransfer.App.UseCases.Users.Commands.DeleteUser
{
    public record DeleteUserCommand : IRequest<Result<int>>, IMapFrom<User>
    {
        public int Id { get; set; }

        public DeleteUserCommand()
        {

        }

        public DeleteUserCommand(int id)
        {
            Id = id; 
        }
    }

    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var User = await _unitOfWork.Repository<User>().GetByIdAsync(command.Id);
            if (User != null)
            {
                await _unitOfWork.Repository<User>().DeleteAsync(User);
                User.AddDomainEvent(new UserDeletedEvent(User));

                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(User.Id, "User Deleted");
            }
            else
            {
                return await Result<int>.FailureAsync("User Not Found.");
            }
        }
    }
}
