using CyprusAirportTransfer.App.Interfaces;
using CyprusAirportTransfer.Core.Entities;
using MediatR;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CyprusAirportTransfer.App.Common.Mappings;
using CyprusAirportTransfer.Shared;

namespace CyprusAirportTransfer.App.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<Result<int>>, IMapFrom<User>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }

    internal class CreatePlayerCommandHandler : IRequestHandler<CreateUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePlayerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                UserName = command.UserName,
                Email = command.Email,
                Password = command.Password,
                Salt = command.Salt,
                CreatedBy = 0,
                CreatedDate = DateTime.Now,
                UpdatedBy = 0,
                UpdatedDate = DateTime.Now
            };

            await _unitOfWork.Repository<User>().AddAsync(user);
            user.AddDomainEvent(new UserCreatedEvent(user));
            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(user.Id, "User Created.");
        }
    }
}
