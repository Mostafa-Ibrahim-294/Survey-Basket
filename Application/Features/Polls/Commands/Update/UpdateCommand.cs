using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Polls.Commands.Update
{
    public record UpdateCommand(string Title, string Description) : IRequest<bool>;
}
