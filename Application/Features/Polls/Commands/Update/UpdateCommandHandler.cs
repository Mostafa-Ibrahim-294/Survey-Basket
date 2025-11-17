using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using AutoMapper;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Polls.Commands.Update
{
    internal class UpdateCommandHandler : IRequestHandler<UpdateCommand, OneOf<bool, Error>>
    {
        private readonly IPollRepository _pollRepository;
        private readonly IMapper _mapper;
        public UpdateCommandHandler(IPollRepository pollRepository, IMapper mapper)
        {
            _pollRepository = pollRepository;
            _mapper = mapper;
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
            return true;
        }
    }
}
