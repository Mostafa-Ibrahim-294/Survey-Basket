using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Polls.Dtos;
using MediatR;

namespace Application.Features.Polls.Commands.Create
{
    internal class CreateCommandHandler : IRequestHandler<CreateCommand, PollDto>
    {
        public Task<PollDto> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}