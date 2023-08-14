using AutoMapper;
using AutoMapper.QueryableExtensions;
using CyprusAirportTransfer.App.Extensions;
using CyprusAirportTransfer.App.UseCases.Users.Queries.GetUserWithPagination;
using CyprusAirportTransfer.App.Interfaces;
using CyprusAirportTransfer.Core.Entities;
using CyprusAirportTransfer.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CyprusAirportTransfer.App.UseCases.Users.Queries.GetUsersWithPagination
{
    public record GetUsersWithPaginationQuery : IRequest<PaginatedResult<GetUsersWithPaginationDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetUsersWithPaginationQuery() { }

        public GetUsersWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQuery, PaginatedResult<GetUsersWithPaginationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsersWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetUsersWithPaginationDto>> Handle(GetUsersWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<User>().Entities
                   .OrderBy(x => x.UserName)
                   .ProjectTo<GetUsersWithPaginationDto>(_mapper.ConfigurationProvider)
                   .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}
