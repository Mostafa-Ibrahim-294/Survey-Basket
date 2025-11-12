using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Polls.Commands.Delete
{
    internal class DeleteCommandHandler : IRequestHandler<DeleteCommand, bool>
    {
        public Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
