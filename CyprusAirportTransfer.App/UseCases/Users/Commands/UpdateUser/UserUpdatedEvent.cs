using CyprusAirportTransfer.Core.Commons;
using CyprusAirportTransfer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CyprusAirportTransfer.App.Features.Users.Commands.UpdateUser
{
    public class UserUpdatedEvent : BaseEvent
    {
        public User user { get; }

        public UserUpdatedEvent(User user)
        {
            this.user = user;
        }
    }
}
