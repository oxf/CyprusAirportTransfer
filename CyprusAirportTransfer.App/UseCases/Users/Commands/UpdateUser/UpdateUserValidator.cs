using CyprusAirportTransfer.App.Interfaces.Repositories;
using FluentValidation;

namespace CyprusAirportTransfer.App.UseCases.Users.Commands.UpdateUser
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
            RuleFor(x => _userRepository.GetOtherUserByUsername(x.UserName, x.Id))
                .Null()
                .WithMessage("Username is already registered.");
            RuleFor(x => _userRepository.GetOtherUserByEmail(x.Email, x.Id))
                .Null()
                .WithMessage("Email is already registered.");
        }
    }
}
