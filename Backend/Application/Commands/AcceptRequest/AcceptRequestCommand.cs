using Domain;
using MediatR;

namespace Application.Commands.AcceptRequest
{
    public class AcceptRequestCommand : IRequest<string>
    {
        public readonly string Id;
        
        public AcceptRequestCommand(string id)
        {
            Id = id;
        }
    }
}