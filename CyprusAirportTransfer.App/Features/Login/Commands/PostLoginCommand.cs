using AutoMapper;
using CyprusAirportTransfer.App.Features.Users.Commands.CreateUser;
using CyprusAirportTransfer.App.Interfaces;
using CyprusAirportTransfer.App.Interfaces.Repositories;
using CyprusAirportTransfer.Core.Entities;
using CyprusAirportTransfer.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyprusAirportTransfer.App.Features.Login.Commands
{
    public record class PostLoginCommand : IRequest<Result<int>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    internal class PostLoginCommandHandler : IRequestHandler<PostLoginCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IHashingService _hashingService;

        public PostLoginCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, IHashingService hashingService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
            _hashingService = hashingService;
        }

        public async Task<Result<int>> Handle(PostLoginCommand request, CancellationToken cancellationToken)
        {
            User userToLogin = _userRepository.GetUserByUsername(request.Username);
            byte[] hashedPassword = await _hashingService.Encrypt(request.Password, userToLogin.Salt, cancellationToken);
            if (Enumerable.SequenceEqual(hashedPassword,userToLogin.Password))
            {
                //ok
                return await Result<int>.SuccessAsync(1, "Logged in");
            } else
            {
                //fail
                return await Result<int>.FailureAsync(0, "Login failed");
            }
        }
    }
}
