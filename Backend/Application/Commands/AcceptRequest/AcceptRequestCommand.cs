using Domain;
using MediatR;

namespace Application.Commands.AcceptRequest
{
    public class AcceptRequestCommand : IRequest<string>
    {
        public string Id;
        
        public AcceptRequestCommand(string id)
        {
            Id = id;
        }
    }
}