using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Polls.Commands.Delete
{
    public record DeleteCommand(int Id) : IRequest<OneOf<bool, Error>>;
}
