using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Polls.Dtos;
using MediatR;

namespace Application.Features.Polls.Queries.GetCurrent
{
    public record GetCurrentQuery : IRequest<IEnumerable<PollDto>>;
}
