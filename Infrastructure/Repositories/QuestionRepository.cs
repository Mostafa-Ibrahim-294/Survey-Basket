using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Questions.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public QuestionRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuestionDto>> GetAllByPollAsync(int pollId, CancellationToken cancellationToken = default)
        {
            return await _context.Questions
                .AsNoTracking()
                .Where(q => q.PollId == pollId)
                .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<Question?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Questions
                .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
        }

        public async Task<Question> CreateAsync(Question question, CancellationToken cancellationToken = default)
        {
            await _context.Questions.AddAsync(question, cancellationToken);
            await SaveAsync(cancellationToken);
            return question;
        }

        public async Task UpdateAsync(Question question, CancellationToken cancellationToken = default)
        {
            _context.Questions.Update(question);
            await SaveAsync(cancellationToken);
        }
        public async Task<bool> IsContentExistInPollAsync(int pollId, string content, CancellationToken cancellationToken = default)
        {
            return await _context.Questions.AnyAsync(q => q.PollId == pollId && q.Content == content, cancellationToken);
        }

        public async Task<bool> IsContentExistForOtherIdInPollAsync(int pollId, string content, int id, CancellationToken cancellationToken = default)
        {
            return await _context.Questions.AnyAsync(q => q.PollId == pollId && q.Content == content && q.Id != id, cancellationToken);
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}