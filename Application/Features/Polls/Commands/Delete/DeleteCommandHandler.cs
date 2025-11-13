using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using MediatR;

namespace Application.Features.Polls.Commands.Delete
{
    internal class DeleteCommandHandler : IRequestHandler<DeleteCommand, bool>
    {
        private readonly IPollRepository _pollRepository;
        public DeleteCommandHandler(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }
        public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var poll = await _pollRepository.GetByIdAsync(request.Id, cancellationToken);
            if (poll is null) return false;

            await _pollRepository.DeleteAsync(poll, cancellationToken);
            return true;
        }
    }
}
