using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts.Repositories;
using Domain.Errors;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using OneOf;

namespace Application.Features.Polls.Commands.Delete
{
    internal class DeleteCommandHandler : IRequestHandler<DeleteCommand, OneOf<bool, Error>>
    {
        private readonly IPollRepository _pollRepository;
        private readonly HybridCache _hybridCache;
        public DeleteCommandHandler(IPollRepository pollRepository, HybridCache hybridCache)
        {
            _pollRepository = pollRepository;
            _hybridCache = hybridCache;
        }
        public async Task<OneOf<bool, Error>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var poll = await _pollRepository.GetByIdAsync(request.Id, cancellationToken);
            if (poll is null) return PollErrors.NotFound;

            await _pollRepository.DeleteAsync(poll, cancellationToken);
            await _hybridCache.RemoveAsync(CacheConstants.AllPolls, cancellationToken);
            return true;
        }
    }
}
