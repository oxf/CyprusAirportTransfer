using CyprusAirportTransfer.App.Interfaces;
using CyprusAirportTransfer.App.Interfaces.Repositories;
using CyprusAirportTransfer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CyprusAirportTransfer.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IGenericRepository<User> _repository;

        public UserRepository(IGenericRepository<User> repository)
        {
            _repository = repository;
        }
    }
}
