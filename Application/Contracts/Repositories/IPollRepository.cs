using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites;

namespace Application.Contracts.Repositories
{
    public interface IPollRepository
    {
        Task<IEnumerable<Poll>> GetAll();
        Task<Poll?> GetById(int id);
        Task<Poll> Create(Poll poll);
        Task<bool> Update(Poll poll);
        Task<bool> Delete(int id);
    }
}
