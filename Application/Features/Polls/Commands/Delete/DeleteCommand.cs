using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Polls.Commands.Delete
{
    public record DeleteCommand(int Id) : IRequest<bool>;
}
