using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Polls.Commands.Update
{
    internal class UpdateCommandHandler : IRequestHandler<UpdateCommand, bool>
    {
        public Task<bool> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
