﻿using CyprusAirportTransfer.App.Interfaces.Repositories;
using FluentValidation;

namespace CyprusAirportTransfer.App.Features.Users.Commands.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        IUserRepository _userRepository;
        public UpdateUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email should be in a correct format.");
            RuleFor(x => _userRepository.GetUserByUsername(x.UserName))
                .Null()
                .WithMessage("Username is already registered.");
            RuleFor(x => _userRepository.GetUserByEmail(x.Email))
                .Null()
                .WithMessage("Email is already registered.");
        }
    }
}
