using AutoMapper;
using AutoMapper.QueryableExtensions;
using CyprusAirportTransfer.Shared;
using CyprusAirportTransfer.App.Interfaces;
using CyprusAirportTransfer.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CyprusAirportTransfer.App.Features.Users.Queries.GetAllUsers
{
    public record GetAllUsersQuery : IRequest<Result<List<GetAllUsersDto>>>;

    internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<GetAllUsersDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllUsersDto>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var Users = await _unitOfWork.Repository<User>().Entities
                   .ProjectTo<GetAllUsersDto>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);

            return await Result<List<GetAllUsersDto>>.SuccessAsync(Users);
        }
    }
}
