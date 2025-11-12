using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Polls.Dtos;
using MediatR;

namespace Application.Features.Polls.Queries.GetAll
{
    internal class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<PollDto>>
    {
        public Task<IEnumerable<PollDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
