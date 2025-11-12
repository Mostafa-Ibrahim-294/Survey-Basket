using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Polls.Dtos;
using MediatR;

namespace Application.Features.Polls.Queries.GetAll
{
    public record GetAllQuery : IRequest<IEnumerable<PollDto>>;
}
