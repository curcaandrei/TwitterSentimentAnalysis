
using MediatR;

namespace Application.Features.ExternalTwitterAPI.LogInUser.ValidateAuth
{
    public class ValidateAuthQuery : IRequest<string>
    {
        public string QueryStringValue { get; set; }

        public ValidateAuthQuery(string queryStringValue)
        {
            QueryStringValue = queryStringValue;
        }
    }
}