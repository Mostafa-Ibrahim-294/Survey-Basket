using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.Polls.Dtos;
using MediatR;

namespace Application.Features.Polls.Queries.GetAll
{
    public record GetAllQuery(string? Search, string? sortBy, SortDirection SortDirection,
        int PageNumber = 1 , int PageSize = 10 ) : IRequest<PageResult<PollDto>>;
}
