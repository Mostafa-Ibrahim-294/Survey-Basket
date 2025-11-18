using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Dtos;
using Domain.Errors;
using OneOf;

namespace Application.Contracts.Authentication
{
    public interface IUserContext
    {
       public CurrentUser? GetCurrentUser();
    }
}
