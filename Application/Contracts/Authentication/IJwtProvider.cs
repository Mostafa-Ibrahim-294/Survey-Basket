using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites;

namespace Application.Contracts.Authentication
{
    public interface IJwtProvider
    {
      public (string token , int expiresIn) GenerateToken(ApplicationUser applicationUser);
    }
}
