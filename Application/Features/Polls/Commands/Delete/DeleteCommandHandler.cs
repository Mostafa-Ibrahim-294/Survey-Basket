using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Polls.Commands.Delete
{
    internal class DeleteCommandHandler : IRequestHandler<DeleteCommand, OneOf<bool, Error>>
    {
        private readonly IPollRepository _pollRepository;
        public DeleteCommandHandler(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }
        public async Task<OneOf<bool, Error>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var poll = await _pollRepository.GetByIdAsync(request.Id, cancellationToken);
            if (poll is null) return PollErrors.NotFound;

            await _pollRepository.DeleteAsync(poll, cancellationToken);
            return true;
        }
    }
}
