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

        public User GetUserByUsername(string username) =>
            _repository.Entities.Where(x => x.UserName == username).FirstOrDefault();

        public User GetUserByEmail(string email) =>
            _repository.Entities.Where(x => x.Email == email).FirstOrDefault();

        public User GetOtherUserByUsername(string username, int userId) =>
            _repository.Entities.Where(x => x.UserName == username && x.Id != userId).FirstOrDefault();


        public User GetOtherUserByEmail(string email, int userId) =>
            _repository.Entities.Where(x => x.Email == email && x.Id != userId).FirstOrDefault();

    }
}
