using CyprusAirportTransfer.App.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CyprusAirportTransfer.App.UseCases.Users.Commands.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        IUserRepository _userRepository;
        public CreateUserValidator(IUserRepository userRepository)
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
