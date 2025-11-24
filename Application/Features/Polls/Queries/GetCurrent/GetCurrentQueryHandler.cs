using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Polls.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Features.Polls.Queries.GetCurrent
{
    internal class GetCurrentQueryHandler : IRequestHandler<GetCurrentQuery, IEnumerable<PollDto>>
    {
        private readonly IPollRepository _pollRepository;
        private readonly IMapper _mapper;
        public GetCurrentQueryHandler(IPollRepository pollRepository, IMapper mapper)
        {
            _pollRepository = pollRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PollDto>> Handle(GetCurrentQuery request, CancellationToken cancellationToken)
        {
            var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
            var polls = await _pollRepository.GetCurrentPolls(cancellationToken);
            return _mapper.Map<IEnumerable<PollDto>>(polls);
        }
    }
}
