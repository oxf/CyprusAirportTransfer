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
using System.Collections;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CyprusAirportTransfer.App.UseCases.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<Result<int>>, IMapFrom<User>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    internal class CreatePlayerCommandHandler : IRequestHandler<CreateUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHashingService _hashingService;
        public CreatePlayerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHashingService hashingService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hashingService = hashingService;
        }

        public async Task<Result<int>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            byte[] salt = await _hashingService.GenerateSalt();
            byte[] hashedPassword = await _hashingService.Encrypt(command.Password, salt, cancellationToken);
            var user = new User()
            {
                UserName = command.UserName,
                Email = command.Email,
                Password = hashedPassword,
                Salt = salt,
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
