using FeatureToggle.Application.DTOs;
using MediatR;

namespace FeatureToggle.Application.Requests.Queries.Login
{
    public class GetAuthTokenQuery : IRequest<LoginResponseDTO>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
