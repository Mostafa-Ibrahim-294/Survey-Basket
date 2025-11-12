using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Polls.Dtos;
using MediatR;

namespace Application.Features.Polls.Queries.GetById
{
    internal class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, PollDto>
    {
        public Task<PollDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}