﻿using CyprusAirportTransfer.Core.Commons;
using CyprusAirportTransfer.Core.Entities;

namespace CyprusAirportTransfer.App.Features.Users.Commands.DeleteUser
{
    public class UserDeletedEvent : BaseEvent
    {
        public User User { get; }

        public UserDeletedEvent(User User)
        {
            User = User;
        }
    }
}
