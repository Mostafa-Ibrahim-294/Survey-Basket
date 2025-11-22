using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts.Repositories;
using AutoMapper;
using Domain.Errors;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using OneOf;

namespace Application.Features.Polls.Commands.Update
{
    internal class UpdateCommandHandler : IRequestHandler<UpdateCommand, OneOf<bool, Error>>
    {
        private readonly IPollRepository _pollRepository;
        private readonly IMapper _mapper;
        private readonly HybridCache _hybridCache;
        public UpdateCommandHandler(IPollRepository pollRepository, IMapper mapper, HybridCache hybridCache)
        {
            _pollRepository = pollRepository;
            _mapper = mapper;
            _hybridCache = hybridCache;
        }
        public async Task<OneOf<bool, Error>> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var poll = await _pollRepository.GetByIdAsync(request.id);
            if (poll == null) return PollErrors.NotFound;
            var exists = await _pollRepository.IsTitleExistForOtherId(request.Title, request.id);
            if (exists)
            {
                return PollErrors.DuplicateTitle;
            }
            _mapper.Map(request, poll);
            await _pollRepository.UpdateAsync(poll);
            await _hybridCache.RemoveAsync(CacheConstants.AllPolls , cancellationToken);
            return true;
        }
    }
}
